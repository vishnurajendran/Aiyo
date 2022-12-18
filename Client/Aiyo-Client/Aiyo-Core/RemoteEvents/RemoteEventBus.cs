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

            Logger.Log("Event Bus Initialised");
        }

        /// <summary>
        /// Register an event handler to the bus
        /// </summary>
        /// <param name="handler"></param>
        public static void RegisterEventHandler(IRemoteEventHandler handler)
        {
            if(handlers== null)
                handlers= new List<IRemoteEventHandler>();

            handlers.Add(handler);
            Logger.Log("Registered Event Handler " + handler.GetType().Name);
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

            var evnt = new RemoteEvent()
            {
                //metadata
                //needs to be filled by client before sending

                //payload
                EventId = sender.SupportedEventId,
                SerialisedEventData = eventData,
            };
            client.SubmitEvent(evnt);
            Logger.Log($"Sent Event Data for {evnt.EventId}");
        }

        private static void OnRemoteEventRecieved(RemoteEvent remoteEvent)
        {
            try
            {
                Logger.Log($"Recieved Event Data for {remoteEvent.EventId}");
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
