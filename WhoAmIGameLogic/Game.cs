using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace WhoAmIGameLogic
{
    public enum GameState
    {
        Idle,
        Started,
        Finished
    }
    public class Game : INotifyStateChanged
    {
        public Game()
        {
            this.Chat = new Chat();
            this.Chat.StateChanged += (obj, ev) => StateChanged?.Invoke(obj, ev);
        }
        public GameState GameState = GameState.Idle;
        public Chat Chat { get; private set; }
        public Round Round { get; private set; }

        public void AddPlayer(Player player)
        {
            if (this.GameState == GameState.Idle)
            {
                this._players.Add(player);
                player.StateChanged += (obj, ev) =>
                {
                    StateChanged?.Invoke(obj, ev);
                };
                this.PlayerAdded?.Invoke(player, EventArgs.Empty);
                this.StateChanged?.Invoke(player, EventArgs.Empty);
            }
            else
            {
                throw new InvalidOperationException("Game already started");
            }
        }
        private List<Player> _players = new List<Player>();
        public IEnumerable<Player> Players { get => this._players; }

        public async Task StartGame()
        {
            GameState = GameState.Started;
            GameStarted?.Invoke(null, EventArgs.Empty);


            while (!AllPlayersFinished())
            {
                foreach (var player in this._players)
                {
                    do
                    {
                        this.Round = new Round()
                        {
                            CurrentPlayer = player,
                            PlayerCount = _players.Count,
                            TimeRemaining = 20,
                        };

                        while (this.Round.TimeRemaining > 0 && !this.Round.RoundFinished)
                        {
                            await Task.Delay(1000);
                            this.Round.TimeRemaining -= 1;
                        }



                        player.Guesses.Add(new Tuple<string, bool>(this.Round.Question, this.Round.PlayerAnswers.Item1.Count > this.Round.PlayerAnswers.Item2.Count));
                    } while (this.Round.PlayerAnswers.Item1.Count > this.Round.PlayerAnswers.Item2.Count);
                }
            }



        }

        private bool AllPlayersFinished()
        {
            return Players.Any((player) =>
            {
                return !player.CorrectGuess;
            });
        }

        public Round CurrentRount { get; private set; }

        public event EventHandler GameStarted;
        public event EventHandler PlayerAdded;
        public event EventHandler StateChanged;
    }
}
