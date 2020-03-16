using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoAmIGameLogic;

namespace WhoAmIBlazor.GameManagment
{
    public class GameManager : IGameManager
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        private Dictionary<string, Game> Games { get; set; } = new Dictionary<string, Game>();

        private string _name;
        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
                NameChanged?.Invoke(this.Name, EventArgs.Empty);
            }
        }

        public event EventHandler NameChanged;

        private static Game gameInstance = new Game();

        public Game GetGame(string id)
        {
            if (this.Games.ContainsKey(id))
            {
                return Games[id];
            }
            else
            {
                return null;
            }
        }

        public string CreateGame()
        {
            string key = "";
            do
            {
                key = GenerateRandomString(8);
            } while (this.Games.ContainsKey(key));

            this.Games.Add(key, new Game()
            {

            });
            return key;
        }

        public static string GenerateRandomString(int length)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWQYZ1234567890";

            string generatedString = "";
            for (int i = 0; i < length; i++)
            {
                generatedString += allowedChars[random.Next(0, allowedChars.Length)];
            }
            return generatedString;
        }
    }
    public static class StorageKeys
    {
        public const string PlayerId = "player_id";
    }
}
