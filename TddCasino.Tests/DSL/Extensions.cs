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
    }
}
