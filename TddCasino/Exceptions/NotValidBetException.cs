using System;

namespace TddCasino
{
    public class NotValidBetException : Exception
    {
        public override string Message => "Ставка должна быть кратной 5";
    }
}