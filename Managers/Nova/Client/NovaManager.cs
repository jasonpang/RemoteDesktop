using System;
using Network;
using Model.Nova;
using Network.Utilities;
using Network.Messages.Nova;
using Model.Extensions;
using System.Threading.Tasks;
using Providers.Nova.Client;
using Providers.Nova.Modules;

namespace Managers.Nova.Client
{
    public class NovaManager : Manager<NovaProvider>
    {
        public NovaManager(NetworkPeer network)
            : base(new NovaProvider(network))
        {
            Network.OnConnected += (s, e) => { if (OnConnected != null) OnConnected(s, e); };
            Provider.OnIntroductionCompleted += (s, e) => { if (OnIntroductionCompleted != null) OnIntroductionCompleted(s, e); };
            Provider.OnNatTraversalSucceeded += (s, e) => { if (OnNatTraversalSucceeded != null) OnNatTraversalSucceeded(s, e); };
        }

        // TODO: Redo this
        public Task<IntroducerIntroductionCompletedEventArgs> RequestIntroductionAsTask(string novaId, string password)
        {
            var tcs = RegisterAsTask<IntroducerIntroductionCompletedEventArgs>(ref OnIntroductionCompleted);

            var childTask = Task.Factory.StartNew(() =>
            {
                var clientMachine = new Machine {Identity = MachineIdentity.GetCurrentIdentity(), PrivateEndPoint = Network.GetInternalEndPoint()};

                var serverMachine = new Machine {NovaId = novaId, PasswordHash = HashUtil.ComputeHash(password, HashType.SHA512)};

                Network.SendUnconnectedMessage(new RequestIntroducerIntroductionMessage { ClientMachine = clientMachine, ServerMachine = serverMachine },
                                              Config.GetIPEndPoint("IntroducerEndPoint"));
            }, TaskCreationOptions.AttachedToParent);

            return tcs.Task;
        }

        public event EventHandler<ConnectedEventArgs> OnConnected;
        public event EventHandler<IntroducerIntroductionCompletedEventArgs> OnIntroductionCompleted;
        public event EventHandler<NatTraversedEventArgs> OnNatTraversalSucceeded;
    }
}
