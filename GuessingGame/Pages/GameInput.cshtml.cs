using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Database;
using GuessingGame.Models;
using GuessingGame.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages
{
    public class GameInputModel : PageModel
    {

        private GuessGameDbContext dbContext;

        public GameInputModel(GuessGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult OnGet(string input)
        {

            var gameSession = HttpContext.Session.Get<GameSession>("_GameSession");

            if (input != null && gameSession.TriesLeft > 0 && gameSession.PlayerWon == false)
            {
                gameSession.PlayerInputs.Add(input);
                gameSession.TriesLeft--;
                var lastGuessResult = CheckInputResult(input, gameSession.NumberToGuess, gameSession.NumberToGuess.Length);

                if (lastGuessResult.CorrectPositions == GameSession.NumberSize)
                {
                    gameSession.PlayerWon = true;
                    SaveGameResult(gameSession.PlayerName, true, gameSession.UsedTries);
                }

                if (gameSession.TriesLeft == 0 && gameSession.PlayerWon == false)
                {
                    SaveGameResult(gameSession.PlayerName, false, gameSession.UsedTries);
                }

                gameSession.GuessResults.Add(lastGuessResult);

                HttpContext.Session.Set<GameSession>("_GameSession", gameSession);
                var response = new Response() 
                { 
                    LastMove = input, 
                    TriesLeft = gameSession.TriesLeft, 
                    LastGuessResult = lastGuessResult,
                    PlayerWon = gameSession.PlayerWon,
                };

                return new JsonResult(response);
            }
            return new JsonResult(true) { StatusCode = 404 };
        }

        private bool SaveGameResult(string playerName, bool playerWon, int triesMade)
        {
            var gameResult = new GameResult()
            {
                Username = playerName,
                GameIsWon = playerWon,
                TriesMade = triesMade,
            };

            try
            {
                dbContext.GameResults.Add(gameResult);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
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
