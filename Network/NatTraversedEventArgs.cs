using System;
using System.Net;

namespace Network
{
    public class NatTraversedEventArgs : EventArgs
    {
        /// <summary>
        /// The endpoint of the other machine attempting the NAT punch-through.
        /// </summary>
        /// <remarks>Beware OnNatTraversalSuccess can be called multiple times, one for the internal IP and one for the external IP.
        /// Call Connect() to this endpoint.</remarks>
        public IPEndPoint From { get; set; }
    }
}