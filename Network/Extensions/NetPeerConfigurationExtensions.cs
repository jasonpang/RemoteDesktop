using Lidgren.Network;

namespace Network.Extensions
{
    public static class NetPeerConfigurationExtensions
    {
        public static NetPeerConfiguration GetStandardConfiguration(this NetPeerConfiguration self)
        {
            var config = new NetPeerConfiguration("NovaRat");
            // Disable all message types
            config.DisableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.DisableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            config.DisableMessageType(NetIncomingMessageType.Data);
            config.DisableMessageType(NetIncomingMessageType.DebugMessage);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryResponse);
            config.DisableMessageType(NetIncomingMessageType.Error);
            config.DisableMessageType(NetIncomingMessageType.ErrorMessage);
            config.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            config.DisableMessageType(NetIncomingMessageType.Receipt);
            config.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            config.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.DisableMessageType(NetIncomingMessageType.WarningMessage);
            // Enable only what we need
            config.EnableMessageType(NetIncomingMessageType.UnconnectedData);
            config.EnableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            config.EnableMessageType(NetIncomingMessageType.StatusChanged);
            //config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            //config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.AcceptIncomingConnections = true;
            // No need to assign a port, as the OS will automatically assign an available port
            return config;
        }

        // Only for Introducer's NetworkServer
        public static NetPeerConfiguration GetStandardConfiguration(this NetPeerConfiguration self, int listeningPort)
        {
            var config = new NetPeerConfiguration("NovaRat");
            // Disable all message types
            config.DisableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.DisableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            config.DisableMessageType(NetIncomingMessageType.Data);
            config.DisableMessageType(NetIncomingMessageType.DebugMessage);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.DisableMessageType(NetIncomingMessageType.DiscoveryResponse);
            config.DisableMessageType(NetIncomingMessageType.Error);
            config.DisableMessageType(NetIncomingMessageType.ErrorMessage);
            config.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            config.DisableMessageType(NetIncomingMessageType.Receipt);
            config.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            config.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.DisableMessageType(NetIncomingMessageType.WarningMessage);
            // Enable only what we need
            config.EnableMessageType(NetIncomingMessageType.UnconnectedData);
            config.EnableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            config.EnableMessageType(NetIncomingMessageType.StatusChanged);
            //config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            //config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.Port = listeningPort;
            config.AcceptIncomingConnections = true;
            return config;
        }
    }
}