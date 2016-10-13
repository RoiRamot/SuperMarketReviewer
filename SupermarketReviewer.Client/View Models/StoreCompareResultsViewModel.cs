using System.Collections.Generic;
using System.Linq;
using SupermarketReviewer.Core.BaseClasses;
using SupermarketReviewer.Core.Models;

namespace SupermarketReviewer.Client
{
    class StoreCompareResultsViewModel : BaseViewModel
    {
        private List<ShoppingBasket> _shopingBaskets;
        private List<ShoppingItem> _shopingItems;

        public StoreCompareResultsViewModel(List<ShoppingBasket> shopingBaskts)
        {
            _shopingBaskets = shopingBaskts;
            _shopingItems = shopingBaskts.Select(x => x.ShoppingList).FirstOrDefault();
        }

        public List<ShoppingItem> ShopingItems
        {
            get
            {

                return _shopingItems;
            }
            set
            {
                value = _shopingItems;
                RaisePropertyChangedEvent("ShopingItems");
            }
        }

        public List<ShoppingBasket> ShopingBaskts
        {
            get { return _shopingBaskets; }
            set
            {
                _shopingBaskets = value;
                RaisePropertyChangedEvent("ShopingBaskts");

            }
        }
    }
}
