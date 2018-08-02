using System;
using System.Collections.Generic;
using System.Text;

namespace TddCasino.Tests.DSL
{
    public class GameBuilder
    {
        private readonly Casino casino;
        private readonly Croupier croupier;

        public GameBuilder()
        {
        }

        internal Game Please()
        {
            return new Game(casino ?? new Casino(100), croupier ?? new Croupier(1));
        }

    }
}
