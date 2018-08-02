using System;
using System.Collections.Generic;
using System.Linq;

namespace TddCasino
{
    public class Game
    {
        private const int MaxPlayersCount = 6;
        private Dictionary<int, int> _winCoefficients = new Dictionary<int, int>()
        {
            {2, 36}, {3, 18}, {4, 12}, {5, 9}, {6, 7}, {7, 6}, {8, 7}, {9, 9}, {10, 12}, {11, 18}, {12, 36}
        };

        public List<Player> Players = new List<Player>();
        public int Chips { get; private set; }
        public List<Dice> Dices { get; private set; }

        public Game(int diceCount)
        {
            Chips = 100;
            Dices = new List<Dice>();

            for (int i = 0; i < diceCount; i++)
            {
                Dices.Add(new Dice());
            }
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
            var luckyNumber = RollDices();

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
        
        public void SellChips(int amount)
        {
            Chips -= amount;
        }

        public void TakeChips(int amount)
        {
            Chips += amount;
        }

        public void CheckBet(Bet bet)
        {
            if (bet.ChipsAmount % 5 != 0)
            {
                throw new NotValidBetException();
            }
        }

        public virtual int GetWinCoefficient(int luckyNumber)
        {
            _winCoefficients.TryGetValue(luckyNumber, out int result);
            return result;
        }

        public virtual int RollDices()
        {
            return Dices.Sum(x => x.GetLuckyNumber());
        }
    }
}