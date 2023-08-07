using AppA.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppA
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
           HttpClient client = new HttpClient();

           string json = client.GetStringAsync("https://dummyjson.com/products").Result;
           ProductVM products = JsonConvert.DeserializeObject<ProductVM>(json);
        }
    }
}