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

        public static void PlayerNotInGame(Action act)
        {
            Assert.Throws<NotInGameException>(act);
        }

        public static void PlayerAlreadyInGame(Action act)
        {
            Assert.Throws<AlreadyInGameException>(act);
        }

        public static void PlayerHasNotEnoughChips(Action act)
        {
            Assert.Throws<NotEnoughChipsException>(act);
        }

        public static void NotValidBetNumber(Action act)
        {
            Assert.Throws<NotValidBetNumberException>(act);
        }
    }
}
