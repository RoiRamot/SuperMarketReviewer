using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SupermarketReviewer.Core.Models;

namespace SupermarketReviewer.XmlParser.ViewModels
{

    public class XmlParser1
    {

        private List<Brand> storeBrands = new List<Brand>();
        public List<Brand> XmlScanner()
        {
                var xmlFilesList = Directory.GetFiles(@"C:\projects\קורס .net\SupremarketReviewer\SupermarketReviewer\SupermarketReviewer\xml\");
            foreach (var file in xmlFilesList)
            {
                XmlParseToBranchList(file);
            }
            foreach (var file in xmlFilesList)
            {
                XmlParseToProductList(file);
            }
            BuildDB(storeBrands);
            return storeBrands;
        }
        //List<Product>

        private void BuildDB(List<Brand> brands)
        {
            var db = new BrandContext();
            foreach (var brand in brands)
            {
                db.Brands.Add(brand);
            }

        }
        private  void XmlParseToProductList(string filePath)
        {
           var productList = new List<Product>();
            XDocument doc = XDocument.Load(filePath);
            if (doc.Root != null)
            {
                if (!doc.Descendants("Item").Any())
                {
                    return;
                }
                var items = doc.Root
                    .Elements("Items").Elements("Item");
                if (!items.Any())
                {
                    items = doc.Root
                    .Elements("Products").Elements("Product");
                }
                foreach (var xElement in items)
                {
                    string name="";
                    string price="";
                    string code = "";

                    var element = xElement.Element("ItemName");
                        if (element != null)
                        {
                             name = element.Value;
                        }

                    element = xElement.Element("ItemPrice");
                       if (element != null)
                        {
                            price = element.Value;
                        }

                    element = xElement.Element("ItemCode");
                        if (element != null)
                        {
                             code = element.Value;
                        }
                        productList.Add(new Product(code,name, price));
                }

            }
            var chainId = double.Parse(doc.Descendants("ChainId").First().Value);
            var storeId =double.Parse(doc.Descendants("StoreId").First().Value) ;
            var localStore = storeBrands.Where(s => s.Id == chainId).Select(s=>s.StoreList);
            foreach (var s in localStore)
            {

                foreach (var st in s)
                {
                    if (st.Id==storeId)
                    {
                        st.ProductList = productList;
                    }
                }
            }
        }
        private void XmlParseToBranchList(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            if (doc.Root != null)
            {
                double brandId=0;
                int storeId = 0;
                string storeName = null;
                string storeAdress = null;
                string brandName=null;
                if (!doc.Descendants("CHAINID").Any())
                {
                    return;
                }
                var chainId = doc.Descendants("CHAINID").First().Value;
                    double.TryParse(chainId, out brandId);

                    var chainName = doc.Descendants("CHAINNAME").First().Value;

                if (!storeBrands.Any(brand => brand.Id == brandId))
                {
                    storeBrands.Add(new Brand(brandId, chainName));
                }

                var localBrand = storeBrands.First(s => s.Id==brandId);
                    var items = doc.Descendants("STORE");

                    foreach (var item in items)
                    {

                        var element = item.Element("STOREID");
                        if (element != null)
                        {
                            int.TryParse(element.Value,out storeId);
                        }
                        element = item.Element("STORENAME");
                        if (element != null)
                        {
                             storeName = element.Value;
                        }
                        element = item.Element("ADDRESS");
                        if (element != null)
                        {
                             storeAdress = element.Value;
                        }

                        if (!localBrand.StoreList.Any(s=>s.Id==storeId))
                        {
                            localBrand.StoreList.Add(new Store(storeId, storeName, storeAdress));
                        }
                        else
                        {
                            var localStore = localBrand.StoreList.First(s => s.Id == storeId);
                            localStore.Adress = storeAdress;
                            localStore.Name = storeName;
                        }
                   }
                }
        }
    }
}
