using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using AutoFixture;
using Moq;

namespace TddCasino.Tests.DSL
{
    public class GameMockBuilder
    {
        private int _diceCount = 1;
        private int _playersCount;
        private int? _luckyNumber;
        private int? _winCoefficient;

        public GameMockBuilder WithDiceCount(int diceCount)
        {
            _diceCount = diceCount;

            return this;
        }

        public GameMockBuilder WithLuckyNumber(int number)
        {
            _luckyNumber = number;

            return this;
        }

        public GameMockBuilder WithWinCoefficient(int coefficient)
        {
            _winCoefficient = coefficient;

            return this;
        }

        public GameMockBuilder WithPlayersCount(int count)
        {
            _playersCount = count;

            return this;
        }

        internal Mock<Game> Please()
        {
            var game = new Mock<Game>(_diceCount);

            if (_playersCount > 0)
            {
                var fixture = new Fixture();
                fixture.CreateMany<Player>(_playersCount).ToList().ForEach(x => x.JoinGame(game.Object));
            }

            if (_winCoefficient != null)
            {
                game.Setup(x => x.GetWinCoefficient(It.IsAny<int>())).Returns((int)_winCoefficient);
            }

            if (_luckyNumber != null)
            {
                game.Setup(x => x.RollDices()).Returns((int)_luckyNumber);
            }

            return game;
        }

    }
}
