using System.Collections.Generic;
using System.Linq;

namespace TddCasino
{
    public class Croupier
    {
        public List<Dice> Dices { get; private set; }

        public Croupier(int diceCount)
        {
            Dices = new List<Dice>();

            for (int i = 0; i < diceCount; i++)
            {
                Dices.Add(new Dice());
            }
        }

        public virtual int RollDices()
        {
            return Dices.Sum(x => x.GetLuckyNumber());
        }
    }
}