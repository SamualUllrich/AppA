using EmbedIO.WebSockets;
using System.Net.WebSockets;
using System.Text;

namespace AppA
{
    public class TicTacToeWebSocketModule : WebSocketModule
    {
        private readonly Game game;
        private readonly WebSocketService webSocketService;

        public TicTacToeWebSocketModule(string urlPath, Game gameInstance, WebSocketService webSocketServiceInstance)
            : base(urlPath, true)
        {
            game = gameInstance;
            webSocketService = webSocketServiceInstance;
        }

        protected override Task OnMessageReceivedAsync(
            IWebSocketContext context,
            byte[] rxBuffer,
            IWebSocketReceiveResult rxResult)
        {
            string message = Encoding.UTF8.GetString(rxBuffer);


            var moveParts = message.Split(',');
            if (moveParts.Length == 2 && int.TryParse(moveParts[0], out int row) && int.TryParse(moveParts[1], out int col))
            {
                char playerSymbol = 'X';
                bool isValidMove = game.MakeMove(row, col, playerSymbol);

                if (isValidMove)
                {
                    // Broadcast the move to other players
                    BroadcastAsync(message, c => c != context);
                }
                else
                {
                    // Inform the player that the move is invalid
                    SendAsync(context, "Invalid move. Try again.");
                }

            }

            return Task.CompletedTask;
        }

        protected override async Task OnClientConnectedAsync(IWebSocketContext context)
        {
            await SendAsync(context, "Welcome to the Tic Tac Toe game!");
        }

        protected override async Task OnClientDisconnectedAsync(IWebSocketContext context)
        {
            await SendToOthersAsync(context, "A player has left the game.");
        }

        private async Task SendToOthersAsync(IWebSocketContext context, string payload)
        {
            await BroadcastAsync(payload, c => c != context);
        }
    }
    
}