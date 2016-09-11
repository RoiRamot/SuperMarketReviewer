using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReviewer.ViewModels
{
    public class Product
    {
        public Product(string _code, string _name, string _price)
        {
            Price = _price;
            Name = _name;
            ProductId = _code;
        }
        public string Price { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
    }
}
