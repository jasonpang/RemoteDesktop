namespace Network.Messages
{
    /// <summary>
    /// Base class for all custom messages.
    /// </summary>
    public abstract class NovaMessage : Message
    {
        protected NovaMessage(ushort type)
            : base(NovaProtocol, type)
        {
        }

        public static readonly Protocol NovaProtocol = new Protocol(2, 1);

        static NovaMessage()
        {
            NovaProtocol.Discover();
        }
    }
}