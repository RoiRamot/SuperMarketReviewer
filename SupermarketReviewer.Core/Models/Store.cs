using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SupermarketReviewer.Core.Models
{
    [DataContract]
    public  class Store
    {
        public Store(double id,string name, string adress)
        {
            StoreCode = id;
            Name = name;
            Adress = adress;
            ProductList = new List<Product>();
        }

        public Store()
        {

        }
        [DataMember]
        public double StoreCode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Adress { get; set; }

        [DataMember]
        public List<Product> ProductList { get; set; }
        [DataMember]
        public string LastUpdatedTime { get; set; }
    }

}
