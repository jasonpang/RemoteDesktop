using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Network
{
    public class ConnectedEventArgs : EventArgs
    {
        public IPEndPoint From
        {
            get;
            set;
        }
    }
}
