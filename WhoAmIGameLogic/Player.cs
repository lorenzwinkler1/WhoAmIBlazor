using System;
using System.Collections.Generic;

namespace WhoAmIGameLogic
{
    public class Player : INotifyStateChanged
    {
        private Dictionary<string, List<Player>> roleProposals = new Dictionary<string, List<Player>>();
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public string PlayerId { get; set; }

        public IEnumerable<KeyValuePair<string, List<Player>>> RoleProposals { get => roleProposals; }

        public void ProposeRole(string proposal, Player issuer)
        {
            if (roleProposals.ContainsKey(proposal))
            {
                if (!roleProposals[proposal].Contains(issuer))
                {
                    roleProposals[proposal].Add(issuer);
                }
            }
            else
            {
                roleProposals.Add(proposal, new List<Player>()
                {
                    issuer
                });
            }
            foreach (var prop in roleProposals)
            {
                if (prop.Value.Contains(issuer) && prop.Key != proposal)
                {
                    prop.Value.Remove(issuer);
                }
            }

            StateChanged?.Invoke(null, EventArgs.Empty);
        }


        public event EventHandler StateChanged;
    }
}