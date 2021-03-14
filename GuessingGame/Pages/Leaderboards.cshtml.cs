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

        public Dictionary<string, UserStatsModel> LeaderboardRecords { get; private set; }

        public void OnGet(string minGames)
        {
            int minGamesCount = (minGames == null) ? 0 : Convert.ToInt32(minGames);
            var gameResults = dbContext.GameResults.ToList();
            LeaderboardRecords = ParseDatabaseRecords(gameResults, minGamesCount);
        }

        private Dictionary<string, UserStatsModel> ParseDatabaseRecords(List<GameResult> gameResults, int minGamesPlayed)
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
                        GamesPlayed = 1,
                        GamesWon = gameResult.GameIsWon ? 1 : 0,
                        TotalTries = gameResult.TriesMade
                    };
                    leaderboardsDictionary.Add(gameResult.Username, userStats);
                }
            }

            return SortRecords(new Dictionary<string, UserStatsModel>(leaderboardsDictionary), minGamesPlayed);
        }

        private Dictionary<string, UserStatsModel> SortRecords(Dictionary<string, UserStatsModel> leaderboardsDictionary, int minGamesPlayed)
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

            Dictionary<string, UserStatsModel> newLeaderboardsDictionary = new Dictionary<string, UserStatsModel>();

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

                newLeaderboardsDictionary.Add(currentTopPlayer.Key, currentTopPlayer.Value);
                leaderboardsDictionary.Remove(currentTopPlayer.Key);
                i--;
            }

            return newLeaderboardsDictionary;
        }
    }
}
