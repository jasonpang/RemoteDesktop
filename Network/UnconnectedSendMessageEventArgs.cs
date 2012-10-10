using System;
using System.Net;

namespace Network
{
    /// <summary>
    /// Provides data for the <see cref="IConnectionProvider.ConnectionlessMessageReceived"/> event.
    /// </summary>
    public class UnconnectedSendMessageEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnconnectedMessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message received connectionlessly.</param>
        /// <param name="from">Where the message came from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> or <paramref name="from"/> is <c>null</c>.</exception>
        public UnconnectedSendMessageEventArgs(Message message, EndPoint to)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (to == null)
                throw new ArgumentNullException("to");

            Message = message;
            To = to;
        }

        /// <summary>
        /// Gets the received message.
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// Where the message is to be sent.
        /// </summary>
        public EndPoint To { get; private set; }
    }
}