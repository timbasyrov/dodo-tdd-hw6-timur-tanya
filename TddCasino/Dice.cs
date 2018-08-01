using System;

namespace TddCasino
{
    public class Dice
    {
        public virtual int GetLuckyNumber()
        {
            return (new Random()).Next(1, 6);
        }
    }
}