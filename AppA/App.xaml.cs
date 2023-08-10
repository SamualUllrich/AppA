using EmbedIO;
using EmbedIO.Actions;

namespace AppA
{
    public partial class App : Application
    {
        private WebSocketService webSocketService; 

        public App()
        {
            InitializeComponent();

            var gameId = Guid.NewGuid().ToString(); 
            var game = new Game(gameId); 
            webSocketService = new WebSocketService();

            _ = StartWebServerAsync(game); 

            MainPage = new MainPage();
        }

        private async Task StartWebServerAsync(Game gameInstance)
        {
            var webServer = new WebServer(o => o
                    .WithUrlPrefix("http://localhost:8080/")
                    .WithMode(HttpListenerMode.EmbedIO))
                    .WithWebApi("/api", m => m.RegisterController<GameController>())
                    .WithModule(new TicTacToeWebSocketModule("/ws", gameInstance, webSocketService)); // Pass the game instance

            // Start the EmbedIO server
            await webServer.RunAsync();

            // Start WebSocket connection after the server starts
            await webSocketService.ConnectAsync("ws://localhost:8080/ws", CancellationToken.None);
        }
    }
}