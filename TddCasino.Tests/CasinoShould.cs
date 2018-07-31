using System;
using Moq;
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

        // Я, как казино, получаю фишки, которые проиграл игрок
        [Fact]
        public void GetPlayerChips_WhenHeLose()
        {
            var casino = new Casino(100);
            var gameStub = new Mock<Game>(casino);
            gameStub.Setup(x => x.GetLuckyNumber()).Returns(4);
            var player = new Player();
            player.JoinGame(gameStub.Object);
            player.BuyChips(10);
            player.MakeBet(chipsAmount: 10, number: 1);

            gameStub.Object.Play();

            Assert.Equal(100, casino.Chips);
        }
    }
}
