using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://dummyjson.com/products
//https://jsonformatter.curiousconcept.com/#
namespace AppA.Models
{
    internal class Product
    {
       public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal rating { get; set; }
        public int stock { get; set; }
        public string brand { get; set; }
        public string catagory { get; set; }
        public string thumbnail { get; set; }
        public List<string> images { get; set; }

    }

    internal class ProductVM
    {
    public List<Product> products { get; set; } = new List<Product>();
    public int total { get; set; }
    public int skip { get; set; }
    public int limit { get; set; }
    }
}
