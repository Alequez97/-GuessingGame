using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class Response
    {

        public string LastMove { get; set; }

        public GuessResult LastGuessResult { get; set; }

        public int TriesLeft { get; set; }

        public bool PlayerWon { get; set; }

    }
}
