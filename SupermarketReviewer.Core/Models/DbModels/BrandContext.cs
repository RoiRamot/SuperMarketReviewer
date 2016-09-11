using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SupermarketReviewer.Core.Models.DbModels
{
    [Serializable]
    public class BrandForDb
    {
        public string Name { get;  set; }
        public double Id { get;  set; }
        public virtual List<StoreForDb> StoreList { get; set; }
        public BrandForDb()
        {
        }
        public BrandForDb(Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
            StoreList=new List<StoreForDb>();
            foreach (var store in brand.StoreList)
            {
                StoreList.Add(new StoreForDb(store));
            }
        }
        public BrandForDb(Brand brand, List<StoreForDb> saveStoreForDb)
        {
            Id = brand.Id;
            Name = brand.Name;
            StoreList=saveStoreForDb;
        }

    }
    [Serializable]
    public class StoreForDb
    {
        public StoreForDb(Store store)
        {
            StoreCode = store.StoreCode;
            Name = store.Name;
            Adress = store.Adress;
            ProductList = store.ProductList;
        }       
        public StoreForDb()
        {
        }

        [Key]
        public double StoreCode { get; set; }

        public string Name { get; set; }
        
        public string Adress { get; set; }
        
        public virtual List<Product> ProductList { get; set; }
        public string LastUpdatedTime { get; set; }


    }
    public class BrandContext : DbContext
    {
        public DbSet<BrandForDb> Brands { get; set; }
   
    }
}
