using NUnit.Framework;
using GuessingGame.Pages.Test;
using Microsoft.AspNetCore.Mvc;
using GuessingGame.Models;
using System.Linq;
using Newtonsoft.Json;

namespace GuessingGameTests
{
    public class GameInputTest
    {

        GameInputModel gameInputTestPage;
        GameSession mockGameSession;

        [SetUp]
        public void Setup()
        {
            mockGameSession = new GameSession()
            {
                NumberToGuess = "1234",
                PlayerName = "Mock",
            };

            gameInputTestPage = new GameInputModel(mockGameSession);
        }

        [Test]
        public void EmptyInputTest()
        {
            string input = "";
            var result = gameInputTestPage.OnGet(input);
            Assert.AreEqual(mockGameSession.TriesLeft, GameSession.TriesAtStart);
            Assert.AreEqual(0, mockGameSession.PlayerInputs.Count);
            Assert.AreEqual(0, mockGameSession.GuessResults.Count);
        }

        [Test]
        public void InputLengthNotEqualWithSecretTest()
        {
            string input = "321";
            var result = gameInputTestPage.OnGet(input);
            Assert.AreEqual(mockGameSession.TriesLeft, GameSession.TriesAtStart);
            Assert.AreEqual(0, mockGameSession.PlayerInputs.Count);
            Assert.AreEqual(0, mockGameSession.GuessResults.Count);
        }

        [Test]
        public void PartialCorrectInputTest()
        {
            string input = "5164";
            var result = gameInputTestPage.OnGet(input);

            var lastTryResult = mockGameSession.GuessResults.LastOrDefault();
            Assert.AreNotEqual(null, lastTryResult);

            Assert.AreEqual(1, lastTryResult.CorrectPositions);
            Assert.AreEqual(1, lastTryResult.CorrectMatches);

            input = "1432";
            result = gameInputTestPage.OnGet(input);

            lastTryResult = mockGameSession.GuessResults.LastOrDefault();
            Assert.AreEqual(mockGameSession.TriesLeft, GameSession.TriesAtStart - 2);
            Assert.AreEqual(2, lastTryResult.CorrectMatches);
            Assert.AreEqual(2, lastTryResult.CorrectPositions);
        }

        [Test]
        public void CorrectInputTest()
        {
            string input = "1234";
            var result = gameInputTestPage.OnGet(input);

            var lastTryResult = mockGameSession.GuessResults.LastOrDefault();
            Assert.AreNotEqual(null, lastTryResult);

            Assert.AreEqual(4, lastTryResult.CorrectPositions);
            Assert.AreEqual(0, lastTryResult.CorrectMatches);
            Assert.AreEqual(true, mockGameSession.PlayerWon);

            input = "4321";
            result = gameInputTestPage.OnGet(input);

            Assert.AreEqual(1, mockGameSession.GuessResults.Count);
            Assert.AreEqual(1, mockGameSession.PlayerInputs.Count);
            Assert.AreEqual(true, mockGameSession.PlayerWon);
        }
    }
}