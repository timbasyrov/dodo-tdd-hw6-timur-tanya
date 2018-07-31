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
            if (Game != null)
            {
                throw new AlreadyInGameException();
            }

            Game = game;
            game.AddPlayer(this);
        }

        public void LeaveGame()
        {
            if (Game == null)
            {
                throw new NotInGameException();
            }

            Game = null;
        }
    }
}