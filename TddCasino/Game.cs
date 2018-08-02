using System;
using System.Collections.Generic;
using System.Linq;

namespace TddCasino
{
    public class Game
    {
        private const int MaxPlayersCount = 6;
        private readonly Dictionary<int, int> _winCoefficients = new Dictionary<int, int>()
        {
            {2, 36}, {3, 18}, {4, 12}, {5, 9}, {6, 7}, {7, 6}, {8, 7}, {9, 9}, {10, 12}, {11, 18}, {12, 36}
        };

        private readonly List<Player> _players = new List<Player>();

        public int Chips { get; private set; }

        public List<Dice> Dices { get; }

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
            if (_players.Count >= MaxPlayersCount)
            {
                throw new TooManyPlayersException();
            }

            _players.Add(player);
        }
        
        public void Play()
        {
            var luckyNumber = RollDices();

            foreach (var player in _players)
            {
                var luckyBet = player.FindBet(luckyNumber);
                if (luckyBet != null)
                {
                    var coefficient = Dices.Count == 1 ? 6 : GetWinCoefficient(luckyNumber);
                    var chips = luckyBet.ChipsAmount * coefficient;

                    player.WinChips(chips);
                }
                else
                {
                    var lostChips = player.GetChipsFromAllBets();
                    player.LoseChips(lostChips);
                    Chips += lostChips;
                }
            }
        }
        
        public void SellChips(int amount)
        {
            Chips -= amount;
        }

        public void CheckBet(Bet bet, Player player)
        {
            if (player.AvailableChips < bet.ChipsAmount)
            {
                throw new NotEnoughChipsException();
            }

            var diceCount = Dices.Count;

            if (bet.Number < 1 * diceCount || bet.Number > 6 * diceCount)
            {
                throw new NotValidBetNumberException();
            }

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