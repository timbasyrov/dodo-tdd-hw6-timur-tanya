using Moq;

namespace TddCasino.Tests.DSL
{
    public class PlayerBuilder
    {
        private readonly Player _player;

        public PlayerBuilder()
        {
            _player = new Player();
        }

        public PlayerBuilder InGame(Game game = null)
        {
            _player.JoinGame(game ?? Create.Game.Please());

            return this;
        }

        public PlayerBuilder WithChips(int chipsAmount)
        {
            _player.BuyChips(chipsAmount);

            return this;
        }

        public PlayerBuilder WithBet(int chipsAmount, int number)
        {
            _player.MakeBet(chipsAmount, number);

            return this;
        }

        public Player Please()
        {
            return _player;
        }
    }
}
