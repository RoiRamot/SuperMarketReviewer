using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Client.View_Models
{
    class ServerConector
    {

        public  List<Product> GetAllProduts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22465/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response =  client.GetAsync("api/Product/GetProducts").Result;
                    var products =  response.Content.ReadAsAsync<List<Product>>().Result;
                    return products;
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }
        }

        public List<ShoppingBasket> CheckPrices(List<ShoppingItem> selectedProductsList)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22465/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.PostAsJsonAsync("api/ShoppingBasket/CheckPrices", selectedProductsList).Result;
                    var responseString = response.Content.ReadAsStreamAsync().Result;
                    var  formatter = new JsonMediaTypeFormatter();
                    var list = formatter.ReadFromStreamAsync(typeof(List<ShoppingBasket>), responseString, null, null).Result as List<ShoppingBasket>;
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public List<ShoppingListForDb> GetPastShoppingLists()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22465/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/ShoppingBasket/GetPastShoppingLists").Result;
                    var responseString = response.Content.ReadAsStreamAsync().Result;
                    var formatter = new JsonMediaTypeFormatter();
                    var list = formatter.ReadFromStreamAsync(typeof(List<ShoppingListForDb>), responseString, null, null).Result as List<ShoppingListForDb>;
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


    }
}
