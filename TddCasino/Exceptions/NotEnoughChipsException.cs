using System;

namespace TddCasino
{
    public class NotEnoughChipsException : Exception
    {
        public override string Message => "Недостаточно фишек для ставки";
    }
}