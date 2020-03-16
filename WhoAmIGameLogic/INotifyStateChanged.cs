using System;
using System.Collections.Generic;
using System.Text;

namespace WhoAmIGameLogic
{
    public interface INotifyStateChanged
    {
        public event EventHandler StateChanged;
    }
}
