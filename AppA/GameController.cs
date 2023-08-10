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
                // Handle errors and return appropriate response
                return false;
            }
        }
    }
    public class Game
    {
        public string GameId { get; }
        private char[,] board = new char[3, 3]; // Represents the game board

        public char CurrentPlayer { get; private set; } = 'X'; // Current player ('X' or 'O')

        public bool MakeMove(int row, int col, char player)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
                return false; // Invalid move

            if (board[row, col] == '\0')
            {
                board[row, col] = player;
                return true; // Move successfully made
            }

            return false; // Cell already occupied
        }
        public Game(string gameId)
        {
            GameId = gameId;
            board = new char[3, 3];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = '\0'; // Initialize the board cells
                }
            }
        }

        public bool MakeMove(int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
                return false; // Invalid move

            if (board[row, col] == '\0')
            {
                board[row, col] = CurrentPlayer;
                SwitchPlayer();
                return true; // Move successfully made
            }

            return false; // Cell already occupied
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X'; // Toggle players
        }
    }

}
