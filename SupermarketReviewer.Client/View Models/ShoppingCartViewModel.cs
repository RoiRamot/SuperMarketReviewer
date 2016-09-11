using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SupermarketReviewer.Client.Models;
using SupermarketReviewer.Client.Views;
using SupermarketReviewer.Client.View_Models;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Client
{
   public class ShoppingCartViewModel:BaseViewModel
    {
        public ShoppingCartViewModel()
        {
            SelectedProductsList = new ObservableCollection<ShoppingItem>();
            FilteredAvailableProducts = new ObservableCollection<Product>();
            availableProducts = new List<Product>();
            GetAvailableProdutsAsync();
            GetPastShoppingLists();
            foreach (var product in availableProducts)
            {
                FilteredAvailableProducts.Add(product);
            }
        }

        private List<Product> availableProducts;
        private string _filterInput;
        private bool _isBusy;
       private bool _isSoppingBasketBusy;
       private ShoppingListForDb _pastSelectedList;

       public bool IsBusy
       {
           get { return _isBusy; }
           set
           {
               _isBusy = value;
               RaisePropertyChangedEvent("IsBusy");
           }
       }

       public bool IsSoppingBasketBusy
       {
           get { return _isSoppingBasketBusy; }
           set
           {
               _isSoppingBasketBusy = value;
               RaisePropertyChangedEvent("IsSoppingBasketBusy");
           }
       }



       public string FilterInput
       {
           get { return _filterInput; }
           set
           {
               _filterInput = value;
               var filterList = availableProducts.Where(p => p.Name.StartsWith(_filterInput));
               FilteredAvailableProducts.Clear();
               foreach (var product in filterList)
               {
                   FilteredAvailableProducts.Add(product);
               }
           }
       }
       public ObservableCollection<ShoppingItem> SelectedProductsList { get; set; }
       public ObservableCollection<ShoppingListForDb> PastShoppingLists { get; set; }

       public ShoppingListForDb PastSelectedList
       {
           get
           {
               return _pastSelectedList;
           }
           set
           {
               SelectedProductsList.Clear();
               _pastSelectedList = value;
               foreach (var shoppingItem in value.ShoppingList)
               {
                   SelectedProductsList.Add(shoppingItem);
               }
           }
       }

       public Product SelectedProduct { get; set; }
       public ShoppingItem BasketSelectedProduct { get; set; }
       public ObservableCollection<Product> FilteredAvailableProducts { get; set; }
       private async void GetAvailableProdutsAsync()
        {
            var connector = new ServerConector();
            IsBusy = true;
             var products = new List<Product>();
           await Task.Run(() =>
            {
                products = connector.GetAllProduts();
            });
            
            foreach (var product in products)
            {
                availableProducts.Add(product);
            }
            FilterInput = "";
            
            IsBusy = false;
        }
       public ICommand AddNewSelectedProductCommand
        {
            get { return new DelegateCommand(AddNewSelectedProduct); }
        }
       public ICommand RefreshProductListCommand
        {
            get { return new DelegateCommand(GetAvailableProdutsAsync); }
        }
       public ICommand CheckPricesCommand
        {
            get { return new DelegateCommand(CheckPrices); }
        }
       public ICommand RemoveItemFromShoppingCartCommand
        {
            get { return new DelegateCommand(RemoveItemFromShoppingCart); }
        }
       private void AddNewSelectedProduct()
        {
            var addedProduct = new ShoppingItem(SelectedProduct,0.0);
            SelectedProductsList.Add(addedProduct);
        }
       private void RemoveItemFromShoppingCart()
       {
           SelectedProductsList.Remove(BasketSelectedProduct);
       }
       private async void CheckPrices()
        {
            var connector = new ServerConector();
            IsSoppingBasketBusy = true;
           try
           {
            var list=new List<ShoppingBasket>();
            await Task.Run(() =>
               {
                  list = connector.CheckPrices(SelectedProductsList.ToList());
               });
            var resultwindow = new Window();
            resultwindow.WindowStyle = WindowStyle.ToolWindow;
            resultwindow.Title = "Price Compare Window";
            resultwindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            resultwindow.DataContext = new StoreCompareResultsViewModel(list);
            resultwindow.Content = new StoreCompareResultsView();
            resultwindow.Show();
           }
           finally
           {
               IsSoppingBasketBusy = false;
           }
        }

       private async void GetPastShoppingLists()
       {
           PastShoppingLists=new ObservableCollection<ShoppingListForDb>();
           var connector = new ServerConector();
           IsSoppingBasketBusy = true;
           try
           {
               var list = new List<ShoppingListForDb>();
               await Task.Run(() =>
               {
                   list = connector.GetPastShoppingLists();
               });
               foreach (var item in list)
               {
                   PastShoppingLists.Add(item);
               }
           }
           finally
           {
               IsSoppingBasketBusy = false;
           }
       }


    }


}
