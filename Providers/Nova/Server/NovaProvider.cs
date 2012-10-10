using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Model.Nova;
using Network;
using Network.Messages.Nova;
using Providers.Nova.Modules;

namespace Providers.Nova.Server
{
    public class NovaProvider : Provider
    {
        /// <summary>
        /// Represents the current server's machine, used to store the current session password.
        /// </summary>
        public Machine ServerMachine { get; set; }

        public NovaProvider(NetworkPeer network)
            : base(network)
        {
            ServerMachine = new Machine();
        }

        public override void RegisterMessageHandlers()
        {
            Network.RegisterUnconnectedMessageHandler<ResponseIntroducerRegistrationDeniedMessage>(OnResponseIntroducerRegistrationDeniedMessageReceived);
            Network.RegisterUnconnectedMessageHandler<ResponseIntroducerRegistrationSucceededMessage>(OnResponseIntroducerRegistrationSucceededMessageReceived);
            Network.OnConnected += new EventHandler<ConnectedEventArgs>(Network_OnConnected);
            Network.OnNatTraversalSucceeded += new EventHandler<NatTraversedEventArgs>(Network_OnNatTraversalSucceeded);
        }

        private static object syncLock = new object();
        private static bool alreadySent = false;
        
        void Network_OnNatTraversalSucceeded(object sender, NatTraversedEventArgs e)
        {
            Trace.WriteLine("Server:  Network_OnNatTraversalSucceeded()");

            lock (syncLock)
            {
                if (!alreadySent)
                {
                    alreadySent = true;

                    Trace.WriteLine("Server:  Network_OnNatTraversalSucceeded()  [Final Time]");

                    alreadySent = true;

                    Network.Connect((e.From));
                }
            }
        }

        private void OnResponseIntroducerRegistrationDeniedMessageReceived(UnconnectedMessageEventArgs<ResponseIntroducerRegistrationDeniedMessage> e)
        {
            if (OnIntroducerRegistrationResponded != null)
                OnIntroducerRegistrationResponded(this,
                                                  new IntroducerRegistrationResponsedEventArgs { RegistrationResult = IntroducerRegistrationResponsedEventArgs.Result.Denied });
        }

        private void OnResponseIntroducerRegistrationSucceededMessageReceived(UnconnectedMessageEventArgs<ResponseIntroducerRegistrationSucceededMessage> e)
        {
            if (OnIntroducerRegistrationResponded != null)
                OnIntroducerRegistrationResponded(this,
                                                  new IntroducerRegistrationResponsedEventArgs
                                                  {
                                                      RegistrationResult = IntroducerRegistrationResponsedEventArgs.Result.Succeeded,
                                                      NovaId = e.Message.Machine.NovaId
                                                  });

            // Client will request NAT introduction, which leads to Server_OnNatTraversalSuccess() in the Client code block
        }

        // Warning - this event can be called multiple times
        void Network_OnConnected(object sender, ConnectedEventArgs e)
        {
            Trace.WriteLine("Server:  Network_OnConnected()");

            lock (syncLock2)
            {
                if (!alreadySent2)
                {
                    alreadySent2 = true;

                    Trace.WriteLine("Server:  Network_OnConnected()  [Final Time]");
                }
            }
        }

        private static object syncLock2 = new object();
        private static bool alreadySent2 = false;

        public event EventHandler<IntroducerRegistrationResponsedEventArgs> OnIntroducerRegistrationResponded;
    }
}
