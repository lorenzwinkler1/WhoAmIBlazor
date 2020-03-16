using System;
using System.Collections.Generic;

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

        public event EventHandler PlayerAdded;
        public event EventHandler StateChanged;
    }
}
