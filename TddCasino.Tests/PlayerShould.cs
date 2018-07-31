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
            var game = new Game(new Casino(100));

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //�, ��� �����, ���� ����� �� ����
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game(new Casino(100)));

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
            player.JoinGame(new Game(casino));

            Action act = () => player.JoinGame(new Game(casino));

            Assert.Throws<AlreadyInGameException>(act);
        }
        
        [Fact]
        // �, ��� �����, ���� ������ ����� � ������, ����� ������ ������
        public void Have10AvailableChips_WhenBuy10Chips()
        {
            var game = new Game(new Casino(100));
            var player = new Player();
            player.JoinGame(game);

            player.BuyChips(10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // �, ��� �����, ���� ������� ������ � ���� � �����, ����� ��������
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
        // �, ��� �����, �� ���� ��������� ����� ������, ��� � �����
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
        // �, ��� �����, ���� ������� ��������� ������ �� ������ �����, ����� �������� ����������� ��������
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
        // �, ��� �����, ���� ��������� ������ �� ����� 1 - 6
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
        // �, ��� �����, ���� ��������� ������ �� ����� 1 - 6
        public void ThrowException_WhenBetNumberIs7()
        {
            var player = new Player();
            var game = new Game(new Casino(100));
            player.JoinGame(game);
            player.BuyChips(15);

            Action act = () => player.MakeBet(chipsAmount: 5, number: 7);

            Assert.Throws<NotValidBetNumberException>(act);
        }

        
        //    �, ��� �����, ���� ������� ��������� ������ �� ������ ����� � �������� ������� �� ���, ������� ��������

        [Fact]
        // �, ��� �����, ���� ���������, ���� ������ ������������ ������
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

        // �, ��� �����, ���� �������� 6 ������, ���� ������ ���������� ������
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
