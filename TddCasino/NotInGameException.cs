using System;

namespace TddCasino
{
    public class NotInGameException : Exception
    {
        public override string Message => "Игрок не в игре";
    }
}