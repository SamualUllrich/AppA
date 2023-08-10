using EmbedIO;
using EmbedIO.Actions;

namespace AppA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Start the EmbedIO web server asynchronously
            _ = StartWebServerAsync();

            MainPage = new MainPage();
        }

        private async Task StartWebServerAsync()
        {
            var webServer = new WebServer(o => o
                .WithUrlPrefix("http://localhost:8080/")
                .WithMode(HttpListenerMode.EmbedIO))
                //.WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendStringAsync("Welcome to Tic Tac Toe API")))
                .WithWebApi("/api", m => m.RegisterController<GameController>())
                .WithModule(new TicTacToeWebSocketModule("/ws")); 

            // Start the EmbedIO server
            await webServer.RunAsync();
        }
    }
}