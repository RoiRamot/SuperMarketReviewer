using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReviewer.ViewModels
{
    public class Store
    {
        public Store(double id,string name, string adress)
        {
            Id = id;
            Name = name;
            Adress = adress;
            ProductList = new List<Product>();
        }
        public double Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public virtual List<Product> ProductList { get; set; } 
    }

}
