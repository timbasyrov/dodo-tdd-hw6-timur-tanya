using System;
using Xunit;

namespace TddCasino.Tests
{
    public class CasinoShould
    {
        // Я, как казино, принимаю только ставки, кратные 5
        [Fact]
        public void ThrowException_WhenBetIsNotMultipleTo5()
        {
            var player = new Player();
            player.JoinGame(new Game(new Casino(100)));
            player.BuyChips(10);

            Action act = () => player.MakeBet(9, 2);

            Assert.Throws<NotValidBetException>(act);
        }
    }
}
