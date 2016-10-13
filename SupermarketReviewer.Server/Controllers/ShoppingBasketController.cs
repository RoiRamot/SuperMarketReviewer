using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
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
                    foreach (var brand in brands)
                    {
                        var stores = brand.StoreList;
                        foreach (var store in stores)
                        {
                            var productList = store.ProductList;
                            var barcodeList = productList.Select(p => p.BarCodeNumber);
                            var shopingList = new List<ShoppingItem>();
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
                                    if (storeProduct != null)
                                    {
                                        var price = storeProduct.Price;
                                        basketPrice = basketPrice + (price * item.Quntity);
                                    }
                                    shopingList.Add(new ShoppingItem(storeProduct,item.Quntity));
                                }
                                shopingBasketsList.Add(new ShoppingBasket(shopingList, store, brand, basketPrice));
                            }
                        }
                   
                    }


                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
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
                    shoppingBasketsList.AddRange(list);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }


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
