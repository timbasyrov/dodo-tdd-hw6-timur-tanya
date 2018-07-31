using System;
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
            var game = new Game();

            player.JoinGame(game);

            Assert.Equal(game, player.Game);
        }

        [Fact]
        //Я, как игрок, могу выйти из игры
        public void NotBeInGame_WhenLeaveGame()
        {
            var player = new Player();
            player.JoinGame(new Game());

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
            var player = new Player();
            player.JoinGame(new Game());

            Action act = () => player.JoinGame(new Game());

            Assert.Throws<AlreadyInGameException>(act);
        }
    }

}
