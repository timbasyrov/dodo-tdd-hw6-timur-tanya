using System;

namespace TddCasino
{
    public class AlreadyInGameException : Exception
    {
        public override string Message => "Игрок уже в игре";
    }
}