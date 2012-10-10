using Lidgren.Network;
using Model.Nova;

namespace Network.Messages.Nova
{
    /// <summary>
    /// Requests the Introducer to register this Server, by passing a unique hardware ID.
    /// </summary>
    public class RequestIntroducerRegistrationMessage : NovaMessage
    {
        public RequestIntroducerRegistrationMessage()
            : base((ushort) CustomMessageType.RequestIntroducerRegistration)
        {
        }

        /// <summary>
        /// The machine for the Introducer to register
        /// </summary>
        public Machine Machine { get; set; }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            Machine.WritePayload(message);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Machine = new Machine();
            Machine.ReadPayload(message);
        }
    }
}