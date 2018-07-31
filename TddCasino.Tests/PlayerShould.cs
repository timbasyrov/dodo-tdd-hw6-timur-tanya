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

        
        //    Я, как игрок, не могу поставить фишек больше, чем я купил
        //    Я, как игрок, могу сделать несколько ставок на разные числа, чтобы повысить вероятность выигрыша
        //Я, как казино, принимаю только ставки, кратные 5
        //Я, как игрок, могу поставить только на числа 1 - 6

        [Fact]
        // Я, как игрок, могу купить фишки у казино, чтобы делать ставки
        public void Have10AvailableChips_WhenBuy10ChipsInCasino()
        {
            var player = new Player();
            var casino = new Casino(100);

            player.BuyChips(casino, 10);

            Assert.Equal(10, player.AvailableChips);
        }

        [Fact]
        // Я, как игрок, могу сделать ставку в игре в кости, чтобы выиграть
        public void Have9AvailableChips_WhenBuy10ChipsAndMakeBetWith1Chip()
        {
            var player = new Player();
            player.BuyChips(new Casino(100), 10);
            var game = new Game();
            player.JoinGame(game);

            player.MakeBet(chipsAmount:1, number:2);

            Assert.Equal(9, player.AvailableChips);
        }
    }

}
