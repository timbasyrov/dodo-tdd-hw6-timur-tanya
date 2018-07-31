using System;
using System.Collections.Generic;

namespace TddCasino
{
    public class Game
    {
        private const int MaxPlayersCount = 6;
        public Dictionary<Player, Bet> Players = new Dictionary<Player, Bet>();

        public void AddPlayer(Player player)
        {
            if (Players.Count >= MaxPlayersCount)
            {
                throw new TooManyPlayersException();
            }

            Players.Add(player, null);
        }

        public bool HasBetFrom(Player player)
        {
            return Players.ContainsKey(player);
        }

        public void AcceptBet(Player player, Bet bet)
        {
            Players[player] = bet;
        }
    }
}