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
            var game = new Game(new Casino(100), new Croupier(1));

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //Я, как игрок, могу выйти из игры
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game(new Casino(100), new Croupier(1)));

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
            player.JoinGame(new Game(casino, new Croupier(1)));

            Action act = () => player.JoinGame(new Game(casino, new Croupier(1)));

            Assert.Throws<AlreadyInGameException>(act);
        }

        [Fact]
        // Я, как игрок, могу купить фишки у казино, чтобы делать ставки
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var game = new Game(new Casino(100), new Croupier(1));
            var player = new Player();
            player.JoinGame(game);

            player.BuyChips(10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, могу сделать ставку в игре в кости, чтобы выиграть
        public void Have9AvailableChips_WhenBuy10ChipsAndMakeBetWith1Chip()
        {
            var game = new Game(new Casino(100), new Croupier(1));
            var player = new Player();
            player.JoinGame(game);
            player.BuyChips(20);

            player.MakeBet(chipsAmount: 10, number: 2);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, не могу поставить фишек больше, чем я купил
        public void ThrowException_WhenBuy10ChipsAndMakeBetWith100Chips()
        {
            var game = new Game(new Casino(100), new Croupier(1));
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
            var game = new Game(new Casino(100), new Croupier(1));
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
            var game = new Game(new Casino(100), new Croupier(1));
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
            var game = new Game(new Casino(100), new Croupier(1));
            player.JoinGame(game);
            player.BuyChips(15);

            Action act = () => player.MakeBet(chipsAmount: 5, number: 7);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        [Fact]
        // Я, как игрок, могу проиграть, если сделал неправильную ставку
        public void Lose_WhenMadeWrongBet()
        {
            var playerMock = new Mock<Player>();
            var cropierStub = new Mock<Croupier>(1);
            cropierStub.Setup(x => x.RollDices()).Returns(4);
            var game = new Game(new Casino(100), cropierStub.Object);
            playerMock.Object.JoinGame(game);
            playerMock.Object.BuyChips(15);
            playerMock.Object.MakeBet(chipsAmount: 5, number: 5);

            game.Play();

            playerMock.Verify(x => x.Lose(), Times.Once);
        }

        [Fact]
        // Я, как игрок, могу выиграть 6 ставок, если сделал правильную ставку
        public void GetChipsMultipleTo6_WhenMadeRightBet()
        {
            var cropierStub = new Mock<Croupier>(1);
            cropierStub.Setup(x => x.RollDices()).Returns(4);
            var game = new Game(new Casino(100), cropierStub.Object);
            var player = new Player();
            player.JoinGame(game);
            player.BuyChips(10);
            player.MakeBet(chipsAmount: 10, number: 4);

            game.Play();

            Assert.Equal(60, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, могу сделать несколько ставок на разные числа и получить выигрыш по тем, которые выиграли
        public void Win_WhenMadeAtListOneRightBet()
        {
            var playerMock = new Mock<Player>();
            var cropierStub = new Mock<Croupier>(1);
            cropierStub.Setup(x => x.RollDices()).Returns(4);
            var game = new Game(new Casino(100), cropierStub.Object);
            playerMock.Object.JoinGame(game);
            playerMock.Object.BuyChips(15);
            playerMock.Object.MakeBet(chipsAmount: 5, number: 4);
            playerMock.Object.MakeBet(chipsAmount: 10, number: 1);

            game.Play();

            playerMock.Verify(x => x.Win(5), Times.Once);
        }


        //    Я, как игрок, могу делать ставки на числа от 2 до 12
        //Я, как казино, определяю выигрышный коэффициент по вероятности выпадения того или иного номера

        //2 3 4 5 6 7 8 9 10 11 12
        //36 18 12 9 7 6 7 9 12 18 36


    }

}
