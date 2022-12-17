using Aiyo_Core.RemoteEvents.Client;
using Aiyo_Core.Utilities.ErrorManagement;

namespace Aiyo_Core.RemoteEvents
{
    public static class RemoteEventBus
    {
        private static List<IRemoteEventHandler> handlers;
        private static RemoteClientBase client;

        public static void Initialise(RemoteClientBase remoteClientInstance)
        {
            if (handlers == null)
                handlers = new List<IRemoteEventHandler>();

            //cleanup any remove listeners here
            handlers.Clear();
            client = remoteClientInstance;
            client.OnRemoteEventRecieved += OnRemoteEventRecieved;
        }

        /// <summary>
        /// Register an event handler to the bus
        /// </summary>
        /// <param name="handler"></param>
        public static void RegisterEventHandler(IRemoteEventHandler handler)
        {
            handlers.Add(handler);
        }

        /// <summary>
        /// Submit an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventData"></param>
        public static void SubmitEvent(IRemoteEventHandler sender, string eventData)
        {
            if(client == null)
            {
                Logger.LogError("Remote Client Instance is NULL!, Cannot emit events");
                return;
            }

            client.SubmitEvent(new RemoteEvent()
            {
                //metadata
                //needs to be filled by client before sending

                //payload
                EventId = sender.SupportedEventId,
                SerialisedEventData = eventData,
            });
        }

        private static void OnRemoteEventRecieved(RemoteEvent remoteEvent)
        {
            try
            {
                TryHandleEventData(remoteEvent);
            }
            catch (Exception ex) { 
                Logger.LogException(ex.ToString());
            }
        }

        private static void TryHandleEventData(RemoteEvent remoteEvent)
        {
            if (handlers == null)
                return;

            foreach(var handler in handlers)
            {
                if (handler.SupportedEventId.Equals(remoteEvent.EventId))
                    handler.OnEventRecieved(remoteEvent.SerialisedEventData);
            }
        }
    }
}
