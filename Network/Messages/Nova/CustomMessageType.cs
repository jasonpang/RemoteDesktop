namespace Network.Messages.Nova
{
    /// <summary>
    /// Defines the Nova protocol message types, for the Introducer, Client, and Server application-related messages.
    /// </summary>
    public enum CustomMessageType : ushort
    {
        ProxyMessage = 0,
        KeepAliveMessage,
        ProxyResponseMessage,
        RequestIntroducerRegistration, // Step 1, called by Server, pass hardware ID to permanently remember
        ResponseIntroducerRegistrationDenied,
        ResponseIntroducerRegistrationSucceeded, // Step 2a)
        RequestIntroducerIntroduction, // Step 2b), called by Client, pass server's Nova ID and Nova password
        // Note that it doesn't matter if someone happens to intercept the remote Nova ID and password. If the ID/password combination is incorrect, the connection wouldn't succeed
        // if the interceptor re-used those credentials. If the ID/password combination is correct, the Nova server wouldn't be accepting new connections anyways. The moment
        // the current connection is finished, the server will ask the Introducer for a new password.
        ResponseIntroducerIntroductionCompleted, // Denied: Client provided either invalid ID or incorrect password; Succeeded: Nothing, will get real success from OnNatTraversalSucceeeded

        // When the Server finishes the session with the client,
        RequestDisconnection,
        ResponseDisconnectionAcknowledged,

        // Step 3a) was OnNatTraversalSucceeded above, but after that, we are not yet connected, we must manually call Connect() after OnNatTraversalSucceeded
        //          Assume connection succeeds. If the connection didn't succeed, there wouldn't even be a message returned to describe success or failure.
        // Step 3b) so call Connect()

        // Step 4a) begins on NetworkClient.OnConnected
        // Step 4b), begin authentication procedure, send authentication packets
    }
}