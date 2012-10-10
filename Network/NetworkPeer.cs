using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Network.Extensions;
using Model.Nova;
using System.Net;
using Network.Messages;
using Network.Messages.Nova;
using System.Threading;
using System.Diagnostics;
using Network.Utilities;

namespace Network
{
    /// <summary>
    /// A wrapper over Lidgren.Network.NetPeer.
    /// </summary>
    /// <remarks>NetworkClient and NetworkServer inherit this class. This is the base class used in Providers.</remarks>
    public class NetworkPeer : MessageHandler
    {
        private readonly NetPeer netPeer;
        private INetEncryption cryptoAlgorithm;
        private byte[] key;

        /// <summary>
        /// Creates and starts a new NetworkPeer object, for servers.
        /// </summary>
        /// <param name="port">The port the server should listen on.</param>
        /// <param name="protocols">An array of messaging protocols.</param>
        public NetworkPeer(int port, params Protocol[] protocols)
        {
            Protocols = new List<Protocol>(protocols);
            netPeer = new NetPeer(new NetPeerConfiguration("N/A").GetStandardConfiguration(port));
            netPeer.RegisterReceivedCallback(OnMessageReceivedCallback);
            Start();
        }

        /// <summary>
        /// Creates and starts a new NetworkPeer object with a random listening port, for clients.
        /// </summary>
        /// <param name="protocols">An array of messaging protocols.</param>
        public NetworkPeer(params Protocol[] protocols)
            : this(0, protocols)
        {
        }

        /// <summary>
        /// Gets the Configuration used by this NetPeer.
        /// </summary>
        public NetPeerConfiguration Configuration
        {
            get { return netPeer.Configuration; }
        }

        /// <summary>
        /// Gets or sets the list of protocols this client negotiates with.
        /// </summary>
        public List<Protocol> Protocols { get; set; }

        /// <summary>
        /// Raised when a connected message is received.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessageReceived;

        /// <summary>
        /// Raised when an unconnected message is received.
        /// </summary>
        public event EventHandler<UnconnectedMessageEventArgs> OnUnconnectedMessageReceived;

        /// <summary>
        /// Raised when a connected message has completed sending.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnConnectedMessageSent;

        /// <summary>
        /// Raised when an unconnected message has completed sending.
        /// </summary>
        public event EventHandler<UnconnectedSendMessageEventArgs> OnUnconnectedMessageSent;

        /// <summary>
        /// Raised when the connection is lost or manually disconnected.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> OnDisconnected;

        /// <summary>
        /// Raised when the client has connected.
        /// </summary>
        public event EventHandler<ConnectedEventArgs> OnConnected;

        /// <summary>
        /// Raised when NAT traversal has succeeded.
        /// </summary>
        public event EventHandler<NatTraversedEventArgs> OnNatTraversalSucceeded;

        /// <summary>
        /// An internal library debug message, for debugging purposes only.
        /// </summary>
        public event EventHandler<DebugMessageEventArgs> OnDebugMessage;

        /// <summary>
        /// Gets the list of NetConnection objects.
        /// </summary>
        public List<NetConnection> Connections
        {
            get
            {
                return netPeer.Connections;
            }
        }

        /// <summary>
        /// Gets the status of the network client.
        /// </summary>
        public NetworkStatus Status
        {
            get
            {
                if (netPeer == null)
                {
                    return NetworkStatus.NotRunning;
                }
                else
                {
                    byte ordinal = (byte)netPeer.Status;
                    return (NetworkStatus)ordinal;
                }
            }
        }

        /// <summary>
        /// Returns true if the client is connected to a server.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return Connections.Count > 0;
            }
        }

        /// <summary>
        /// Gets the statistics of this network client since it was initialized.
        /// </summary>
        public NetPeerStatistics Statistics
        {
            get
            {
                return netPeer.Statistics;
            }
        }

        /// <summary>
        /// Gets a unique identifier for this NetPeer based on Mac address and ip/port.
        /// </summary>
        /// <remarks>Not available until Start() has been called!</remarks>
        public long UniqueIdentifier
        {
            get
            {
                return netPeer.UniqueIdentifier;
            }
        }

        /// <summary>
        /// Gets the port number this NetworkClient is listening and sending on, if Start() has been called.
        /// </summary>
        public int Port
        {
            get
            {
                return netPeer.Port;
            }
        }

        /// <summary>
        /// Gets or sets the key used for symmetric encryption on all messages.
        /// </summary>
        public byte[] Key
        {
            get
            {
                return key;
            }
            set
            {
                cryptoAlgorithm = new NetAESEncryption(value, ArrayUtil.GetSequence(value, 0, 16));
                key = value;
            }
        }

        /// <summary>
        /// Gets whether this network is ready to encrypt its communications.
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            set;
        }

        /// <summary>
        /// Binds to socket and starts the networking thread.
        /// </summary>
        /// <remarks>This is called by the constructor of NetworkPeer.</remarks>
        public void Start()
        {
            netPeer.Start();
        }

        /// <summary>
        /// Connects to the remote endpoint.
        /// </summary>
        /// <param name="remoteEndPoint"></param>
        public void Connect(IPEndPoint remoteEndPoint)
        {
            netPeer.Connect(remoteEndPoint);
        }

        /// <summary>
        /// Connects to the remote endpoint with an initial hail message.
        /// </summary>
        public void Connect(IPEndPoint remoteEndPoint, NetOutgoingMessage hailMessage)
        {
            netPeer.Connect(remoteEndPoint, hailMessage);
        }

        /// <summary>
        /// Connects to the specified host and port.
        /// </summary>
        public void Connect(string host, int port)
        {
            netPeer.Connect(host, port);
        }

        /// <summary>
        /// Connects to the specified host and port with an initial hail message.
        /// </summary>
        public void Connect(string host, int port, NetOutgoingMessage hailMessage)
        {
            netPeer.Connect(host, port, hailMessage);
        }

        public void SendMessage(Message message)
        {
            SendMessage(message, NetDeliveryMethod.ReliableOrdered, 0);
        }

        public void SendMessage(Message message, NetDeliveryMethod deliveryMethod)
        {
            SendMessage(message, deliveryMethod, 0);
        }

        public void SendMessage(Message message, NetDeliveryMethod deliveryMethod, int sequenceChannel)
        {
            var outgoingMessage = netPeer.CreateMessage();
            message.WritePayload(outgoingMessage);

            if (Key != null)
            {
                IsAuthenticated = true;
                //outgoingMessage.Encrypt(cryptoAlgorithm);
            }

            netPeer.SendMessage(outgoingMessage, Connections, deliveryMethod, sequenceChannel);

            //Trace.WriteLine("Sent " + ((CustomMessageType)message.MessageType).ToString() + ".");

            if (OnConnectedMessageSent != null)
                OnConnectedMessageSent(this, new MessageEventArgs(this, message));
        }
        
        public void SendUnconnectedMessage(Message message, string host, int port)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (host == null)
                throw new ArgumentNullException("host");

            var outgoingMsg = netPeer.CreateMessage();
            message.WritePayload(outgoingMsg);

            netPeer.SendUnconnectedMessage(outgoingMsg, host, port);

            //Trace.WriteLine("Sent " + ((CustomMessageType)message.MessageType).ToString() + " unconnected to " + host + ":" + port.ToString() + ".");

            if (OnUnconnectedMessageSent != null)
                OnUnconnectedMessageSent(this, new UnconnectedSendMessageEventArgs(message, new DnsEndPoint(host, port)));
        }

        public void SendUnconnectedMessage(Message message, IPEndPoint endPoint)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (endPoint == null)
                throw new ArgumentNullException("endPoint");

            var outgoingMsg = netPeer.CreateMessage();
            message.WritePayload(outgoingMsg);

            netPeer.SendUnconnectedMessage(outgoingMsg, endPoint);

            //Trace.WriteLine("Sent " + ((CustomMessageType)message.MessageType).ToString() + " unconnected to " + endPoint.ToString() + ".");

            if (OnUnconnectedMessageSent != null)
                OnUnconnectedMessageSent(this, new UnconnectedSendMessageEventArgs(message, endPoint));
        }

        public void Introduce(Machine clientMachine, Machine serverMachine)
        {
            if (clientMachine == null)
                throw new ArgumentNullException("clientMachine");
            if (serverMachine == null)
                throw new ArgumentNullException("serverMachine");

            Trace.WriteLine("Introduced " + clientMachine.ToString() + " to " + serverMachine.ToString());

            netPeer.Introduce(clientMachine.PrivateEndPoint, clientMachine.PublicEndPoint, serverMachine.PrivateEndPoint,
            serverMachine.PublicEndPoint, String.Empty);
        }

        /// <remarks>
        /// Call this when the application is shutting down.
        /// </remarks>
        public void Shutdown()
        {
            netPeer.Shutdown(String.Empty);
        }

        /// <summary>
        /// Returns the internal private IPEndPoint of this machine (local IP).
        /// </summary>
        public IPEndPoint GetInternalEndPoint()
        {
            IPAddress subnetMask;
            return new IPEndPoint(NetUtility.GetMyAddress(out subnetMask), netPeer.Port);
        }

        private void OnMessageReceivedCallback(object netPeerObject)
        {
            // It could be a library message
            var incomingMessage = netPeer.ReadMessage();

            switch (incomingMessage.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    HandleStatusChangeMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.UnconnectedData:
                    HandleUnconnectedDataMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.NatIntroductionSuccess:
                    HandleNatIntroductionSuccessMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.Data:
                    HandleDataMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.DebugMessage:
                    HandleDebugMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.VerboseDebugMessage:
                    HandleDebugMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.WarningMessage:
                    HandleDebugMessage(incomingMessage);
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    HandleDebugMessage(incomingMessage);
                    break;
            }

            netPeer.Recycle(incomingMessage);
        }

        private void HandleDebugMessage(NetIncomingMessage message)
        {
            var msg = message.ReadString();

            System.Diagnostics.Trace.WriteLine("Debug Message:  " + msg);

            if (OnDebugMessage != null)
                OnDebugMessage(this, new DebugMessageEventArgs() { Message = msg } );
        }

        private void HandleStatusChangeMessage(NetIncomingMessage message)
        {
            var newStatus = (NetConnectionStatus)(message.ReadByte());

            System.Diagnostics.Trace.WriteLine("New Status Message:  " + newStatus.ToString());

            switch (newStatus)
            {
                case NetConnectionStatus.Connected:
                    if (OnConnected != null)
                        OnConnected(this, new ConnectedEventArgs() { From = message.SenderEndpoint } );
                    break;
                case NetConnectionStatus.Disconnected:
                    if (OnDisconnected != null)
                        OnDisconnected(this, new DisconnectedEventArgs(true));
                    break;
            }
        }

        private void HandleUnconnectedDataMessage(NetIncomingMessage message)
        {
            var p = message.ReadProtocol();
            ushort messageType = message.ReadUInt16();

            //Trace.WriteLine("Received " + ((CustomMessageType)messageType).ToString() + " unconnected.");

            var protocol = Protocols.Where(x => x.Equals(p)).First();

            var mHandlers = GetUnconnectedHandlers(protocol, messageType);
            var customMessage = protocol.Create(messageType);
            customMessage.ReadPayload(message);

            if (OnUnconnectedMessageReceived != null)
                OnUnconnectedMessageReceived(null, new UnconnectedMessageEventArgs(customMessage, message.SenderEndpoint));

            if (mHandlers != null)
            {
                for (int n = 0; n < mHandlers.Count; ++n)
                    mHandlers[n](new UnconnectedMessageEventArgs(customMessage, message.SenderEndpoint));
            }
        }

        private void HandleNatIntroductionSuccessMessage(NetIncomingMessage message)
        {
            if (OnNatTraversalSucceeded != null)
                OnNatTraversalSucceeded(this, new NatTraversedEventArgs { From = message.SenderEndpoint });
        }

        private void HandleDataMessage(NetIncomingMessage incomingMessage)
        {
            if (IsAuthenticated)
            {
                //incomingMessage.Decrypt(cryptoAlgorithm);
            }

            var p = incomingMessage.ReadProtocol();
            ushort messageType = incomingMessage.ReadUInt16();

            //Trace.WriteLine("Received " + ((CustomMessageType)messageType).ToString() + ".");

            var protocol = Protocols.Where(x => x.Equals(p)).First();

            var mHandlers = GetHandlers(protocol, messageType);
            var message = protocol.Create(messageType);
            message.ReadPayload(incomingMessage);

            if (OnMessageReceived != null)
                OnMessageReceived(null, new MessageEventArgs(this, message));

            if (mHandlers != null)
            {
                for (int n = 0; n < mHandlers.Count; ++n)
                    mHandlers[n](new MessageEventArgs(this, message));
            }
        }
    }
}
