using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Core.Models
{
    [DataContract]

    public class ShoppingBasket
    {
        public ShoppingBasket(List<ShoppingItem> shoppingList, StoreForDb store, BrandForDb brand,double totalPrice)
        {
            ShoppingList = shoppingList;
            Store = store;
            Brand = brand;
            TotalPrice = totalPrice;
        }
        [DataMember]
        public List<ShoppingItem> ShoppingList { get; set; }
        [DataMember]
        public StoreForDb Store { get; set; }
        [DataMember]
        public BrandForDb Brand { get; set; }
        [DataMember]
        public double TotalPrice { get; set; }
    }
}
