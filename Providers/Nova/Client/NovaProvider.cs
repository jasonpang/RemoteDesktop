using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Network;
using Network.Messages.Nova;
using Providers.Nova.Modules;

namespace Providers.Nova.Client
{
    public class NovaProvider : Provider
    {
        public NovaProvider(NetworkPeer network)
            : base(network)
        {
        }

        public override void RegisterMessageHandlers()
        {
            Network.RegisterUnconnectedMessageHandler<ResponseIntroducerIntroductionCompletedMessage>(OnResponseIntroducerIntroductionCompletedMessageReceived);
            Network.OnNatTraversalSucceeded += new EventHandler<NatTraversedEventArgs>(Network_OnNatTraversalSucceeded);
        }

        private void OnResponseIntroducerIntroductionCompletedMessageReceived(UnconnectedMessageEventArgs<ResponseIntroducerIntroductionCompletedMessage> e)
        {
            if (OnIntroductionCompleted != null)
                OnIntroductionCompleted(this, new IntroducerIntroductionCompletedEventArgs { Result = e.Message.ResponseResult, DenyReason = e.Message.DenyReason });
        }

        void Network_OnNatTraversalSucceeded(object sender, NatTraversedEventArgs e)
        {
            Trace.WriteLine("Client:  Network_OnNatTraversalSucceeded()");

            lock (syncLock)
            {
                if (!alreadySent)
                {
                    alreadySent = true;

                    Trace.WriteLine("Client:  Network_OnNatTraversalSucceeded()  [LOCKED]");

                    alreadySent = true;
                    if (OnNatTraversalSucceeded != null)
                        OnNatTraversalSucceeded(this, new NatTraversedEventArgs { From = e.From });

                    Network.Connect((e.From));
                }
            }
        }

        // TODO: Refactor this
        private static object syncLock = new object();
        private static bool alreadySent = false;

        public event EventHandler<IntroducerIntroductionCompletedEventArgs> OnIntroductionCompleted;
        public event EventHandler<NatTraversedEventArgs> OnNatTraversalSucceeded;
    }
}
