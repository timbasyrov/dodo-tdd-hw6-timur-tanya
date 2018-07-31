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
    }
}