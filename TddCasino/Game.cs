using System;
using System.Collections.Generic;

namespace TddCasino
{
    public class Game
    {
        private const int MaxPlayersCount = 6;
        public List<Player> Players = new List<Player>();

        public void AddPlayer(Player player)
        {
            if (Players.Count >= MaxPlayersCount)
            {
                throw new TooManyPlayersException();
            }

            Players.Add(player);
        }

        public bool HasBetFrom(Player player)
        {
            return Players.Contains(player);
        }
    }
}