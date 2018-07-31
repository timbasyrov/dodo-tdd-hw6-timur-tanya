using System;
using System.Collections.Generic;
using System.Linq;

namespace TddCasino
{
    public class Game
    {
        private const int MaxPlayersCount = 6;

        public List<Player> Players = new List<Player>();

        public Casino Casino { get; private set; }

        public Game(Casino casino)
        {
            Casino = casino;
        }

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

        public virtual int GetLuckyNumber()
        {
            return (new Random()).Next(1, 6);
        }

        public void Play()
        {
            var luckyNumber = GetLuckyNumber();

            foreach (var player in Players)
            {
                var luckyBet = player.AllBets.FirstOrDefault(x => x.Number == luckyNumber);
                if (luckyBet != null)
                {
                    player.Win(luckyBet.ChipsAmount);
                }
                else
                {
                    player.Lose();
                }
            }
        }
    }
}