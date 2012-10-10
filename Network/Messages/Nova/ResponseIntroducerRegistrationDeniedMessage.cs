using Lidgren.Network;

namespace Network.Messages.Nova
{
    /// <summary>
    /// A response by the Introducer to inform the Server that it denied its registration request.
    /// </summary>
    public class ResponseIntroducerRegistrationDeniedMessage : NovaMessage
    {
        public enum DenyReason
        {
            /// <summary>
            /// The Server is already registered. Did you forget to send Unregister after a successful session?
            /// </summary>
            AlreadyRegistered,

            /// <summary>
            /// No idea why a Server would be banned (usually a Client is banned from connecting to a Server), but it's a possibility.
            /// </summary>
            Banned,
        }

        public ResponseIntroducerRegistrationDeniedMessage()
            : base((ushort) CustomMessageType.ResponseIntroducerRegistrationDenied)
        {
        }

        public DenyReason Reason { get; set; }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write((byte) Reason);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Reason = (DenyReason) message.ReadByte();
        }
    }
}