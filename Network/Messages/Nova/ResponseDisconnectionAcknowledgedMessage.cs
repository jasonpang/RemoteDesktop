using Lidgren.Network;

namespace Network.Messages.Nova
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseDisconnectionAcknowledgedMessage : NovaMessage
    {
        public ResponseDisconnectionAcknowledgedMessage()
            : base((ushort) CustomMessageType.ResponseDisconnectionAcknowledged)
        {
        }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
        }
    }
}