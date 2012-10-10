using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Network.Messages.LiveControl
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseEndScreenshotMessage : NovaMessage
    {
        public uint Number { get; set; }

        public ResponseEndScreenshotMessage()
            : base((ushort)CustomMessageType.ResponseEndScreenshotMessage)
        {
        }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write(Number);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Number = message.ReadUInt32();
        }
    }
}
