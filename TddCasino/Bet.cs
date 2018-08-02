namespace TddCasino
{
    public class Bet
    {
        public int ChipsAmount { get; }
        public int Number { get; }

        public Bet(int amount, int number)
        {
            ChipsAmount = amount;
            Number = number;
        }
    }
}