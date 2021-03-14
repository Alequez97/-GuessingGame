using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Database
{
    public class GuessGameDbContext : DbContext
    {

        public GuessGameDbContext(DbContextOptions<GuessGameDbContext> options) : base(options)
        {

        }

        public DbSet<GameResult> GameResults { get; set; }

    }
}
