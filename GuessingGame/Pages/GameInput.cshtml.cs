using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Models;
using GuessingGame.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages
{
    public class GameInputModel : PageModel
    {
        public IActionResult OnGet(string input)
        {
            if (input != null)
            {
                var gameSession = HttpContext.Session.Get<GameSession>("_GameSession");
                gameSession.PlayerInputs.Add(input);
                gameSession.TriesLeft--;
                HttpContext.Session.Set<GameSession>("_GameSession", gameSession);

                var response = new Response() { LastMove = input, TriesLeft = gameSession.TriesLeft };

                return new JsonResult(response);
            }
            return new JsonResult(true) { StatusCode = 404 };
        }
    }
}
