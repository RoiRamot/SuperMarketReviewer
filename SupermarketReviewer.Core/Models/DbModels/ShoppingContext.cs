using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SupermarketReviewer.Core.Models.DbModels
{
    [DataContract]
    public class ShoppingListForDb
    {
        public ShoppingListForDb()
        {
            
        }
        public ShoppingListForDb(ShoppingBasket basket)
        {
            ListTimeStamp = DateTime.Now.ToString();
            DbId = Guid.NewGuid();
            ShoppingList=basket.ShoppingList;
        }
        [Key]
        [DataMember]
        public Guid DbId { get; set; }
        [DataMember]
        public virtual List<ShoppingItem> ShoppingList { get; set; }
        [DataMember]
        public string ListTimeStamp { get; set; }


    }
   
    public class ShoppingContext : DbContext
    {
        public DbSet<ShoppingListForDb> ShoppingLists { get; set; }

    }
}
