using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketReviewer.ViewModels;

namespace SupermarketReviewer.Model
{
    class SuperMarketContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        //public DbSet<Store> Stores { get; set; }
        //public DbSet<Product> Products { get; set; }
    }
}
