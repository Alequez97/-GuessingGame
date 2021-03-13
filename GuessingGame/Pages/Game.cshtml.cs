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
    public class GameModel : PageModel
    {

        private readonly string gameSessionName = "_GameSession";


        public void OnGet()
        {
            string name = Request.Query["name"].ToString();
            string number = GenerateNumber(4);

            var gameSession = GetGameSession();     //create new inctance if page is loaded first time or is entered different name
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

        private string GenerateNumber(int size)
        {
            if (size < 1) return "";

            Random random = new Random();
            string newNumber = random.Next(1, 9).ToString();

            for (int i = 0; i < size - 1; i++)    //-1 because first number is already generated before
            {
                string s;
                do
                {
                    s = random.Next(0, 9).ToString();
                } while (newNumber.Contains(s));
                newNumber += s;
            }

            return newNumber;
        }

        public GameSession GetGameSession()
        {
            return HttpContext.Session.Get<GameSession>(gameSessionName);
        }
    }
}
