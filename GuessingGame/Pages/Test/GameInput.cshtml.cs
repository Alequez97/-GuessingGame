using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages.Test
{
    public class GameInputModel : PageModel
    {

        private GameSession gameSession;

        public GameInputModel(GameSession mockGameSession)
        {
            gameSession = mockGameSession;
        }

        public GameSession OnGet(string input)
        {
            if (input != null && gameSession.TriesLeft > 0 && gameSession.PlayerWon == false)
            {
                if (input.Length != GameSession.NumberSize)
                {
                    return gameSession;
                }

                gameSession.PlayerInputs.Add(input);
                gameSession.TriesLeft--;
                var lastGuessResult = CheckInputResult(input, gameSession.NumberToGuess, gameSession.NumberToGuess.Length);

                if (lastGuessResult.CorrectPositions == GameSession.NumberSize)
                {
                    gameSession.PlayerWon = true;
                }

                gameSession.GuessResults.Add(lastGuessResult);

                return gameSession;
            }
            return gameSession;
        }

        private GuessResult CheckInputResult(string playerInput, string numberToGuess, int numberSize)
        {
            int correctPositions = 0, correctMatches = 0;

            for (int i = 0; i < numberSize; i++)
            {

                if (playerInput[i] == numberToGuess[i])     //Check if input number matches secret number
                {
                    correctPositions++;
                }
                else                                        //if not, than check if secret number contains that number
                {
                    if (numberToGuess.Contains(playerInput[i]))
                    {
                        correctMatches++;
                    }
                }
            }

            return new GuessResult { CorrectMatches = correctMatches, CorrectPositions = correctPositions };
        }
    }
}
