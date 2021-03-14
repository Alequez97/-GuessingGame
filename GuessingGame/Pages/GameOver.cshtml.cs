using GuessingGame.Models;
using GuessingGame.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages
{
    public class GameOverModel : PageModel
    {
        public void OnGet()
        {
        }

        public GameSession GetGameSession()
        {
            return HttpContext.Session.Get<GameSession>("_GameSession");
        }
    }
}
