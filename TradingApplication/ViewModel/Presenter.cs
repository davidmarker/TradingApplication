using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradingApplication.Model;
using TradingApplication.View;
using TradingApplication.API;

namespace TradingApplication.ViewModel
{
    public class Presenter : ObservableObject
    {
        private string _headerTradePrice;
        private string _selectedProductValue;
        private Product _selectedProduct;
        public NotifyTaskCompletion<ProdTicker> MainProdTicker { get; private set; }
        public NotifyTaskCompletion<ObservableCollection<Product>> HeaderProductList { get; private set; }

        public Presenter()
        {
            HeaderProductList = new NotifyTaskCompletion<ObservableCollection<Product>>(
                GDAXAPIClient.GetProductsAsync());
            SelectedProduct = HeaderProductList.Result[0];
        }    
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                RaisePropertyChangedEvent("SelectedProduct");
                SelectedProductValue = _selectedProduct.id;
            }

        }

        public string HeaderTradePrice
        {
            get { return _headerTradePrice; }
            set
            {
                _headerTradePrice = value;
                RaisePropertyChangedEvent("HeaderTradePrice");

            }
        }

        public string SelectedProductValue
        {
            get { return _selectedProductValue; }
            set
            {
                _selectedProductValue = value;
                MainProdTicker = new NotifyTaskCompletion<ProdTicker>(
                    GDAXAPIClient.GetProdTickerAsync(_selectedProductValue));
                HeaderTradePrice = MainProdTicker.Result.price;
                RaisePropertyChangedEvent("SelectedProductValue");
            }
        }

        /*

        public IEnumerable<Product> HeaderProductList
        {
            get { return _headerProductList; }
            
        }

        
        
        public ICommand ConvertTextCommand
        {
            get { return new DelegateCommand(ConvertText); }
        }

        private void ConvertText()
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(_textConverter.ConvertText(SomeText));
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
                _history.Add(item);
        }
        */


    }
}
