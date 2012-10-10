using System;
using System.Net;

namespace Network
{
    public class UnconnectedMessageEventArgs<T> : EventArgs
        where T : Message
    {
        public UnconnectedMessageEventArgs(IPEndPoint from, T message)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            From = from;
            Message = message;
        }

        /// <summary>
        /// Gets the sender's endpoint.
        /// </summary>
        public IPEndPoint From { get; private set; }

        public T Message { get; private set; }
    }

    /// <summary>
    /// Provides data for the <see cref="IConnectionProvider.ConnectionlessMessageReceived"/> event.
    /// </summary>
    public class UnconnectedMessageEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnconnectedMessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message received connectionlessly.</param>
        /// <param name="from">Where the message came from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> or <paramref name="from"/> is <c>null</c>.</exception>
        public UnconnectedMessageEventArgs(Message message, IPEndPoint from)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (from == null)
                throw new ArgumentNullException("from");

            Message = message;
            From = from;
        }

        /// <summary>
        /// Gets the received message.
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// Where the message came from.
        /// </summary>
        public IPEndPoint From { get; private set; }
    }
}