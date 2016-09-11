using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SupermarketReviewer.Core.Models
{
    [DataContract]
    public class Brand
    {
        public Brand()
        {
        }

        public Brand(double id, string name)
        {
            Name = name;
            Id = id;
            StoreList = new List<Store>();

        }
        public Brand(double id, string name, List<Store> storeList)
        {
            Name = name;
            Id = id;
            StoreList = storeList;

        }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Id { get; set; }
        [DataMember]
        public List<Store> StoreList { get; set; }
    }
}
