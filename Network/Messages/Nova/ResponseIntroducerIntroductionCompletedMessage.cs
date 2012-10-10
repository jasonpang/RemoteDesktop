using Lidgren.Network;

namespace Network.Messages.Nova
{
    /// <summary>
    /// Request for the Introducer to introduce two machines was denied (probably because of incorrect password).
    /// </summary>
    public class ResponseIntroducerIntroductionCompletedMessage : NovaMessage
    {
        public enum Result
        {
            Denied,
            Allowed,
        }

        public enum Reason
        {
            /// <summary>
            /// The client end-user provided the wrong server's password.
            /// </summary>
            WrongPassword,

            /// <summary>
            /// The client is banned for repeated connection attempts.
            /// </summary>
            Banned,
        }

        public Result ResponseResult { get; set; }
        public Reason DenyReason { get; set; }

        public ResponseIntroducerIntroductionCompletedMessage()
            : base((ushort)CustomMessageType.ResponseIntroducerIntroductionCompleted)
        {
        }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write((byte)ResponseResult);
            message.Write((byte)DenyReason);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            ResponseResult = (Result)message.ReadByte();
            DenyReason = (Reason) message.ReadByte();
        }
    }
}