using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TddCasino.Tests.DSL
{
    public static class Extensions
    {
        public static void AssertThatChipsEqualTo(this Game game, int chipsAmount)
        {
            Assert.Equal(chipsAmount, game.Chips);
        }

        public static void AssertThatDiceCountEqualTo(this Game game, int count)
        {
            Assert.Equal(count, game.Dices.Count);
        }

        public static void AssertThatGameEqualTo(this Player player, Game game)
        {
            Assert.Equal(game, player.Game);
        }

        public static void AssertThatGameIsNull(this Player player)
        {
            Assert.Null(player.Game);
        }

        public static void AssertThatAvailableChipsEqualTo(this Player player, int count)
        {
            Assert.Equal(count, player.AvailableChips);
        }

        public static void AssertThatBetsCountsEqualTo(this Player player, int count)
        {
            Assert.Equal(count, player.GetBetsCount());
        }
    }
}
