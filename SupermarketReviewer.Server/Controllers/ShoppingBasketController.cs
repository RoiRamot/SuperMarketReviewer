using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Server.Controllers
{
    public class ShoppingBasketController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CheckPrices(List<ShoppingItem> basketsProducts)
        {
            var shopingBasketsList = new List<ShoppingBasket>();
            using (var db = new BrandContext())
            {
                try
                {
                    var brands = db.Brands;
                    //var stores = db.Brands.SelectMany(s => s.StoreList).ToList();
                    foreach (var brand in brands)
                    {
                        var stores = brand.StoreList;
                        foreach (var store in stores)
                        {
                            var productList = store.ProductList;
                            var barcodeList = productList.Select(p => p.BarCodeNumber);
                            var ShopingList = new List<ShoppingItem>();
                            bool hasAllItems=true;
                            foreach (var item in basketsProducts)
                            {
                                if (!barcodeList.Contains(item.Product.BarCodeNumber))
                                {
                                    hasAllItems = false;
                                }
                            }

                            if (hasAllItems)
                            {
                                var basketPrice = 0.0;

                                foreach (var item in basketsProducts)
                                {
                                    var storeProduct = productList.FirstOrDefault(p => p.BarCodeNumber == item.Product.BarCodeNumber);
                                    var price = storeProduct.Price;
                                    basketPrice = basketPrice + (price * item.Quntity);
                                    ShopingList.Add(new ShoppingItem(storeProduct,item.Quntity));
                                }
                                shopingBasketsList.Add(new ShoppingBasket(ShopingList, store, brand, basketPrice));
                            }
                        }
                   
                    }


                }
                catch (Exception)
                {

                    throw;
                }



            }
            using (var db = new ShoppingContext())
            {
               db.ShoppingLists.Add(new ShoppingListForDb(shopingBasketsList.First()));
               db.SaveChanges();
            }
            var formatter = new JsonMediaTypeFormatter();
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            formatter.WriteToStreamAsync(typeof(List<ShoppingBasket>), shopingBasketsList, stream, content, null).Wait();
            stream.Position = 0;
            var a = content.ReadAsStringAsync().Result;
            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new StringContent(a)
            };
            return message;

        }

        [HttpGet]
        public HttpResponseMessage GetPastShoppingLists()
        {
            var shoppingBasketsList=new List<ShoppingListForDb>();
            using (var db = new ShoppingContext())
            {
                try
                {
                    var list = db.ShoppingLists;
                    var product = db.ShoppingLists.First().ShoppingList.First();
                    //shoppingBasketsList.AddRange(db.ShoppingLists);
                    foreach (var item in list)
                    {
                        shoppingBasketsList.Add(item);
                    }
                }
                catch (Exception)
                {

                    throw;
                }


                var arry = shoppingBasketsList.ToArray();
                var formatter = new JsonMediaTypeFormatter();
                Stream stream = new MemoryStream();
                var content = new StreamContent(stream);
                formatter.WriteToStreamAsync(typeof(List<ShoppingListForDb>), shoppingBasketsList, stream, content, null);
                stream.Position = 0;
                var a = content.ReadAsStringAsync().Result;
                var message = new HttpResponseMessage(HttpStatusCode.OK)
                {

                    Content = new StringContent(a)
                };
                return message;
            }


        }


    }
}
