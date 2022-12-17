
namespace Aiyo_Core.RemoteEvents
{
    public interface IRemoteEventHandler
    {
        public int SupportedEventId { get; }
        public void OnEventRecieved(string eventData);
    }
}
