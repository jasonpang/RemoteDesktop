using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Network;
using Nini.Config;

namespace Providers
{
    /// <summary>
    /// Sits under the Manager layer, directly on top of the network layer (in which messages are received), and exposes the lowest-level message event handlers as higher-level semi-functional events (i.e. three message event handlers may turn into one functional OnFileTransferCompleted event) for the Manager layer. See the Manager layer for what its responsibilities are. Note that the Provider layer doesn't create a custom event for each message - that would be too many and unnecessary. The Provider layer only creates custom events for messages which mean something (i.e. perhaps the fifth message in a sequence signifies a milestone of a file transfer in progress. Then the fifth message would invoke a custom event such as OnFileChecksumVerified. The first four messages would not expose events. This means both the Provider and Manager layer will have custom EventArgs. The EventArgs will be different since they expose different functions with different purposes. And if they should overlap, it is not a big deal.
    /// </summary>
    /// <remarks>REVISION: The Provider layer will not provide any Begin/End async method patterns and will not provide any AsTask() methods. It will only expose events such as OnChatMessageReceived and OnFileTransferProgress and OnFileTransferCompleted. It is up to the Manager layer to invent DownloadFileAsync() and DownloadFileAsTask() and SendChatMessage(). And we are no longer using the Begin/End async pattern anyways. It looks nice, but it is unnecessary extra work. The events exposed by the Provider layer should be just enough to implement the task methods.</remarks>
    public abstract class Provider
    {
        /// <summary>
        /// Gets the network all providers are bound to.
        /// </summary>
        public NetworkPeer Network { get; private set; }

        /// <summary>
        /// Provides access to common constants.
        /// </summary>
        public IConfig Config { get; private set; }

        protected Provider(NetworkPeer network)
        {
            Network = network;

            try
            {
                Config = new IniConfigSource(Application.StartupPath + "\\config.ini").Configs["NovaRat"];
            }
            catch (FileNotFoundException ex)
            {
                string defaultConfigText = "; Nova Remote Assistance Tool INI Configuration File" + Environment.NewLine +
                                                 "[NovaRat]" + Environment.NewLine +
                                                 "; Uncomment the line below for the public Introducer running on an Amazon EC2 server" + Environment.NewLine +
                                                 "; IntroducerEndPoint = 50.18.245.235:16168" + Environment.NewLine +
                                                 "; Comment the line below to stop using your own Introducer" + Environment.NewLine +
                                                 "IntroducerEndPoint = 127.0.0.1:16168" + Environment.NewLine +
                                                 "MaxNumConnectionAttemptsPerMachine = 3" + Environment.NewLine +
                                                 "BanTime = 60";

                File.WriteAllText(Application.StartupPath + "\\config.ini", defaultConfigText);

                Config = new IniConfigSource(Application.StartupPath + "\\config.ini").Configs["NovaRat"];
            }

            RegisterMessageHandlers(); 
        }

        public abstract void RegisterMessageHandlers();
    }
}
