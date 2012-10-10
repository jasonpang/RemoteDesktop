using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Model;
using Model.Nova;
using Network;
using Network.Messages.Nova;
using Providers.Nova.Modules;

namespace Providers.Nova.Introducer
{
    public class NovaProvider : Provider
    {
        public MachineLookupTable MachineLookupTable { get; set; }
        public MachineBanTable MachineBanTable { get; set; }

        public int NumMachinesRegistered { get; set; }
        public int NumMachinesIntroduced { get; set; }
        public int NumMachinesActive { get; set; }

        public NovaProvider(NetworkPeer network)
            : base(network)
        {
            MachineLookupTable = new MachineLookupTable();
            MachineBanTable = new MachineBanTable();
        }

        public override void RegisterMessageHandlers()
        {
            Network.RegisterUnconnectedMessageHandler<RequestIntroducerRegistrationMessage>(OnRequestIntroducerRegistrationMessageReceived);
            Network.RegisterUnconnectedMessageHandler<RequestIntroducerIntroductionMessage>(OnRequestIntroducerIntroductionMessageReceived);
            Network.RegisterUnconnectedMessageHandler<KeepAliveMessage>(OnKeepAliveMessageReceived);
        }

        private void OnKeepAliveMessageReceived(UnconnectedMessageEventArgs<KeepAliveMessage> e)
        {
            if (OnKeepAliveReceived != null) OnKeepAliveReceived(this, null);
        }

        private void OnRequestIntroducerRegistrationMessageReceived(UnconnectedMessageEventArgs<RequestIntroducerRegistrationMessage> e)
        {
            string novaId = String.Empty;
            do
            {
                novaId = AlphaNumericGenerator.Generate(3, GeneratorOptions.AlphaNumeric);
            } while (MachineLookupTable.IdExists(novaId));

            e.Message.Machine.NovaId = novaId;
            e.Message.Machine.PublicEndPoint = e.From; // For introduction, one method below
            MachineLookupTable.Add(novaId, new LookupMachine(e.Message.Machine));

            NumMachinesRegistered++;

            if (OnMachineRegistered != null)
                OnMachineRegistered(this, new MachineRegisteredEventArgs { Machine = e.Message.Machine });

            Network.SendUnconnectedMessage(new ResponseIntroducerRegistrationSucceededMessage { Machine = e.Message.Machine }, e.From);
        }

        private void OnRequestIntroducerIntroductionMessageReceived(UnconnectedMessageEventArgs<RequestIntroducerIntroductionMessage> e)
        {
            // First assign the client machine (the sender) its IPEndPoint, for introducting, later, or for the ban
            e.Message.ClientMachine.PublicEndPoint = e.From;

            // We want to introduce this machine with the target server machine
            // First, we check if this client machine is banned
            if (MachineBanTable.IdExists(e.Message.ClientMachine))
            {
                // It is banned - get the amount of time left
                DateTime paroleDateTime = MachineBanTable.Get(e.Message.ClientMachine);

                // Client is still banned
                if (DateTime.Now < paroleDateTime)
                {
                    // Tell him bye bye
                    Network.SendUnconnectedMessage(
                        new ResponseIntroducerIntroductionCompletedMessage { ResponseResult = ResponseIntroducerIntroductionCompletedMessage.Result.Denied, DenyReason = ResponseIntroducerIntroductionCompletedMessage.Reason.Banned },
                        e.From);
                    return;
                }
                else
                {
                    // Ban time is up - client is free to reconnect again
                    MachineBanTable.Remove(e.Message.ClientMachine);
                }
            }

            // Client is not banned, so we check the password hash to make sure it matches the server's
            var retrievedServerMachine = MachineLookupTable.GetMachineById(e.Message.ServerMachine.NovaId);
            string correctPasswordHash = retrievedServerMachine.Machine.PasswordHash;

            // Password hash doesn't match - client entered wrong password
            if (e.Message.ServerMachine.PasswordHash != correctPasswordHash)
            {
                // Tell him bye bye
                Network.SendUnconnectedMessage(
                    new ResponseIntroducerIntroductionCompletedMessage { ResponseResult = ResponseIntroducerIntroductionCompletedMessage.Result.Denied, DenyReason = ResponseIntroducerIntroductionCompletedMessage.Reason.WrongPassword },
                    e.From);

                retrievedServerMachine.NumConnectionAttempts++;

                if (OnWrongPassword != null)
                    OnWrongPassword(this, new WrongPasswordEventArgs() { NumConnectionAttempts = retrievedServerMachine.NumConnectionAttempts, OffendingMachine = e.Message.ClientMachine, TargetMachine = retrievedServerMachine.Machine });

                // Number of client connection attempts exceeds allowed value
                if (retrievedServerMachine.NumConnectionAttempts > Config.GetInt("MaxNumConnectionAttemptsPerMachine", 3))
                {
                    // Banned :D
                    MachineBanTable.Add(e.Message.ClientMachine, DateTime.Now.AddSeconds(Config.GetDouble("BanTime", 60.0)));

                    if (OnMachineBanned != null)
                        OnMachineBanned(this, new MachineBannedEventArgs() { BannedMachine = e.Message.ClientMachine, NumConnectionAttempts = retrievedServerMachine.NumConnectionAttempts });
                }
            }
            else
            {
                // If the password hash is correct, introduce the two machines
                // First we need to set the private and public endpoints of the Client and Server

                Network.Introduce(e.Message.ClientMachine, retrievedServerMachine.Machine);

                NumMachinesIntroduced++;

                Network.SendUnconnectedMessage(
                    new ResponseIntroducerIntroductionCompletedMessage { ResponseResult = ResponseIntroducerIntroductionCompletedMessage.Result.Allowed },
                    e.From);

                if (OnIntroductionCompleted != null)
                    OnIntroductionCompleted(this,
                                            new IntroductionCompletedEventArgs { ClientMachine = e.Message.ClientMachine, ServerMachine = retrievedServerMachine.Machine });
            }
        }

        public event EventHandler<MachineRegisteredEventArgs> OnMachineRegistered;
        public event EventHandler<IntroductionCompletedEventArgs> OnIntroductionCompleted;
        public event EventHandler<MachineBannedEventArgs> OnMachineBanned;
        public event EventHandler<WrongPasswordEventArgs> OnWrongPassword;
        public event EventHandler<EventArgs> OnKeepAliveReceived;
    }
}
