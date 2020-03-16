using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoAmIGameLogic;

namespace WhoAmIBlazor.GameManagment
{
    interface IGameManager
    {
        public string Name { get; set; }

        public event EventHandler NameChanged;

        public Game GetGame(string id);

        public string CreateGame();
    }
}
