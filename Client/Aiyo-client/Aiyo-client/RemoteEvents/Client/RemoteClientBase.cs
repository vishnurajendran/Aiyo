
namespace Aiyo_Client.RemoteEvents.Client
{
    public enum DisconnectionType
    {
        /// <summary>
        /// This means disconnection was a user triggered or clean disconnection 
        /// </summary>
        GRACEFULL,
        /// <summary>
        /// This means the connection was close unexpecteadly
        /// </summary>
        UNEXPECTED
    }

    public abstract class RemoteClientBase
    {
        //Properties

        /// <summary>
        /// Stores active connection related details
        /// </summary>
        public RemoteConnetcionInfo ConnectionInfo { get; protected set; }

        //Events
        /// <summary>
        /// Triggered when a client successfully connects
        /// </summary>
        public Action OnClientConnected;
        /// <summary>
        /// Triggered when a client disconnects
        /// </summary>
        public Action<DisconnectionType> OnClientDisconnected;

        /// <summary>
        /// Triggered when a remote event is recieved
        /// </summary>
        public Action<RemoteEvent> OnRemoteEventRecieved;

        //abstract methods
        /// <summary>
        /// Send Event implementation over the network
        /// </summary>
        /// <param name="eventData"></param>
        protected abstract void SendEventData(RemoteEvent eventData);
        
        /// <summary>
        /// Implementation for Connection to server
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public abstract void Connect(string address, int port);

        /// <summary>
        /// Implementation for Disconnection from server
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Preparation of event before sending
        /// </summary>
        /// <param name="eventData"></param>
        protected virtual void PerpareEvent(RemoteEvent eventData)
        {
            eventData.Time = DateTimeOffset.Now.ToUnixTimeSeconds();
            eventData.SenderClientId = ConnectionInfo.Id;
        }
        
        /// <summary>
        /// Submit events to client to send via network
        /// </summary>
        /// <param name="eventData"></param>
        public void SubmitEvent(RemoteEvent eventData)
        {
            PerpareEvent(eventData);
            SendEventData(eventData);
        }
    }
}
