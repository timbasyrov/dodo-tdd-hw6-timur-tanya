using System;
using Moq;
using TddCasino.Tests.DSL;
using Xunit;

namespace TddCasino.Tests
{
    public class GameShould 
    {
        [Fact]
        public void ThrowExceptionOnJoinPlayer_When6PlayersInGame()
        {           
            var game = Create.Game.WithPlayersCount(6).Please();
            
            Action act = () => (new Player()).JoinGame(game);

            AssertThat.TooManyPlayers(act);
        }

        [Fact]
        public void ThrowException_WhenBetIsNotMultipleTo5()
        {
            var player = Create.Player.InGame().WithChips(10).Please();

            Action act = () => player.MakeBet(9, 2);

            AssertThat.NotValidBet(act);
        }

        [Fact]
        public void GetPlayerChips_WhenHeLose()
        {
            var game = Create.GameMock.WithLuckyNumber(4).Please().Object;
            var expectedChips = game.Chips;
            var player = Create
                .Player
                .InGame(game)
                .WithChips(10)
                .WithBet(chipsAmount: 10, number: 1)
                .Please();

            game.Play();

            game.AssertThatChipsEqualTo(expectedChips);
        }

        [Fact]
        public void GetWinCoefficient_WhenPlayerWinWithTwoDicesInGame()
        {
            var game = Create.GameMock.WithDiceCount(2).WithLuckyNumber(4).Please();
            var player = Create.Player.InGame(game.Object).WithChips(10).WithBet(chipsAmount: 10, number: 4);

            game.Object.Play();

            VerifyThat.GetWinCoefficientCallOnceIn(game);
        }

        [Fact]
        public void MakeGameWithTwoDices()
        {
            var game = Create.Game.WithDiceCount(2).Please();

            Assert.Equal(2, game.Dices.Count);
        }
    }
}
