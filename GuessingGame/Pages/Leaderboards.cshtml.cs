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

        public void OnGet()
        {
            var gameResults = dbContext.GameResults.ToList();
            LeaderboardRecords = ParseDatabaseRecords(gameResults);
        }

        private Dictionary<string, UserStatsModel> ParseDatabaseRecords(List<GameResult> gameResults)
        {
            Dictionary<string, UserStatsModel> leaderboardsDictionary = new Dictionary<string, UserStatsModel>();

            foreach(var gameResult in gameResults)
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

            return leaderboardsDictionary;

        }
    }
}
