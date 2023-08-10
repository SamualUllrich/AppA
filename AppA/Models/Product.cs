
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppA.Models.Product;

namespace AppA.Models
{
    internal class Product
    {
        public IssPosition iss_position { get; set; }
        public long timestamp { get; set; }
        public string message { get; set; }
    }

    internal class IssPosition
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
