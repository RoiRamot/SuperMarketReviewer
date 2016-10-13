using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Server.Controllers
{
    public class ProductController : ApiController
    {

        [HttpGet]
        public List<Product> GetProducts()
        {
            using (var db = new BrandContext())
            {
                try
                {
                    var allProductsBybarcode = db.Brands.SelectMany(p=>p.StoreList).SelectMany(p=>p.ProductList).GroupBy(p => p.BarCodeNumber);
                    var allProductList = new List<Product>();
                    foreach (var item in allProductsBybarcode)
                    {
                        allProductList.Add(item.First());
                    }
                    return allProductList;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }

            }
        }

    }
}