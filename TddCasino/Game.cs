using System;
using System.Collections.Generic;

namespace TddCasino
{
    public class Game
    {
        public List<Player> Players = new List<Player>();

        public Game()
        {
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count >= 6)
            {
                throw new Exception();
            }

            Players.Add(player);
        }
    }
}