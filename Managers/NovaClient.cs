using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managers.LiveControl.Client;
using Managers.Nova;
using Managers.Nova.Client;
using Network;

namespace Managers
{
    public class NovaClient
    {
        #region Singleton Design Pattern Implementation

        private static volatile NovaClient instance;
        private static readonly object syncRoot = new Object();

        public static NovaClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new NovaClient();
                    }
                }

                return instance;
            }
        }

        #endregion

        public NetworkClient Network { get; private set; }

        private NovaClient()
        {
            Network = new NetworkClient();

            NovaManager = new NovaManager(Network);
            LiveControlManager = new LiveControlManager(Network);
        }

        public NovaManager NovaManager { get; private set; }
        public LiveControlManager LiveControlManager { get; private set; }
    }
}
