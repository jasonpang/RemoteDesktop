using System;
using Network;
using Providers.Nova.Introducer;
using Providers.Nova.Modules;

namespace Managers.Nova.Introducer
{
    public class NovaManager : Manager<NovaProvider>
    {
        public NovaManager(NetworkPeer network)
            : base(new NovaProvider(network))
        {
            Provider.OnMachineRegistered += (s, e) => { if (OnMachineRegistered != null) OnMachineRegistered(s, e); };
            Provider.OnIntroductionCompleted += (s, e) => { if (OnIntroductionCompleted != null) OnIntroductionCompleted(s, e); };
            Provider.OnWrongPassword += (s, e) => { if (OnWrongPassword != null) OnWrongPassword(s, e); };
            Provider.OnMachineBanned += (s, e) => { if (OnMachineBanned != null) OnMachineBanned(s, e); };
            Provider.OnKeepAliveReceived += (s, e) => {if (OnKeepAliveReceived != null) OnKeepAliveReceived(s, e); };
        }

        /// <summary>
        /// Occurs when a machine has been newly registered with the Introducer.
        /// </summary>
        public event EventHandler<MachineRegisteredEventArgs> OnMachineRegistered;

        /// <summary>
        /// Occurs after a completed introduction between the Client and Server.
        /// </summary>
        public event EventHandler<IntroductionCompletedEventArgs> OnIntroductionCompleted;

        /// <summary>
        /// Occurs when a client machine has been banned from requesting any more introductions (for the value defined in config.ini).
        /// </summary>
        public event EventHandler<MachineBannedEventArgs> OnMachineBanned;

        /// <summary>
        /// Occurs when a client machine has requested an introduction with the wrong server password (and has been denied).
        /// </summary>
        public event EventHandler<WrongPasswordEventArgs> OnWrongPassword;

        public event EventHandler<EventArgs> OnKeepAliveReceived;

        public void Restart()
        {
            Network.Start();
        }

        public void Stop()
        {
            Network.Shutdown();
        }
    }
}
