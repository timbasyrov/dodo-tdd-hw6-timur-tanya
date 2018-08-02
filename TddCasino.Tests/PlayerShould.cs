using System;
using System.Runtime.InteropServices;
using Moq;
using TddCasino.Tests.DSL;
using Xunit;

namespace TddCasino.Tests
{
    public class PlayerShould
    {
        [Fact]
        public void BeInGame_WhenJoinGame()
        {
            var game = Create.Game.Please();
            var player = Create.Player.Please();

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = Create.Player.InGame().Please();

            player.LeaveGame();

            Assert.Null(player.Game);
        }

        [Fact]
        public void ThrowExceptionOnLeaveGame_WhenNotInGame()
        {
            var player = Create.Player.Please();

            Action act = () => player.LeaveGame();

            Assert.Throws<NotInGameException>(act);
        }

        [Fact]
        public void ThrowExceptionOnJoinGame_WhenInGame()
        {
            var player = Create.Player.InGame().Please();

            Action act = () => player.JoinGame(Create.Game.Please());

            Assert.Throws<AlreadyInGameException>(act);
        }

        [Fact]
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var player = Create.Player.InGame().Please();

            player.BuyChips(10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        public void Have10AvailableChips_WhenBuy20ChipsAndMakeBetWith10Chips()
        {
            var player = Create.Player.InGame().WithChips(20).Please();

            player.MakeBet(chipsAmount: 10, number: 2);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        public void ThrowException_WhenBuy10ChipsAndMakeBetWith100Chips()
        {
            var player = Create.Player.InGame().WithChips(10).Please();

            Action act = () => player.MakeBet(chipsAmount: 100, number: 2);

            Assert.Throws<NotEnoughChipsException>(act);
        }

        [Fact]
        public void Have2Bets_WhenMakeBetTwice()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            player.MakeBet(chipsAmount: 5, number: 2);
            player.MakeBet(chipsAmount: 10, number: 3);

            Assert.Equal(2, player.AllBets.Count);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIsZero_AndOneDiceInGame()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 0);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs7_AndOneDiceInGame()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 7);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        [Fact]
        public void Lose_WhenMadeWrongBet()
        {
            var game = Create.GameMock.WithLuckyNumber(4).Please().Object;
            var playerMock = Create.PlayerMock
                .InGame(game)
                .WithChips(15)
                .WithBet(chipsAmount: 5, number: 5)
                .Please();

            game.Play();

            playerMock.Verify(x => x.Lose(), Times.Once);
        }

        [Fact]
        public void GetChipsMultipleTo6_WhenMadeRightBet()
        {            
            var game = Create.GameMock.WithLuckyNumber(4).Please().Object;
            var player = Create.Player
                .InGame(game)
                .WithChips(10)
                .WithBet(chipsAmount: 10, number: 4)
                .Please();

            game.Play();

            Assert.Equal(60, player.AvailableChips);
        }

        [Fact]
        public void Win_WhenMadeAtListOneRightBet()
        {
            var game = Create.GameMock.WithLuckyNumber(4).Please().Object;
            var playerMock = Create.PlayerMock
                .InGame(game)
                .WithChips(15)
                .WithBet(chipsAmount: 5, number: 4)
                .WithBet(chipsAmount: 10, number: 1)
                .Please();

            game.Play();

            playerMock.Verify(x => x.Win(It.Is<Bet>(y => y.ChipsAmount == 5)), Times.Once);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs1_AndTwoDicesInGame()
        {
            var player = Create.Player
                .InGame(Create.Game.WithDiceCount(2).Please())
                .WithChips(15)
                .Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 1);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs13_AndTwoDicesInGame()
        {
            var player = Create.Player
                .InGame(Create.Game.WithDiceCount(2).Please())
                .WithChips(15)
                .Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 13);

            Assert.Throws<NotValidBetNumberException>(act);
        }
    }
}
