using Lidgren.Network;
using Model.Nova;

namespace Network.Messages.Nova
{
    public class RequestIntroducerIntroductionMessage : NovaMessage
    {
        public RequestIntroducerIntroductionMessage()
            : base((ushort) CustomMessageType.RequestIntroducerIntroduction)
        {
        }

        /// <summary>
        /// The client machine, the first of two machines, that the Introducer will introduce.
        /// </summary>
        public Machine ClientMachine { get; set; }

        /// <summary>
        /// The server machine, the second machine, that the Introducer will introduce.
        /// </summary>
        /// <remarks>The client only needs to set the 'NovaId' and 'PasswordHash' fields.</remarks>
        public Machine ServerMachine { get; set; }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            ClientMachine.WritePayload(message);
            ServerMachine.WritePayload(message);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            ClientMachine = new Machine();
            ClientMachine.ReadPayload(message);
            ServerMachine = new Machine();
            ServerMachine.ReadPayload(message);
        }
    }
}