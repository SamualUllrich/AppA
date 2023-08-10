using EmbedIO.WebSockets;
using System.Text;

namespace AppA
{
    public class TicTacToeWebSocketModule : WebSocketModule
    {
        public TicTacToeWebSocketModule(string urlPath)
            : base(urlPath, true)
        {
        }

        protected override Task OnMessageReceivedAsync(
            IWebSocketContext context,
            byte[] rxBuffer,
            IWebSocketReceiveResult rxResult)
        {
            // Handle the received message
            string message = Encoding.UTF8.GetString(rxBuffer);
            // Implement your logic to handle the received message
            return Task.CompletedTask;
        }

        protected override Task OnClientConnectedAsync(IWebSocketContext context)
        {
            // Handle client connection
            // For example, send a welcome message to the client
            return SendAsync(context, "Welcome to the Tic Tac Toe game!");
        }

        protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
        {
            // Handle client disconnection
            // For example, notify other players that someone left
            return Task.CompletedTask;
        }

        private Task SendToOthersAsync(IWebSocketContext context, string payload)
        {
            // Broadcast the payload to all connected clients except the given context
            return BroadcastAsync(payload, c => c != context);
        }
    }
}