using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Model
{
    /// <summary>
    /// Defines a type which can be serialized and deserialized to and from the network transport.
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Writes the contents of this type to the transport.
        /// </summary>
        void WritePayload(NetOutgoingMessage message);

        /// <summary>
        /// Reads the transport into the contents of this type.
        /// </summary>
        void ReadPayload(NetIncomingMessage message);
    }
}
