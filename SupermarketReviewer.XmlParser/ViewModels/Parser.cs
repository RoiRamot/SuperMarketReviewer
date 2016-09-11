﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using SupermarketReviewer.Core.Models;

namespace SupermarketReviewer.XmlParser.ViewModels
{
    public class XParser
    {

        private List<Brand> storeBrands = new List<Brand>();
        private Dictionary<string, long> normalizeDictionary =new Dictionary<string, long>();
        public List<Brand> XmlScanner()
        {
            var xmlFilesList = Directory.GetFiles(@"C:\XmlFolder");
            foreach (var file in xmlFilesList)
            {
                if (file.Contains("Stores") )
                {
                    XmlParseToBranchList(file);
                }
            }

            foreach (var file in xmlFilesList)
            {
                if (file.Contains("PriceFull") )
                {
                    XmlParseToProductList(file);
                }
            }
            return storeBrands;
        }
        public void SaveToFile(List<Brand> brands)
        {
            var brandForFile = new List<Brand>();
            foreach (var brand in brands)
            {
                var storeForFile = new List<Store>();
                var stores = brand.StoreList;
                foreach (var store in stores)
                {
                    if (store.ProductList.Count > 0)
                    {
                        storeForFile.Add(store);
                    }
                }
                var brandItem = new Brand();
                brandItem.Name = brand.Name;
                brandItem.Id = brand.Id;
                brandItem.StoreList = storeForFile;
                brandForFile.Add(brandItem);
            }
            var writer =
                new XmlSerializer(typeof(List<Brand>));

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "//brands.xml";
            FileStream file = File.Create(path);

            writer.Serialize(file, brandForFile);
            file.Close();
            MessageBox.Show("finished Saveing");
        }
        public List<Brand> LoadFromFile()
        {
            var reader =
                    new XmlSerializer(typeof(List<Brand>));
            var file = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "//brands.xml");
            List<Brand> brands = (List<Brand>)reader.Deserialize(file);
            file.Close();
            storeBrands =new List<Brand>(brands);
           
            return new List<Brand>(brands);
        }
        public async Task UploadToDb()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:22465/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.PostAsJsonAsync("api/Brands/Postbrands", storeBrands);
                }
                catch (Exception)
                {
                    
                    throw;
                }


            }
        }
        private void XmlParseToProductList(string filePath)
        {
            var productList = new List<Product>();
            try
            {
                XDocument doc = XDocument.Load(filePath);
                if (doc.Root != null)
                {
                    var items = doc.Descendants("Item");
                    if (!items.Any())
                    {
                        items = doc.Descendants("Product");
                    }
                    if (!items.Any())
                    {
                        return;
                    }
                    foreach (var xElement in items)
                    {
                        string name = "";
                        double price = 0;
                        string productIdStr = "";
                        string dateStr = "";

                        var element = xElement.Element("ItemName");
                        if (element != null)
                        {
                            name = element.Value;
                        }

                        element = xElement.Element("ItemPrice");
                        if (element != null)
                        {
                            price = double.Parse(element.Value);
                        }

                        element = xElement.Element("ItemCode");
                        if (element != null)
                        {
                            productIdStr = element.Value;
                        }


                        long productId;
                        long.TryParse(productIdStr, out productId);
                        element = xElement.Element("PriceUpdateDate");
                        if (element != null)
                        {
                            dateStr = element.Value;
                        }
                        //if (productId.ToString().Count()>12)
                        //{
                            productList.Add(new Product(productId, name, price, dateStr));
                        //}
                    }

                }
                var chainID = doc.Descendants().FirstOrDefault(x => String.Compare(x.Name.LocalName, "chainid", StringComparison.CurrentCultureIgnoreCase) == 0);
                if (chainID != null)
                {
                    var chainId = double.Parse(chainID.Value);
                    XElement StoreId = doc.Descendants().FirstOrDefault(x => String.Compare(x.Name.LocalName, "storeid", StringComparison.CurrentCultureIgnoreCase) == 0);
                    if (StoreId != null)
                    {
                        var storeId = double.Parse(StoreId.Value);
                        var brandStores = storeBrands.Where(s => s.Id == chainId).Select(s => s.StoreList);
                        foreach (var stores in brandStores)
                        {
                            foreach (var store in stores)
                            {
                                if (store.StoreCode == storeId)
                                {
                                    foreach (var product in productList)
                                    {

                                        if (store.ProductList.Select(p => p.BarCodeNumber).Contains(product.BarCodeNumber) && (DateTime.Parse(store.ProductList.Where(p => p.BarCodeNumber == product.BarCodeNumber).Select(p => p.LastUpdatedTime).FirstOrDefault()) < DateTime.Parse(product.LastUpdatedTime)))
                                        {
                                            var updatedItem = store.ProductList.FirstOrDefault(p => p.BarCodeNumber == product.BarCodeNumber);
                                            if (updatedItem != null)
                                            {
                                                updatedItem.Name = product.Name;
                                                updatedItem.LastUpdatedTime = product.LastUpdatedTime;
                                                updatedItem.UnitType = product.UnitType;
                                                updatedItem.Price = product.Price;
                                            }

                                        }
                                        if (!store.ProductList.Select(p => p.BarCodeNumber).Contains(product.BarCodeNumber))
                                        {
                                            store.ProductList.Add(new Product(product.BarCodeNumber, product.Name, product.Price, product.LastUpdatedTime));
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        private void XmlParseToBranchList(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            if (doc.Root != null)
            {
                double brandId = 0;
                int storeId = 0;
                string storeName = null;
                string storeAdress = null;
                string brandName = null;
                var chainId = doc.Descendants().First(x => String.Compare(x.Name.LocalName,"chainid",StringComparison.CurrentCultureIgnoreCase)==0).Value;
                double.TryParse(chainId, out brandId);

                var chainName = doc.Descendants().Where(x=>String.Compare(x.Name.LocalName,"chainname",StringComparison.CurrentCultureIgnoreCase)==0).First().Value;

                if (!storeBrands.Any(brand => brand.Id == brandId))
                {
                    storeBrands.Add(new Brand(brandId, chainName));
                }

                var localBrand = storeBrands.First(s => s.Id == brandId);
                IEnumerable<XElement> items;
                items = doc.Descendants()
                        .Where(x => String.Compare(x.Name.LocalName, "branch", StringComparison.CurrentCultureIgnoreCase) == 0);
                if (!items.Any())
                {
                    items = doc.Descendants()
                            .Where(x => String.Compare(x.Name.LocalName, "store", StringComparison.CurrentCultureIgnoreCase) == 0);
                }     
                foreach (var item in items)
                {

                    var element = item.Descendants().First(x => String.Compare(x.Name.LocalName, "storeid", StringComparison.CurrentCultureIgnoreCase) == 0);
                    if (element != null)
                    {
                        int.TryParse(element.Value, out storeId);
                    }
                    element = item.Descendants().First(x => String.Compare(x.Name.LocalName, "storename", StringComparison.CurrentCultureIgnoreCase) == 0);
                    if (element != null)
                    {
                        storeName = element.Value.Trim();
                    }
                    element = item.Descendants().First(x => String.Compare(x.Name.LocalName, "address", StringComparison.CurrentCultureIgnoreCase) == 0); ;
                    if (element != null)
                    {
                        storeAdress = element.Value.Trim();
                    }

                    if (!localBrand.StoreList.Any(s => s.StoreCode == storeId))
                    {
                        localBrand.StoreList.Add(new Store(storeId, storeName, storeAdress));
                    }
                    else
                    {
                        var localStore = localBrand.StoreList.First(s => s.StoreCode == storeId);
                        localStore.Adress = storeAdress;
                        localStore.Name = storeName;
                    }
                }
            }
        }

        public List<Brand> Normalize()
        {
            if (!normalizeDictionary.Keys.Any())
            {
                LoadNormalizeDictionary();
            }
            NormalizeByName();
            //NormalizeByCode();
            return storeBrands;
        }

        //private void NormalizeByCode()
        //{
        //    foreach (var brand in storeBrands)
        //    {
        //        foreach (var store in brand.StoreList)
        //        {
        //            foreach (var product in store.ProductList )
        //            {
        //                if (normalizeDictionary.Keys.Contains(product.BarCodeNumber))
        //                {
        //                    product.Name = normalizeDictionary[product.BarCodeNumber];
        //                }
        //            }
        //        }
        //    }
        //}

        private async void NormalizeByName()
        {
            await Task.Run(() =>
            {
                try
                {
                    foreach (var brand in storeBrands)
                    {
                        var stringsRemove = @" *!@#$%^&(){}\/|.-_";
                        stringsRemove.ToCharArray();
                        foreach (var store in brand.StoreList)
                        {
                            foreach (var product in store.ProductList)
                            {
                                var localName = Regex.Replace(product.Name, stringsRemove, string.Empty);
                                localName = localName.Replace(" ", string.Empty);
                                foreach (var value in normalizeDictionary.Keys)
                                {
                                    var normalizelName = Regex.Replace(value, stringsRemove, string.Empty);
                                    normalizelName = normalizelName.Replace(" ", string.Empty);
                                    if (localName == normalizelName)
                                    {
                                        product.BarCodeNumber = normalizeDictionary[value];
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            });
            
            MessageBox.Show("finished Normalizing by name");
        }

        private void LoadNormalizeDictionary()
        {
            XDocument doc = XDocument.Load(@"C:\XmlFolder\productsDictionary.xml");
            var productList = doc.Descendants("Product");
            foreach (var product in productList)
            {
                var xElement = product.Element("ItemCode");
                long code = 0;
                if (xElement != null)
                {
                    long.TryParse(xElement.Value, out code);
                }
                xElement = product.Element("ItemName");
                string normalizedName = "";
                if (xElement != null)
                {
                    string name = xElement.Value;
                    normalizedName = Regex.Replace(name, @" *!@#$%^&(){}", "");
                }
                normalizeDictionary.Add(normalizedName, code);
            }

        }
    }

}
