using System;
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

            player.AssertThatGameEqualTo(game);
        }

        [Fact]
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = Create.Player.InGame().Please();

            player.LeaveGame();

            player.AssertThatGameIsNull();
        }

        [Fact]
        public void ThrowExceptionOnLeaveGame_WhenNotInGame()
        {
            var player = Create.Player.Please();

            Action act = () => player.LeaveGame();

            AssertThat.PlayerNotInGame(act);
        }

        [Fact]
        public void ThrowExceptionOnJoinGame_WhenInGame()
        {
            var player = Create.Player.InGame().Please();

            Action act = () => player.JoinGame(Create.Game.Please());

            AssertThat.PlayerAlreadyInGame(act);
        }

        [Fact]
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var player = Create.Player.InGame().Please();

            player.BuyChips(10);

            player.AssertThatAvailableChipsEqualTo(10);
        }

        [Fact]
        public void Have10AvailableChips_WhenBuy20ChipsAndMakeBetWith10Chips()
        {
            var player = Create.Player.InGame().WithChips(20).Please();

            player.MakeBet(chipsAmount: 10, number: 2);

            player.AssertThatAvailableChipsEqualTo(10);
        }

        [Fact]
        public void ThrowException_WhenBuy10ChipsAndMakeBetWith100Chips()
        {
            var player = Create.Player.InGame().WithChips(10).Please();

            Action act = () => player.MakeBet(chipsAmount: 100, number: 2);

            AssertThat.PlayerHasNotEnoughChips(act);
        }

        [Fact]
        public void Have2Bets_WhenMakeBetTwice()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            player.MakeBet(chipsAmount: 5, number: 2);
            player.MakeBet(chipsAmount: 10, number: 3);

            player.AssertThatBetsCountsEqualTo(2);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIsZero_AndOneDiceInGame()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 0);

            AssertThat.NotValidBetNumber(act);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs7_AndOneDiceInGame()
        {
            var player = Create.Player.InGame().WithChips(15).Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 7);

            AssertThat.NotValidBetNumber(act);
        }

        [Fact]
        public void Lose_WhenMadeWrongBet()
        {
            var game = Create.GameMock.WithLuckyNumber(4).Please().Object;
            var player = Create.PlayerMock
                .InGame(game)
                .WithChips(15)
                .WithBet(chipsAmount: 5, number: 5)
                .Please();

            game.Play();

            VerifyThat.LoseCalledOnceIn(player);
        }

        [Fact]
        public void GetChipsMultipleToWinCoefficient_WhenMadeRightBet()
        {            
            var game = Create.GameMock
                .WithLuckyNumber(4)
                .WithWinCoefficient(6)
                .Please().Object;
            var player = Create.Player
                .InGame(game)
                .WithChips(10)
                .WithBet(chipsAmount: 10, number: 4)
                .Please();

            game.Play();

            player.AssertThatAvailableChipsEqualTo(60);
        }

        [Fact]
        public void Win_WhenMadeAtListOneRightBet()
        {
            var game = Create.GameMock
                .WithLuckyNumber(4)
                .WithWinCoefficient(10)
                .Please().Object;
            var player = Create.PlayerMock
                .InGame(game)
                .WithChips(20)
                .WithBet(chipsAmount: 5, number: 4)
                .WithBet(chipsAmount: 10, number: 1)
                .Please();

            game.Play();

            VerifyThat.WinCalledWithBetWith50ChipsOnceIn(player);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs1_AndTwoDicesInGame()
        {
            var player = Create.Player
                .InGame(Create.Game.WithDiceCount(2).Please())
                .WithChips(15)
                .Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 1);

            AssertThat.NotValidBetNumber(act);
        }

        [Fact]
        public void ThrowException_WhenBetNumberIs13_AndTwoDicesInGame()
        {
            var player = Create.Player
                .InGame(Create.Game.WithDiceCount(2).Please())
                .WithChips(15)
                .Please();

            Action act = () => player.MakeBet(chipsAmount: 5, number: 13);

            AssertThat.NotValidBetNumber(act);
        }
    }
}
