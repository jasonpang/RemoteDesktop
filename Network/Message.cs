using System;
using Lidgren.Network;
using Network.Extensions;

namespace Network
{
    /// <remarks>
    /// Extends Lidgren's networking library by implementing custom messages.
    /// </remarks>
    public abstract class Message
    {
        /// <summary>
        /// Gets the unique identifier for this custom message type within its protocol.
        /// </summary>
        public ushort MessageType { get; private set; }

        /// <summary>
        /// Gets the protocol this message belongs to.
        /// </summary>
        public Protocol Protocol { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="protocol">The protocol this message belongs to.</param>
        /// <param name="messageType">Unique identifer for this custom message type.</param>
        protected Message(Protocol protocol, ushort messageType)
        {
            if (protocol == null)
                throw new ArgumentNullException("protocol");

            Protocol = protocol;
            MessageType = messageType;
        }

        public virtual void WritePayload(NetOutgoingMessage message)
        {
            message.Write(Protocol);
            message.Write(MessageType);
        }

        public virtual void ReadPayload(NetIncomingMessage message)
        {
        }
    }
}