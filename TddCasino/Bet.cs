namespace TddCasino
{
    public class Bet
    {
        public int ChipsAmount { get; private set; }
        public int Number { get; private set; }

        public Bet(int amount, int number)
        {
            ChipsAmount = amount;
            Number = number;
        }
    }
}