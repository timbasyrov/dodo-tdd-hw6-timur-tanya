using Xunit;

namespace TddCasino.Tests
{
    public class СroupierShould
    {
        [Fact]
        //Я, как крупье, могу сделать игру с двумя кубиками
        public void MakeGameWithTwoDices()
        {
            var croupier = new Croupier(2);

            var game = new Game(new Casino(100), croupier);

            Assert.Equal(2, game.Croupier.Dices.Count);
        }
    }
}
