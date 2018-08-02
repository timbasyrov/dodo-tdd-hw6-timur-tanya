using Moq;

namespace TddCasino.Tests.DSL
{
    public class PlayerMockBuilder
    {
        private readonly Mock<Player> _playerMock;

        public PlayerMockBuilder()
        {
            _playerMock = new Mock<Player>();
        }

        public PlayerMockBuilder InGame(Game game = null)
        {
            _playerMock.Object.JoinGame(game ?? Create.Game.Please());

            return this;
        }

        public PlayerMockBuilder WithChips(int chipsAmount)
        {
            _playerMock.Object.BuyChips(chipsAmount);

            return this;
        }

        

        public PlayerMockBuilder WithBet(int chipsAmount, int number)
        {
            _playerMock.Object.MakeBet(chipsAmount, number);

            return this;
        }

        public Mock<Player> Please()
        {
            return _playerMock;
        }
    }
}
