using System;
using AutoFixture;
using System.Linq;
using Xunit;

namespace TddCasino.Tests
{
    public class GameShould 
    {
        [Fact]
        // Я, как игра, не позволяю войти более чем 6 игрокам
        public void ThrowExceptionOnJoinPlayer_When6PlayersInGame()
        {
            var fixture = new Fixture();
            var game = new Game(new Casino(100));
            fixture.CreateMany<Player>(6).ToList().ForEach(x => x.JoinGame(game));

            Action act = () => (new Player()).JoinGame(game);

            Assert.Throws<TooManyPlayersException>(act);
        }
    }
}
