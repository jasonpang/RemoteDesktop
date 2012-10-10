using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Model.LiveControl;
using Network.Extensions;

namespace Network.Messages.LiveControl
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseScreenshotMessage : NovaMessage
    {
        public ResponseScreenshotMessage()
            : base((ushort)CustomMessageType.ResponseScreenshotMessage)
        {
        }

        public byte[] Image { get; set; }
        public uint Number { get; set; }
        public int SendIndex { get; set; }

        public override void WritePayload(NetOutgoingMessage message)
        {
            base.WritePayload(message);
            message.Write(Image.Length);
            message.Write(Image);
            message.Write(Number);
            message.Write(SendIndex);
        }

        public override void ReadPayload(NetIncomingMessage message)
        {
            base.ReadPayload(message);
            Image = message.ReadBytes(message.ReadInt32());
            Number = message.ReadUInt32();
            SendIndex = message.ReadInt32();
        }
    }
}
