using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Lidgren.Network;
using Network.Extensions;
using Network.Messages;
using Network.Messages.Nova;

namespace Network
{
    public class NetworkClient : NetworkPeer
    {
        public NetworkClient()
            : base(NovaMessage.NovaProtocol)
        {
        }

        /// <summary>
        /// Gets the connection to the server, if any.
        /// </summary>
        public NetConnection ServerConnection
        {
            get
            {
                NetConnection retval = null;
                if (Connections.Count > 0)
                {
                    try
                    {
                        retval = Connections[0];
                    }
                    catch
                    {
                        // preempted!
                        return null;
                    }
                }
                return retval;
            }
        }

        /// <summary>
        /// Gets the connection status of the server connection (or NetConnectionStatus.Disconnected if no connection)
        /// </summary>
        public NetConnectionStatus ConnectionStatus
        {
            get
            {
                var conn = ServerConnection;
                if (conn == null)
                    return NetConnectionStatus.Disconnected;
                return conn.Status;
            }
        }

        /// <remarks>
        /// Call this to reconnect again later.
        /// </remarks>
        public void Disconnect()
        {
            if (IsConnected) 
                ServerConnection.Disconnect(String.Empty);
        }
    }
}