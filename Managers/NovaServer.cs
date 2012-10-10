using System;
using Managers.LiveControl.Server;
using Managers.Nova.Server;
using Network;
using Network.Messages;
using Managers.Nova;

namespace Managers
{
    public class NovaServer
    {
        #region Singleton Design Pattern Implementation

        private static volatile NovaServer instance;
        private static readonly object syncRoot = new Object();

        public static NovaServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new NovaServer();
                    }
                }

                return instance;
            }
        }

        #endregion

        public NetworkServer Network { get; private set; }

        private NovaServer()
        {
            Network = new NetworkServer(0);

            NovaManager = new NovaManager(Network);
            LiveControlManager = new LiveControlManager(Network);
        }

        public NovaManager NovaManager { get; private set; }
        public LiveControlManager LiveControlManager { get; private set; }
    }
}