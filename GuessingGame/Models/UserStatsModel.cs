using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class UserStatsModel
    {

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int TotalTries { get; set; }

        public double PrecentOfWinnigGames
        {
            get
            {
                return (double)GamesWon / (double)GamesPlayed * 100;
            }
        }


        public double AverageTryCount
        {
            get
            {
                return (double)TotalTries / (double)GamesPlayed;
            }
        }

    }
}
