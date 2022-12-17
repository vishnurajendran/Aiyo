using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiyo_Core.Playground
{
    public static class Connect
    {
        public static async Task ConnectServerAsync()
        {
            var server = new SocketIO("http://localhost:3000/");
            await server.ConnectAsync();
        }
    }
}
