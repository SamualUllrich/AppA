using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AppA
{
    public class WebSocketService
    {
        private ClientWebSocket webSocket;

        public async Task ConnectAsync(string url, CancellationToken cancellationToken)
        {
            webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri(url), cancellationToken);
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
            }
        }
    }
}
