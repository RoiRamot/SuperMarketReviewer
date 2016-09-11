using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SupermarketReviewer.Core.Models
{
    [DataContract]
    public class Product
    {
        public Product(long productId, string name, double price,string lastUpdatedTime)
        {
            Price = price;
            Name = name;
            BarCodeNumber = productId;
            LastUpdatedTime = lastUpdatedTime;
            GuidId = Guid.NewGuid();
        }
        public Product(Product p)
        {
            Price = p.Price;
            Name = p.Name;
            BarCodeNumber = p.BarCodeNumber;
            LastUpdatedTime = p.LastUpdatedTime;
            UnitType = "kg";
            GuidId = Guid.NewGuid();

        }
        public Product()
        {
        }
        [Key]
        [DataMember]
        public Guid GuidId { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long BarCodeNumber { get; set; }

        [DataMember]
        public string UnitType { get; set; }
        [DataMember]
        public string LastUpdatedTime { get; set; }
    }
}
