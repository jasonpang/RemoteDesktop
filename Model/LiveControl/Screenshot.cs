using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Model.LiveControl
{
    public class Screenshot : ISerializable
    {
        public byte[] Image { get; set; }
        public Rectangle Region { get; set; }
        public uint Number { get; set; }

        public Screenshot() { }

        public Screenshot(byte[] image, Rectangle region, uint number)
        {
            Image = image;
            Region = region;
            Number = number;
        }
        
        /// <summary>
        /// Writes the contents of this type to the transport.
        /// </summary>
        public void WritePayload(NetOutgoingMessage message)
        {
            message.Write(Image.Length);
            message.Write(Image);
            message.Write(Region.X);
            message.Write(Region.Y);
            message.Write(Region.Width);
            message.Write(Region.Height);
            message.Write(Number);
        }

        /// <summary>
        /// Reads the transport into the contents of this type.
        /// </summary>
        public void ReadPayload(NetIncomingMessage message)
        {
            Image = message.ReadBytes(message.ReadInt32());
            Region = new Rectangle(message.ReadInt32(), message.ReadInt32(), message.ReadInt32(), message.ReadInt32());
            Number = message.ReadUInt32();
        }
    }
}
