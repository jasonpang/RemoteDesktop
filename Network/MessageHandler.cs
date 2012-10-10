using System;
using System.Collections.Generic;
using System.Linq;
using Cadenza.Collections;

namespace Network
{
    public abstract class MessageHandler
    {
        private readonly MutableLookup<MessageType, Action<MessageEventArgs>> handlers = new MutableLookup<MessageType, Action<MessageEventArgs>>();

        private readonly MutableLookup<MessageType, Action<UnconnectedMessageEventArgs>> unconnectedHandlers =
            new MutableLookup<MessageType, Action<UnconnectedMessageEventArgs>>();

        /// <summary>
        /// Registers a message handler.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="self">The context to register the message handler to.</param>
        /// <param name="handler">The message handler to register.</param>
        public void RegisterMessageHandler<T>(Action<MessageEventArgs<T>> handler)
            where T : Message, new()
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            var msg = new T();
            RegisterMessageHandler(msg.Protocol, msg.MessageType, e => handler(new MessageEventArgs<T>(e.Connection, (T) e.Message)));
        }

        /// <summary>
        /// Registers an unconnected message handler.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="self">The context to register the message handler to.</param>
        /// <param name="handler">The message handler to register.</param>
        public void RegisterUnconnectedMessageHandler<T>(Action<UnconnectedMessageEventArgs<T>> handler)
            where T : Message, new()
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            var msg = new T();
            RegisterUnconnectedMessageHandler(msg.Protocol, msg.MessageType, e => handler(new UnconnectedMessageEventArgs<T>(e.From, (T) e.Message)));
        }

        protected void RegisterMessageHandler(Protocol protocol, ushort messageType, Action<MessageEventArgs> handler)
        {
            lock (handlers)
            {
                if (!handlers.Contains(new MessageType(protocol, messageType)))
                {
                    handlers.Add(new MessageType(protocol, messageType), handler);
                }
            }
        }

        protected void RegisterUnconnectedMessageHandler(Protocol protocol, ushort messageType, Action<UnconnectedMessageEventArgs> handler)
        {
            lock (unconnectedHandlers)
            {
                if (!unconnectedHandlers.Contains(new MessageType(protocol, messageType)))
                {
                    unconnectedHandlers.Add(new MessageType(protocol, messageType), handler);
                }
            }
        }

        protected List<Action<MessageEventArgs>> GetHandlers(Protocol protocol, ushort messageType)
        {
            List<Action<MessageEventArgs>> mhandlers = null;
            lock (handlers)
            {
                IEnumerable<Action<MessageEventArgs>> thandlers;
                if (handlers.TryGetValues(new MessageType(protocol, messageType), out thandlers))
                    mhandlers = thandlers.ToList();
            }

            return mhandlers;
        }

        protected List<Action<UnconnectedMessageEventArgs>> GetUnconnectedHandlers(Protocol protocol, ushort messageType)
        {
            List<Action<UnconnectedMessageEventArgs>> mhandlers = null;
            lock (unconnectedHandlers)
            {
                IEnumerable<Action<UnconnectedMessageEventArgs>> thandlers;
                if (unconnectedHandlers.TryGetValues(new MessageType(protocol, messageType), out thandlers))
                    mhandlers = thandlers.ToList();
            }

            return mhandlers;
        }

        private class MessageType
            : IEquatable<MessageType>
        {
            public MessageType(Protocol protocol, ushort messageType)
            {
                this.protocol = protocol;
                this.messageType = messageType;
            }

            public MessageType(Message msg)
            {
                protocol = msg.Protocol;
                messageType = msg.MessageType;
            }

            private readonly Protocol protocol;
            private readonly ushort messageType;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj.GetType() != typeof (MessageType))
                    return false;
                return Equals((MessageType) obj);
            }

            public bool Equals(MessageType other)
            {
                if (ReferenceEquals(null, other))
                    return false;
                if (ReferenceEquals(this, other))
                    return true;
                return Equals(other.protocol, protocol) && other.messageType == messageType;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (protocol.GetHashCode()*397) ^ messageType.GetHashCode();
                }
            }

            public static bool operator ==(MessageType left, MessageType right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(MessageType left, MessageType right)
            {
                return !Equals(left, right);
            }
        }
    }
}