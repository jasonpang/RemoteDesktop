using System;

namespace Network
{
    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(bool forcedDisconnect)
        {
            Forced = forcedDisconnect;
        }

        /// <summary>
        /// Gets whether the connection was forcefully shutdown.
        /// </summary>
        public bool Forced { get; private set; }
    }
}