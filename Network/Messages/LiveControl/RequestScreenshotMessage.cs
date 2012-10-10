using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Network.Messages.LiveControl
{
    public class RequestScreenshotMessage : NovaMessage
    {
        public RequestScreenshotMessage()
            : base((ushort) CustomMessageType.RequestScreenshotMessage)
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
