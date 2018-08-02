using System;
using System.Collections.Generic;
using System.Text;

namespace TddCasino.Tests.DSL
{
    public class PlayerBuilder
    {
        private readonly Player player;

        public PlayerBuilder()
        {
            player = new Player();
        }

        internal Player Please()
        {
            return player;
        }

    }
}
