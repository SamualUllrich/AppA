using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppA
{
    using EmbedIO;
    using EmbedIO.Routing;
    using EmbedIO.WebApi;

    public class GameController : WebApiController
    {
        private readonly List<Game> _games = new List<Game>();

        [Route(HttpVerbs.Post, "/api/game")]
        public async Task<bool> CreateGameAsync()
        {
            try
            {
                var gameId = Guid.NewGuid().ToString();
                var newGame = new Game(gameId);
                _games.Add(newGame);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class Game
    {
        public string GameId { get; }

        public Game(string gameId)
        {
            GameId = gameId;
        }
    }
}
