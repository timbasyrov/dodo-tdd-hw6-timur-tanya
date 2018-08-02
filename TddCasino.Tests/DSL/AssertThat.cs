using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TddCasino.Tests.DSL
{
    public static class AssertThat
    {
        public static void TooManyPlayers(Action act)
        {
            Assert.Throws<TooManyPlayersException>(act);
        }

        public static void NotValidBet(Action act)
        {
            Assert.Throws<NotValidBetException>(act);
        }
    }
}
