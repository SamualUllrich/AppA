using AppA.Models;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;


namespace AppA
{

    public partial class MainPage : ContentPage
    {
        private char currentPlayerSymbol;
        private char[,] gameBoard = new char[3, 3]; // Represents the game board
        private readonly WebSocketService webSocketService = new WebSocketService(); // Add this line


        public MainPage()
        {
            InitializeComponent();
            webSocketService = new WebSocketService();
        }

        private void HandleCellClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            OnCellClicked(row, col);
        }

        private void UpdateCellUI(int row, int col, char symbol)
        {
            // Find the button using its custom attributes
            Button button = FindButtonByCustomAttributes(row, col);

            if (button != null)
            {
                button.Text = symbol.ToString(); // Update the button's text to the player's symbol
                button.IsEnabled = false; // Disable the button after the move
            }
        }

        private Button FindButtonByCustomAttributes(int row, int col)
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Button button &&
                    Grid.GetRow(button) == row &&
                    Grid.GetColumn(button) == col)
                {
                    return button;
                }
            }
            return null;
        }
        private async void OnCellClicked(int row, int col)
        {

            string moveMessage = $"{row},{col}";

            // Use the webSocketService instance to send the message
            await webSocketService.SendAsync(moveMessage, CancellationToken.None);

            if (gameBoard[row, col] == '\0') // Check if the cell is empty
            {
                gameBoard[row, col] = currentPlayerSymbol; // Update the cell with the current player's symbol

                // Update the UI to reflect the new move
                UpdateCellUI(row, col, currentPlayerSymbol);

                // Switch to the next player
                currentPlayerSymbol = (currentPlayerSymbol == 'X') ? 'O' : 'X';
            }
        }
    }
}