﻿using System;
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

            var diceCount = Game.Croupier.Dices.Count;

            if (number < 1 * diceCount || number > 6 * diceCount)
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
            var lostChips = AllBets.Sum(x => x.ChipsAmount);
            AvailableChips -= lostChips;
            Game.Casino.TakeChips(lostChips);
        }

        public virtual void Win(Bet bet)
        {
            var coefficient = Game.Croupier.Dices.Count == 1
                ? 6
                : Game.Casino.GetWinCoefficient(bet.Number);
            
            AvailableChips += bet.ChipsAmount * coefficient;
        }
    }

}