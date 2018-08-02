using System.Linq;
using AutoFixture;
using Moq;

namespace TddCasino.Tests.DSL
{
    public class GameBuilder
    {
        private int _diceCount = 1;
        private int _playersCount;

        public GameBuilder WithDiceCount(int diceCount)
        {
            _diceCount = diceCount;

            return this;
        }

        public GameBuilder WithPlayersCount(int count)
        {
            _playersCount = count;

            return this;
        }

        internal Game Please()
        {
            var game = new Game(_diceCount);

            if (_playersCount > 0)
            {
                var fixture = new Fixture();
                fixture.CreateMany<Player>(_playersCount).ToList().ForEach(x => x.JoinGame(game));
            }

            return game;
        }

    }
}
