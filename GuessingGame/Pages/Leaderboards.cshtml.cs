using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuessingGame.Pages
{
    public class LeaderboardsModel : PageModel
    {
        private GuessGameDbContext dbContext;

        public LeaderboardsModel(GuessGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<GameResult> GameResults { get; private set; }

        public void OnGet()
        {
            GameResults = dbContext.GameResults.ToList();
        }
    }
}
