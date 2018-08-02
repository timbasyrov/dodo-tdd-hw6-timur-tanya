using System;
using System.Collections.Generic;
using System.Text;

namespace TddCasino.Tests.DSL
{
    public static class Create
    {       
        public static PlayerMockBuilder PlayerMock => new PlayerMockBuilder();

        public static PlayerBuilder Player => new PlayerBuilder();

        public static GameMockBuilder GameMock => new GameMockBuilder();
        public static GameBuilder Game => new GameBuilder();
    }
}
