using System;

namespace TddCasino
{
    public class Casino
    {
        public Casino(int chipsAmount)
        {
            Chips = chipsAmount;
        }

        public int Chips { get; private set; }

        public void SellChips(int amount)
        {
            Chips -= amount;
        }

        public void CheckBet(Bet bet)
        {
            if (bet.ChipsAmount % 5 != 0)
            {
                throw new NotValidBetException();
            }
        }
    }
}