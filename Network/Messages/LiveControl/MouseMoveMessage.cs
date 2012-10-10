using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Network.Messages.LiveControl
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseMoveMessage : NovaMessage
    {
        public MouseMoveMessage()
            : base((ushort)CustomMessageType.MouseMoveMessage)
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
