using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SupermarketReviewer.Core.Models
{
    [DataContract]

   public class ShoppingItem
    {
        private double _quantity;

        public ShoppingItem()
        {
            
        }
        public ShoppingItem(Product product, double quantity)
       {
           Product = new Product(product);
           Quntity = quantity;
            DbId = Guid.NewGuid();

       }
        [DataMember]
        public Product Product { get; set; }
        [DataMember]
        public double Quntity {
            get {return _quantity; }
            set
            {
                if (value>=0)
                {
                    _quantity = value;
                }
                else
                {
                    _quantity = 0;
                }
            }
        }
        [Key]
        [DataMember]
        public Guid DbId { get; set; }
    }
}
