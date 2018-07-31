using System;

namespace TddCasino
{
    public class Player
    {
        public Game Game { get; private set; }

        public Player()
        {
        }

        public void JoinGame(Game game)
        {
            this.Game = game;
        }

        public void LeaveGame()
        {
            if (this.Game == null)
            {
                throw new NotInGameException();
            }

            this.Game = null;
        }
    }
}