
namespace Aiyo_Client.RemoteEvents
{
    public interface IRemoteEventHandler
    {
        public int SupportedEventId { get; }
        public void OnEventRecieved(string eventData);
    }
}
