using System;
using System.Diagnostics;
using Network;
using Model.Nova;
using Model;
using Providers.Nova.Modules;
using Network.Utilities;
using System.Threading.Tasks;
using Network.Messages.Nova;
using Model.Extensions;
using Providers.Nova.Server;

namespace Managers.Nova.Server
{
    public class NovaManager : Manager<NovaProvider>
    {
        public NovaManager(NetworkPeer network)
            : base(new NovaProvider(network))
        {
            Provider.OnIntroducerRegistrationResponded += (s, e) =>
            {
                if (OnIntroducerRegistrationResponded != null)
                    OnIntroducerRegistrationResponded(s, e);
            };
        }

        public async Task<PasswordGeneratedEventArgs> GeneratePassword()
        {
            return await GeneratePasswordAsTask();
        }

        public Task<PasswordGeneratedEventArgs> GeneratePasswordAsTask()
        {
            Trace.WriteLine("Ran");
            var tcs = RegisterAsTask<PasswordGeneratedEventArgs>(ref OnNewPasswordGenerated);

            var childTask = Task<PasswordGeneratedEventArgs>.Factory.StartNew(() =>
            {
                Provider.ServerMachine.Identity = MachineIdentity.GetCurrentIdentity();
                Provider.ServerMachine.Password = AlphaNumericGenerator.Generate(2, GeneratorOptions.Numeric);
                Provider.ServerMachine.PasswordHash = HashUtil.ComputeHash(Provider.ServerMachine.Password, HashType.SHA512);

                var newPasswordEventArgs = new PasswordGeneratedEventArgs { NewPassword = Provider.ServerMachine.Password };

                if (OnNewPasswordGenerated != null)
                    OnNewPasswordGenerated(this, newPasswordEventArgs);

                return newPasswordEventArgs;
            }, TaskCreationOptions.AttachedToParent);

            return tcs.Task;
        }

        public Task<IntroducerRegistrationResponsedEventArgs> RegisterWithIntroducerAsTask()
        {
            var tcs = RegisterAsTask<IntroducerRegistrationResponsedEventArgs>(ref OnIntroducerRegistrationResponded);

            // We create a new machine instead of sending ServerMachine because ServerMachine has the plain text password and is a local copy only
            var serverMachine = new Machine
            {
                Identity = Provider.ServerMachine.Identity,
                PasswordHash = Provider.ServerMachine.PasswordHash,
                PrivateEndPoint = Network.GetInternalEndPoint()
            };
            
            Network.SendUnconnectedMessage(new RequestIntroducerRegistrationMessage { Machine = serverMachine }, Config.GetIPEndPoint("IntroducerEndPoint"));

            return tcs.Task;
        }

        public void SendKeepAliveMessage()
        {
            Network.SendUnconnectedMessage(new KeepAliveMessage(), Config.GetIPEndPoint("IntroducerEndPoint"));
        }

        public event EventHandler<PasswordGeneratedEventArgs> OnNewPasswordGenerated;
        public event EventHandler<IntroducerRegistrationResponsedEventArgs> OnIntroducerRegistrationResponded;
    }
}
