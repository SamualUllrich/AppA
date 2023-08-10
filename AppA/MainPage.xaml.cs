using AppA.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppA
{
    public partial class MainPage : ContentPage
    {
        private char currentPlayer = 'X'; // 'X' or 'O', representing the players
        private char[,] gameBoard = new char[3, 3]; // Represents the game board

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCellClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            // Get the row and column from the button's Tag property
            int row = (int)button.GetValue(Grid.RowProperty);
            int col = (int)button.GetValue(Grid.ColumnProperty);

            // Check if the cell is empty
            if (gameBoard[row, col] == '\0')
            {
                // Update the game board and the button's text
                gameBoard[row, col] = currentPlayer;
                button.Text = currentPlayer.ToString();

                // Check for a win or draw condition (not implemented in this example)

                // Switch to the other player
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }
    }
}