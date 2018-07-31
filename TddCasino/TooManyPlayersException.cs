using System;

namespace TddCasino
{
    public class TooManyPlayersException : Exception
    {
        public override string Message => "Превышено количество игроков в игре";
    }
}