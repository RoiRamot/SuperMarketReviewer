using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketReviewer.Core.Models;

namespace SupermarketReviewer.Client.Models
{
    public class SelectedProduct
    {
        public SelectedProduct(Product product, string productUnits, double productQuantity)
        {
            Product = product;
            ProductUnits = productUnits;
            ProductQuantity = productQuantity;
        }

        public SelectedProduct()
        {
            Product = new Product();
            ProductUnits = "unit";
            ProductQuantity = 0;
        }
        public Product Product { get; set; }
        public string ProductUnits { get; set; }
        public double ProductQuantity { get; set; }

    }
}
