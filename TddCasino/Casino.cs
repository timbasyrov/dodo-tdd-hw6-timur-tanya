using System;
using System.Collections.Generic;

namespace TddCasino
{
    public class Casino
    {
        private Dictionary<int, int> _winCoefficients = new Dictionary<int, int>()
        {
            {2, 36}, {3, 18}, {4, 12}, {5, 9}, {6, 7}, {7, 6}, {8, 7}, {9, 9}, {10, 12}, {11, 18}, {12, 36}
        };

        public Casino(int chipsAmount)
        {
            Chips = chipsAmount;
        }

        public int Chips { get; private set; }

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
    }
}