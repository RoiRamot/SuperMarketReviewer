using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReviewer.ViewModels
{
    public class Brand
    {
        public string Name { get; private set; }
        public double Id { get; private set; }
        public virtual List<Store> StoreList { get; set; }

        public Brand(double id,string name)
        {
            Name = name;
            Id = id;
            StoreList =new List<Store>();
        }
    }

}
