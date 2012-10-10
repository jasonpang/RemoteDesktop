using System;
using System.Collections.Generic;
using Lidgren.Network;

namespace Model.Extensions
{
    /// <summary>
    /// Contains extension methods to write and read custom types to the network transport.
    /// </summary>
    /// <remarks>Lidgren's NetOutgoingMessage.Send, which contains the Write() and Read() methods, is a sealed class.</remarks>
    public static class NetOutgoingMessageExtensions
    {
        /// <summary>
        /// Writes any <see cref="IEnumerable{T}"/> to the network transport.
        /// </summary>
        /// <typeparam name="T">Any custom <see cref="ISerializable"/> type that is also <see cref="IEnumerable{T}"/></typeparam>.
        public static void Write<T>(this NetOutgoingMessage message, IEnumerable<T> list)
            where T : ISerializable, new()
        {
            if (list == null)
                throw new ArgumentNullException("list");

            message.Write(list.GetCount());
            foreach (var entity in list)
            {
                entity.WritePayload(message);
            }
        }

        /// <summary>
        /// Reads a List of custom type from the network transport.
        /// </summary>
        /// <typeparam name="T">Any custom <see cref="ISerializable"/> type that is also <see cref="IEnumerable{T}"/></typeparam>.
        public static IList<T> ReadList<T>(this NetIncomingMessage message)
            where T : ISerializable, new()
        {
            if (message == null)
                throw new ArgumentNullException("message");

            var list = new List<T>(message.ReadInt32());
            for (int i = 0; i < list.Count; i++)
            {
                var entity = new T();
                entity.ReadPayload(message);
                list.Add(entity);
            }
            return list;
        }

        public static void Write(this NetOutgoingMessage message, DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                dateTime = dateTime.ToUniversalTime();

            message.Write(dateTime.Ticks);
        }

        public static DateTime ReadDateTime(this NetIncomingMessage message)
        {
            return new DateTime(message.ReadInt64(), DateTimeKind.Utc);
        }
    }
}