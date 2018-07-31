using System;
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
        
        //�, ��� �����, ���� ��������� ������ �� ����� 1 - 6

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
    }

}
