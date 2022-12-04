using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiyo_Client.RemoteEvents.Client
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
