using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;
using Providers.LiveControl.Server;

namespace Managers.LiveControl.Server
{
    public class LiveControlManager : Manager<LiveControlProvider>
    {
        public LiveControlManager(NetworkPeer network)
            : base(new LiveControlProvider(network))
        {
        }
    }
}
