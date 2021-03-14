using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Database
{
    public class GameResult
    {

        public int Id { get; set; }

        public string Username { get; set; }

        public int TriesMade { get; set; }

        public bool GameIsWon { get; set; }

    }
}
