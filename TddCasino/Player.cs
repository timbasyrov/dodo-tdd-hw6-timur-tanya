using System.Collections.Generic;
using System.Linq;

namespace TddCasino
{
    public class Player
    {
        private readonly List<Bet> _allBets = new List<Bet>();

        public Game Game { get; private set; }

        public int AvailableChips { get; private set; }

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
            Game.SellChips(chipsAmount);
            AvailableChips += chipsAmount;
        }

        public void MakeBet(int chipsAmount, int number)
        {
            var bet = new Bet(chipsAmount, number);

            Game.CheckBet(bet, this);

            AvailableChips -= chipsAmount;
            _allBets.Add(bet);
        }

        public int GetChipsFromAllBets()
        {
            return _allBets.Sum(x => x.ChipsAmount);
        }

        public virtual void LoseChips(int count)
        {
            AvailableChips -= count;
        }

        public virtual void WinChips(int count)
        {
            AvailableChips += count;
        }

        public Bet FindBet(int luckyNumber)
        {
            return _allBets.FirstOrDefault(x => x.Number == luckyNumber);
        }

        public int GetBetsCount()
        {
            return _allBets.Count;
        }
    }
}