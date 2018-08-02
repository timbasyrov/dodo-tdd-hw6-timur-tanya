using System;
using System.Collections.Generic;
using System.Text;

namespace TddCasino.Tests.DSL
{
    public static class Create
    {
        public static PlayerBuilder Player => new PlayerBuilder();

        public static GameBuilder Game => new GameBuilder();

        //public static CasinoBuilder Casino => new CasinoBuilder();

        //public static CroupierBuilder Croupier => newCroupierBuilder();

        //public static DiceBuilder Dice => new DiceBuilder();
    }
}
