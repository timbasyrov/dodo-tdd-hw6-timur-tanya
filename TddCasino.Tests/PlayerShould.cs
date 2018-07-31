using System;
using Moq;
using Xunit;

namespace TddCasino.Tests
{
    public class PlayerShould
    {
        
        [Fact]
        // Я, как игрок, могу войти в игру
        public void BeInGame_WhenJoinGame()
        {
            var player = new Player();
            var game = new Game(new Casino(100));

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //Я, как игрок, могу выйти из игры
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game(new Casino(100)));

            player.LeaveGame();

            Assert.Null(player.Game);
        }

        [Fact]
        //Я, как игрок, не могу выйти из игры, если я в нее не входил
        public void ThrowExceptionOnLeaveGame_WhenNotInGame()
        {
            var player = new Player();

            Action act = () => player.LeaveGame();

            Assert.Throws<NotInGameException>(act);
        }

        [Fact]
        //Я, как игрок, могу играть только в одну игру одновременно
        public void ThrowExceptionOnJoinGame_WhenInGame()
        {
            var casino = new Casino(100);
            var player = new Player();
            player.JoinGame(new Game(casino));

            Action act = () => player.JoinGame(new Game(casino));

            Assert.Throws<AlreadyInGameException>(act);
        }
        
        [Fact]
        // Я, как игрок, могу купить фишки у казино, чтобы делать ставки
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var game = new Game(new Casino(100));
            var player = new Player();
            player.JoinGame(game);

            player.BuyChips(10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, могу сделать ставку в игре в кости, чтобы выиграть
        public void Have9AvailableChips_WhenBuy10ChipsAndMakeBetWith1Chip()
        {
            var game = new Game(new Casino(100));
            var player = new Player();
            player.JoinGame(game);
            player.BuyChips(20);

            player.MakeBet(chipsAmount:10, number:2);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, не могу поставить фишек больше, чем я купил
        public void ThrowException_WhenBuy10ChipsAndMakeBetWith100Chips()
        {
            var game = new Game(new Casino(100));
            var player = new Player();
            player.JoinGame(game);
            player.BuyChips(10);

            Action act = () => player.MakeBet(chipsAmount: 100, number: 2);

            Assert.Throws<NotEnoughChipsException>(act);
        }

        [Fact]
        // Я, как игрок, могу сделать несколько ставок на разные числа, чтобы повысить вероятность выигрыша
        public void Have2Bets_WhenMakeBetTwice()
        {
            var player = new Player();
            var game = new Game(new Casino(100));
            player.JoinGame(game);
            player.BuyChips(15);
            
            player.MakeBet(chipsAmount: 5, number: 2);
            player.MakeBet(chipsAmount: 10, number: 3);

            Assert.Equal(2, player.AllBets.Count);
        }

        [Fact]
        // Я, как игрок, могу поставить только на числа 1 - 6
        public void ThrowException_WhenBetNumberIsZero()
        {
            var player = new Player();
            var game = new Game(new Casino(100));
            player.JoinGame(game);
            player.BuyChips(15);

            Action act = () => player.MakeBet(chipsAmount: 5, number: 0);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        [Fact]
        // Я, как игрок, могу поставить только на числа 1 - 6
        public void ThrowException_WhenBetNumberIs7()
        {
            var player = new Player();
            var game = new Game(new Casino(100));
            player.JoinGame(game);
            player.BuyChips(15);

            Action act = () => player.MakeBet(chipsAmount: 5, number: 7);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        
        //    Я, как игрок, могу сделать несколько ставок на разные числа и получить выигрыш по тем, которые выиграли

        [Fact]
        // Я, как игрок, могу проиграть, если сделал неправильную ставку
        public void Lose_WhenMadeWrongBet()
        {
            var playerMock = new Mock<Player>();
            var gameStub = new Mock<Game>(new Casino(100));
            gameStub.Setup(x => x.GetLuckyNumber()).Returns(4);
            playerMock.Object.JoinGame(gameStub.Object);
            playerMock.Object.BuyChips(15);
            playerMock.Object.MakeBet(chipsAmount: 5, number: 5);

            gameStub.Object.Play();

            playerMock.Verify(x => x.Lose(), Times.Once);
        }

        // Я, как игрок, могу выиграть 6 ставок, если сделал правильную ставку
        [Fact]
        public void GetChipsMultipleTo6_WhenMadeRightBet()
        {
            var gameStub = new Mock<Game>(new Casino(100));
            gameStub.Setup(x => x.GetLuckyNumber()).Returns(4);
            var player = new Player();
            player.JoinGame(gameStub.Object);
            player.BuyChips(10);
            player.MakeBet(chipsAmount: 10, number: 4);

            gameStub.Object.Play();

            Assert.Equal(60, player.AvailableChips);
        }

    }

}
