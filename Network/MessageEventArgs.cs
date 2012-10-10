using System;
using Lidgren.Network;

namespace Network
{
    /// <remarks>Copied from Eric Maupin's Tempest networking library</remarks>
    public class MessageEventArgs<T> : EventArgs
        where T : Message
    {
        public MessageEventArgs(NetworkPeer connection, T message)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            Connection = connection;
            Message = message;
        }

        /// <summary>
        /// Gets the NetClient or NetServer for the event.
        /// </summary>
        public NetworkPeer Connection { get; private set; }

        public T Message { get; private set; }
    }

    /// <summary>
    /// Holds event data for a MessageReceived event.
    /// </summary>
    /// <remarks>Copied from Eric Maupin's Tempest networking library</remarks>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="MessageReceivedEventArgs"/>.
        /// </summary>
        /// <param name="connection">The parent NetClient or NetServer connection.</param>
        public MessageEventArgs(NetworkPeer connection, Message message)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (message == null)
                throw new ArgumentNullException("message");

            Connection = connection;
            Message = message;
        }

        /// <summary>
        /// Gets the connection for the event.
        /// </summary>
        public NetworkPeer Connection { get; private set; }

        /// <summary>
        /// Gets the message received.
        /// </summary>
        public Message Message { get; private set; }
    }
}