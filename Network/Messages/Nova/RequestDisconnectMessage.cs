using Lidgren.Network;

namespace Network.Messages.Nova
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestDisconnectMessage : NovaMessage
    {
        public enum DisconnectReason
        {
            /// <summary>
            /// Either the client or server end-user closed the program.
            /// </summary>
            UserInitiated,
        }

        public RequestDisconnectMessage()
            : base((ushort) CustomMessageType.RequestDisconnection)
        {
        }

        public DisconnectReason Reason { get; set; }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write((byte) Reason);
        }

        /// <summary>
        /// Reads the payload.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Reason = (DisconnectReason) message.ReadByte();
        }
    }
}