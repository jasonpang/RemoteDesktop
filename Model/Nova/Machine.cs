using System.Net;
using Lidgren.Network;

namespace Model.Nova
{
    /// <summary>
    /// Represents all the info necessary of a Client or Server machine to store their Introducer registration.
    /// </summary>
    public class Machine : ISerializable
    {
        /// <summary>
        /// Gets or sets the hardware ID of this machine.
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// Gets or sets the NovaId, which identifies machines to the end-user.
        /// </summary>
        /// <remarks>The NovaId is what the client types in to connect to a server. The NovaId identifies the server.</remarks>
        public string NovaId { get; set; }

        /// <summary>
        /// Gets or sets the server's password.
        /// </summary>
        /// <remarks>This field is never sent over the transport. A local Machine object is instead referenecd with this field.</remarks>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the hash of the server's password.
        /// </summary>
        /// <remarks>The server generates a password and sends it to the Introducer as a hash. The client then types in the
        /// password as part of the authentication procedure and sends the hash of what was typed. The hashes are then compared.</remarks>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Returns the public endpoint (IP address, port) of this client machine.
        /// </summary>
        /// <remarks>The public endpoint can be obtained by reading the IP address and port off of the UDP connection.</remarks>
        public IPEndPoint PublicEndPoint { get; set; }

        /// <summary>
        /// Returns the private endpoint (IP address, port) of this client machine.
        /// </summary>
        /// <remarks>The client registration packet must contain the private endpoint - no other way to get it.</remarks>
        public IPEndPoint PrivateEndPoint { get; set; }

        public Machine()
        {
            // Default values
            PublicEndPoint = new IPEndPoint(IPAddress.Any, 0);
            PrivateEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        /// <summary>
        /// Writes the contents of this type to the transport.
        /// </summary>
        public void WritePayload(NetOutgoingMessage message)
        {
            message.Write(Identity);
            message.Write(NovaId);
            message.Write(PasswordHash);
            message.Write(PublicEndPoint);
            message.Write(PrivateEndPoint);
        }

        /// <summary>
        /// Reads the transport into the contents of this type.
        /// </summary>
        public void ReadPayload(NetIncomingMessage message)
        {
            Identity = message.ReadString();
            NovaId = message.ReadString();
            PasswordHash = message.ReadString();
            PublicEndPoint = message.ReadIPEndpoint();
            PrivateEndPoint = message.ReadIPEndpoint();
        }
    }
}