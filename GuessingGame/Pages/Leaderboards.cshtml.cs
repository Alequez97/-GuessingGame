using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Database;
using GuessingGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages
{
    public class LeaderboardsModel : PageModel
    {
        private GuessGameDbContext dbContext;

        public LeaderboardsModel(GuessGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserStatsModel> LeaderboardRecordsList { get; set; }

        public void OnGet()
        {
            dbContext.Database.EnsureCreated();
            var gameResults = dbContext.GameResults.ToList();
            LeaderboardRecordsList = ParseDatabaseRecords(gameResults, 0);
        }

        public IActionResult OnGetUpdate(string minGames)
        {
            int minGamesCount;
            try 
            {
                minGamesCount = Convert.ToInt32(minGames);
            }
            catch 
            {
                minGamesCount = 0;
            }
            var gameResults = dbContext.GameResults.ToList();
            LeaderboardRecordsList = ParseDatabaseRecords(gameResults, minGamesCount);
            return new JsonResult(LeaderboardRecordsList);

        }

        private List<UserStatsModel> ParseDatabaseRecords(List<GameResult> gameResults, int minGamesPlayed)
        {
            Dictionary<string, UserStatsModel> leaderboardsDictionary = new Dictionary<string, UserStatsModel>();

            foreach(var gameResult in gameResults)                                  //filling dictionary with username as key and his total games stats as value
            {
                if (leaderboardsDictionary.ContainsKey(gameResult.Username))
                {
                    leaderboardsDictionary[gameResult.Username].GamesPlayed++;
                    if (gameResult.GameIsWon) leaderboardsDictionary[gameResult.Username].GamesWon++;
                    leaderboardsDictionary[gameResult.Username].TotalTries += gameResult.TriesMade;
                }
                else
                {
                    var userStats = new UserStatsModel()
                    {
                        Username = gameResult.Username,
                        GamesPlayed = 1,
                        GamesWon = gameResult.GameIsWon ? 1 : 0,
                        TotalTries = gameResult.TriesMade
                    };
                    leaderboardsDictionary.Add(gameResult.Username, userStats);
                }
            }

            return SortRecords(leaderboardsDictionary, minGamesPlayed);
        }

        private List<UserStatsModel> SortRecords(Dictionary<string, UserStatsModel> leaderboardsDictionary, int minGamesPlayed)
        {
            for (int i = 0; i < leaderboardsDictionary.Count; i++)      //remove records where user played less games than in input
            {
                var item = leaderboardsDictionary.ElementAt(i);
                if (item.Value.GamesPlayed < minGamesPlayed)
                {
                    leaderboardsDictionary.Remove(item.Key);
                    i--;
                }
            }

            List<UserStatsModel> newLeaderboards = new List<UserStatsModel>();

            string currentUsername = "";
            double currentWinRate = -1;
            double currentTryRate = -1;
            int index = 0;

            for (int i = 0; i < leaderboardsDictionary.Count; i++)
            {
                for (int j = 0; j < leaderboardsDictionary.Count; j++)
                {
                    var record = leaderboardsDictionary.ElementAt(j);
                    if (record.Value.PrecentOfWinnigGames > currentWinRate)
                    {
                        currentUsername = record.Key;
                        currentWinRate = record.Value.PrecentOfWinnigGames;
                        currentTryRate = record.Value.AverageTryCount;
                        index = j;
                    }
                    else if (record.Value.PrecentOfWinnigGames == currentWinRate)
                    {
                        if (record.Value.AverageTryCount < currentTryRate)
                        {
                            currentUsername = record.Key;
                            currentWinRate = record.Value.PrecentOfWinnigGames;
                            currentTryRate = record.Value.AverageTryCount;
                            index = j;
                        }
                    }
                }

                var currentTopPlayer = leaderboardsDictionary.ElementAt(index);

                currentUsername = "";
                currentWinRate = -1;
                currentTryRate = -1;
                index = 0;

                newLeaderboards.Add(currentTopPlayer.Value);

                leaderboardsDictionary.Remove(currentTopPlayer.Key);
                i--;
            }

            return newLeaderboards;
        }
    }
}
