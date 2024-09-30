using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.Views;

namespace Zaklad.ViewModel
{
    internal class AddItemByNameViewModel : IAddItemByNameViewModel
    {
        private static Dictionary<int, string> SearchModsMatrix = new Dictionary<int, string>()
        {
            { 0, "Nazwa"},
            { 1, "Barcode"}
        };
        public ObservableCollection<string> SearchMods { get; private set; } = new ObservableCollection<string>();
        private string _searchMode = SearchModsMatrix[0];
        public string SearchMode
        { 
            get 
            {
                return _searchMode;
            } 
            set 
            {
                _searchMode = value;
                OnPropertyChange(nameof(SearchMode)); 
            }
        }
        public AddItemByNameViewModel()
        {
            foreach (FoodAPI.SearchMode i in Enum.GetValues(typeof(FoodAPI.SearchMode)))
            {
                SearchMods.Add(SearchModsMatrix[(int)i]);
            }
        }
        private string _userInputProduct;
        public string UserInputProduct
        {
            get
            {
                return _userInputProduct;
            }
            set
            {
                _userInputProduct = value;
                OnPropertyChange(nameof(UserInputProduct));
            }
        }
        public ObservableCollection<IProductDataTemplate> Products { get; private set; } = new ObservableCollection<IProductDataTemplate>();
        public IPopupService PopupService { get; private set; } = ServiceHelper.Current.GetService<IPopupService>();
        public ICommand SearchCommand => new Command<string>((productName) => GetProducts(productName).FireAndForgetSafeAsync());
        public ICommand OpenProductEditorCommand => new Command<IProductDataTemplate>((productDataTemplate) => OpenProductEditor(productDataTemplate).FireAndForgetSafeAsync());

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ICommand ShowCalendarCommand => new Command(() => PopupService.ShowPopup(new CalendarPopup()));
        private async Task GetProducts(string productName)
        {
            if(String.IsNullOrWhiteSpace(productName))
                return;
            var loadingIndicatorPopup = new LoadingIndicatorPopup();
            try
            {
                PopupService.ShowPopup(loadingIndicatorPopup);
                Products.Clear();
                List<IProductDataTemplate> products = await FoodAPI.GetFoodByMode(productName, (FoodAPI.SearchMode)SearchModsMatrix.FirstOrDefault(x => x.Value == SearchMode).Key);
                products.AddRange(UserCustomProductTemplates.GetCustomTemplates(productName));
                foreach (var i in products)
                {
                    Products.Add(i);
                }
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", "Nie znaleziono produktu");
            }
            finally
            {
                loadingIndicatorPopup.Close();
            }
        }
        private async Task OpenProductEditor(IProductDataTemplate product)
        {
            bool? redirectToEarlierPage = (bool?)await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProd(product)));
            if(redirectToEarlierPage == true)
                await Shell.Current.GoToAsync(@$"..");
        }
    }
}
