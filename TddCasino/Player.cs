using System;
using System.Collections.Generic;
using System.Linq;

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

        public void BuyChips(int chipsAmount)
        {
            Game.Casino.SellChips(chipsAmount);
            AvailableChips += chipsAmount;
        }

        public void MakeBet(int chipsAmount, int number)
        {
            if (AvailableChips < chipsAmount)
            {
                throw new NotEnoughChipsException();
            }

            if (number < 1 || number > 6)
            {
                throw new NotValidBetNumberException();
            }

            var bet = new Bet(chipsAmount, number);

            Game.Casino.CheckBet(bet);

            AvailableChips -= chipsAmount;
            AllBets.Add(bet);
        }

        public virtual void Lose()
        {
            AvailableChips -= AllBets.Sum(x => x.ChipsAmount);
        }

        public void Win(int chipsAmount)
        {
            AvailableChips += chipsAmount * 6;
        }
    }

}