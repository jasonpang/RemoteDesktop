using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    /// <summary>
    /// Wrapper for NetPeer.Status
    /// </summary>
    public enum NetworkStatus
    {
        /// <summary>
        /// NetworkPeer's socket is not bound.
        /// </summary>
        NotRunning = 0,
        /// <summary>
        /// NetworkPeer is initializing.
        /// </summary>
        Starting = 1,
        /// <summary>
        /// NetworkPeer is bound and listening.
        /// </summary>
        Running = 2,
        /// <summary>
        /// NetworkPeer is shutting down.
        /// </summary>
        ShutdownRequested = 3,
    }
}
