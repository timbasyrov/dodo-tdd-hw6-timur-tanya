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
        public Croupier Croupier { get; private set; }

        public Game(Casino casino, Croupier croupier)
        {
            Casino = casino;
            Croupier = croupier;
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count >= MaxPlayersCount)
            {
                throw new TooManyPlayersException();
            }

            Players.Add(player);
        }
        
        public void Play()
        {
            var luckyNumber = Croupier.RollDices();

            foreach (var player in Players)
            {
                var luckyBet = player.AllBets.FirstOrDefault(x => x.Number == luckyNumber);
                if (luckyBet != null)
                {
                    player.Win(luckyBet);
                }
                else
                {
                    player.Lose();
                }
            }
        }
    }
}