using System;
using System.Collections.Generic;
using System.Text;

namespace WhoAmIGameLogic
{
    public class Chat : INotifyStateChanged
    {
        private List<Tuple<Player, string>> messages = new List<Tuple<Player, string>>();
        public IEnumerable<Tuple<Player, string>> Messages { get => messages; }

        public event EventHandler StateChanged;

        public void AddMessage(Player player, string message)
        {
            messages.Insert(0, new Tuple<Player, string>(player, message));
            StateChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
