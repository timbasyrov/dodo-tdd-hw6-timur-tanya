using System;

namespace TddCasino
{
    public class NotValidBetNumberException : Exception
    {
        public override string Message => "Недопустимый номер ставки";
    }
}