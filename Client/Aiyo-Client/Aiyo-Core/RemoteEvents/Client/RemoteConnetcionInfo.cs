namespace Aiyo_Core.RemoteEvents.Client
{
    /// <summary>
    /// Basic connection info
    /// </summary>
    public struct RemoteConnetcionInfo
    {
        /// <summary>
        /// Server address
        /// </summary>
        public string Address;
        /// <summary>
        /// Server Port
        /// </summary>
        public string Port;

        /// <summary>
        /// Id set for this connection, acts as a ClientID
        /// </summary>
        public string Id;
    }
}
