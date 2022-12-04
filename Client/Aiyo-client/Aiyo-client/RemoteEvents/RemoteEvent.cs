using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiyo_Client.RemoteEvents
{
    /// <summary>
    /// Pay load class for any remote events trigerred.
    /// </summary>
    [System.Serializable]
    public class RemoteEvent
    {
        /// <summary>
        /// Time of Event creation
        /// </summary>
        public long Time;
        /// <summary>
        /// Sender of this event.
        /// </summary>
        public string SenderClientId;
        /// <summary>
        /// Uniquely identifier for this event
        /// </summary>
        public int EventId;
        /// <summary>
        /// Serialised data for this event
        /// </summary>
        public string SerialisedEventData;
    }
}
