using Lidgren.Network;
using Model.Nova;

namespace Network.Messages.Nova
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseIntroducerRegistrationSucceededMessage : NovaMessage
    {
        public ResponseIntroducerRegistrationSucceededMessage()
            : base((ushort) CustomMessageType.ResponseIntroducerRegistrationSucceeded)
        {
        }

        /// <summary>
        /// Server can read the newly generated ID property off this field.
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