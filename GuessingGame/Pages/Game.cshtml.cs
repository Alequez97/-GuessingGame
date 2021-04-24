using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Antiforgery;
using GuessingGame.Utils;
using System.Net.Http;

namespace GuessingGame.Pages
{
    public class GameModel2 : PageModel
    {

        private readonly string gameSessionName = "_GameSession";


        public void OnGet(string name)
        {
            string number = NumberGenerator.GenerateNumber(GameSession.NumberSize);

            var gameSession = GetGameSession();     //create new GameSession instance if page is loaded first time or is entered different name
            if (gameSession == null)
            {
                CreateNewGameSession(name, number);
            }
            else
            {
                if(gameSession.PlayerName.Equals(name) == false)
                {
                    CreateNewGameSession(name, number);
                }
                else
                {
                    if (gameSession.PlayerWon == true || gameSession.TriesLeft == 0)        //if player guessed number or player used all tries, reset game
                    {
                        gameSession.Reset();
                        HttpContext.Session.Set<GameSession>(gameSessionName, gameSession);
                    }
                }
            }
        }

        private void CreateNewGameSession(string playerName, string numberToGuess)
        {
            var gameSession = new GameSession()
            {
                PlayerName = playerName,
                NumberToGuess = numberToGuess,
            };

            HttpContext.Session.Set<GameSession>(gameSessionName, gameSession);
        }

        public GameSession GetGameSession()
        {
            return HttpContext.Session.Get<GameSession>(gameSessionName);
        }
    }
}
