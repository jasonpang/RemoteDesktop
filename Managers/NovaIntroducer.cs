using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managers.Nova.Introducer;
using Network;
using Managers.Nova;

namespace Managers
{
    public class NovaIntroducer
    {
        #region Singleton Design Pattern Implementation

        private static volatile NovaIntroducer instance;
        private static readonly object syncRoot = new Object();

        public static NovaIntroducer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new NovaIntroducer();
                    }
                }

                return instance;
            }
        }

        #endregion

        public NetworkServer Network { get; private set; }

        private NovaIntroducer()
        {
            Network = new NetworkServer(16168);
            NovaManager = new NovaManager(Network);            
        }

        public NovaManager NovaManager { get; private set; }
    }
}
