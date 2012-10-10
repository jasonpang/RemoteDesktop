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
    public class ResponseBeginScreenshotMessage : NovaMessage
    {
        public Rectangle Region { get; set; }
        public uint Number { get; set; }
        // So the receieving storage knows how many bytes to pre-allocate in advance
        public int FinalLength { get; set; }

        public ResponseBeginScreenshotMessage()
            : base((ushort)CustomMessageType.ResponseBeginScreenshotMessage)
        {
        }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write(Region.X);
            message.Write(Region.Y);
            message.Write(Region.Width);
            message.Write(Region.Height);
            message.Write(Number);
            message.Write(FinalLength);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Region = new Rectangle(message.ReadInt32(), message.ReadInt32(), message.ReadInt32(), message.ReadInt32());
            Number = message.ReadUInt32();
            FinalLength = message.ReadInt32();
        }
    }
}
