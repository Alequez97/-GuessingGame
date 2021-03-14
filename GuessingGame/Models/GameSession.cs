using GuessingGame.Utils;
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

        public static int NumberSize { get; private set; } = 4;
        public static int TriesAtStart { get; private set; } = 8;

        public string PlayerName { get; set; }
        public string NumberToGuess{ get; set; }
        public int TriesLeft { get; set; } = TriesAtStart;

        public List<string> PlayerInputs { get; set; } = new List<string>();

        public List<GuessResult> GuessResults { get; set; } = new List<GuessResult>();

        public bool PlayerWon { get; set; } = false;

        public void Reset()
        {
            PlayerWon = false;
            TriesLeft = TriesAtStart;
            NumberToGuess = NumberGenerator.GenerateNumber(NumberSize);
            PlayerInputs.Clear();
            GuessResults.Clear();
        }

    }
}
