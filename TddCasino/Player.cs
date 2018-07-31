using System;

namespace TddCasino
{
    public class Player
    {
        public Game Game { get; private set; }

        public int AvailableChips { get; private set; }

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

        public void BuyChips(Casino casino, int chipsAmount)
        {
            casino.SellChips(chipsAmount);
            AvailableChips += chipsAmount;
        }

        public void MakeBet(int chipsAmount, int number)
        {
            var bet = new Bet(chipsAmount, number);
            AvailableChips -= chipsAmount;
            Game.AcceptBet(this, bet);
        }
    }

}