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
            var game = new Game();

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //�, ��� �����, ���� ����� �� ����
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game());

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
            var player = new Player();
            player.JoinGame(new Game());

            Action act = () => player.JoinGame(new Game());

            Assert.Throws<AlreadyInGameException>(act);
        }
    }

}
