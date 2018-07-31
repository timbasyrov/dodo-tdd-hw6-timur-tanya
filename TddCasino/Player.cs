using System;
using System.Collections.Generic;

namespace TddCasino
{
    public class Player
    {
        public List<Bet> AllBets = new List<Bet>();

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
            if (AvailableChips < chipsAmount)
            {
                throw new NotEnoughChipsException();
            }

            var bet = new Bet(chipsAmount, number);
            AvailableChips -= chipsAmount;
            AllBets.Add(bet);
        }
    }

}