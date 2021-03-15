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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 1, Username = "Aleksandrs", GameIsWon = true, TriesMade = 6 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 2, Username = "Aleksandrs", GameIsWon = false, TriesMade = 8 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 3, Username = "Aleksandrs", GameIsWon = true, TriesMade = 2 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 4, Username = "Aleksandrs", GameIsWon = false, TriesMade = 8 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 5, Username = "Aleksandrs", GameIsWon = false, TriesMade = 8 });

            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 6, Username = "Sergejs", GameIsWon = true, TriesMade = 4 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 7, Username = "Sergejs", GameIsWon = true, TriesMade = 7 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 8, Username = "Sergejs", GameIsWon = false, TriesMade = 8 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 9, Username = "Sergejs", GameIsWon = false, TriesMade = 8 });

            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 10, Username = "Tair", GameIsWon = true, TriesMade = 6 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 11, Username = "Tair", GameIsWon = false, TriesMade = 8 });

            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 12, Username = "Ruslan", GameIsWon = true, TriesMade = 4 });
            modelBuilder.Entity<GameResult>().HasData(new GameResult() { Id = 13, Username = "Ruslan", GameIsWon = false, TriesMade = 8 });

        }

    }
}
