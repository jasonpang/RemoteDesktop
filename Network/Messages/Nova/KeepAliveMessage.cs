using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Network.Messages.Nova
{
    public class KeepAliveMessage : NovaMessage
    {
        public KeepAliveMessage()
            : base((ushort) CustomMessageType.KeepAliveMessage)
        {
        }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
        }

        /// <summary>
        /// Reads the payload.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
        }
    }
}
