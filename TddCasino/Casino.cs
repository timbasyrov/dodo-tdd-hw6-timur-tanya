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
    }
}