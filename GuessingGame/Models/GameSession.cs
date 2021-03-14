using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class GameSession
    {

        public GameSession()
        {

        }

        public string PlayerName { get; set; }
        public string NumberToGuess{ get; set; }
        public int TriesLeft { get; set; } = 8;

        public List<string> PlayerInputs { get; set; } = new List<string>();

        public List<GuessResult> GuessResults { get; set; } = new List<GuessResult>();

        

    }
}
