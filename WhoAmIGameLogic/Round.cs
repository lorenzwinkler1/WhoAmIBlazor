using System;
using System.Collections;
using System.Collections.Generic;

namespace WhoAmIGameLogic
{
    public enum QuestionTypes
    {
        Question,
        Guess
    }
    public class Round : INotifyStateChanged
    {
        private Player _currentPlayer;
        private string _question;
        private QuestionTypes _qtype = QuestionTypes.Question;
        private int _timeremaining;

        public bool RoundFinished { get; private set; }
        public int TimeRemaining {
            get {
                return _timeremaining;
            }
            set {
                _timeremaining = value;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Round()
        {
            this.PlayerAnswers = new Tuple<HashSet<Player>, HashSet<Player>>(new HashSet<Player>(), new HashSet<Player>());


        }

        public int PlayerCount { get; set; }
        public Player CurrentPlayer {
            get {
                return _currentPlayer;
            }
            set {
                this._currentPlayer = value;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string Question {
            get {
                return _question;
            }
            set {
                this._question = value;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public QuestionTypes QType {
            get {
                return _qtype;
            }
            set {
                this._qtype = value;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Tuple<HashSet<Player>, HashSet<Player>> PlayerAnswers { get; set; }

        public void Answer(Player player, bool answer)
        {
            if (player == _currentPlayer)
                return;

            var item = answer ? PlayerAnswers.Item1 : PlayerAnswers.Item2;
            var item1 = answer ? PlayerAnswers.Item2 : PlayerAnswers.Item1;

            if (!item.Contains(player))
            {
                item.Add(player);
            }
            if (item1.Contains(player))
            {
                item1.Remove(player);
            }
            if (item1.Count > ((double)PlayerCount - 1) / 2 || item.Count > ((double)PlayerCount - 1) / 2)
                RoundFinished = true;
            StateChanged?.Invoke(this, EventArgs.Empty);

        }


        public event EventHandler StateChanged;
    }
}