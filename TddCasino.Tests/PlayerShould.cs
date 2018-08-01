using System;
using Moq;
using Xunit;

namespace TddCasino.Tests
{
    public class PlayerShould
    {

        [Fact]
        // �, ��� �����, ���� ����� � ����
        public void BeInGame_WhenJoinGame()
        {
            var player = new Player();
            var game = new Game(new Casino(100), new Croupier(1));

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //�, ��� �����, ���� ����� �� ����
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game(new Casino(100), new Croupier(1)));

            player.LeaveGame();

            Assert.Null(player.Game);
        }

        [Fact]
        //�, ��� �����, �� ���� ����� �� ����, ���� � � ��� �� ������
        public void ThrowExceptionOnLeaveGame_WhenNotInGame()
        {
            var player = new Player();

            Action act = () => player.LeaveGame();

            Assert.Throws<NotInGameException>(act);
        }

        [Fact]
        //�, ��� �����, ���� ������ ������ � ���� ���� ������������
        public void ThrowExceptionOnJoinGame_WhenInGame()
        {
            var casino = new Casino(100);
            var player = new Player();
            player.JoinGame(new Game(casino, new Croupier(1)));

            Action act = () => player.JoinGame(new Game(casino, new Croupier(1)));

            Assert.Throws<AlreadyInGameException>(act);
        }

        [Fact]
        // �, ��� �����, ���� ������ ����� � ������, ����� ������ ������
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var game = new Game(new Casino(100), new Croupier(1));
            var player = new Player();
            player.JoinGame(game);

            player.BuyChips(10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // �, ��� �����, ���� ������� ������ � ���� � �����, ����� ��������
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
        // �, ��� �����, �� ���� ��������� ����� ������, ��� � �����
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
        // �, ��� �����, ���� ������� ��������� ������ �� ������ �����, ����� �������� ����������� ��������
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
        // �, ��� �����, ���� ��������� ������ �� ����� 1 - 6
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
        // �, ��� �����, ���� ��������� ������ �� ����� 1 - 6
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
        // �, ��� �����, ���� ���������, ���� ������ ������������ ������
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
        // �, ��� �����, ���� �������� 6 ������, ���� ������ ���������� ������
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
        // �, ��� �����, ���� ������� ��������� ������ �� ������ ����� � �������� ������� �� ���, ������� ��������
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


        //    �, ��� �����, ���� ������ ������ �� ����� �� 2 �� 12
        //�, ��� ������, ��������� ���������� ����������� �� ����������� ��������� ���� ��� ����� ������

        //2 3 4 5 6 7 8 9 10 11 12
        //36 18 12 9 7 6 7 9 12 18 36


    }

}
