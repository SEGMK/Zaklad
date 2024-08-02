using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;
using ZXing.QrCode.Internal;

namespace Zaklad.ViewModel
{
    internal class AddItemByNameViewModel : IAddItemByNameViewModel
    {
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
        public RangeObservableCollection<Product> Products { get; private set; } = new RangeObservableCollection<Product>();
        public ICommand SearchCommand => new Command<string>(GetProducts);
        public ICommand OpenProductEditorCommand => new Command<Product>(OpenProductEditor);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private async void GetProducts(string productName)
        {
            if(String.IsNullOrWhiteSpace(productName))
                return;
            try
            {
                List<Product> products = await FoodAPI.GetFoodDataByName(productName);
                Products.ReplaceRange(products);
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", "Nie znaleziono produktu");
            }
        }
        private async void OpenProductEditor(Product product)
        {
            await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(product));
            UserProductsData.SaveProduct(product, DateManager.CurrentDate);
            //await Shell.Current.GoToAsync($"///{nameof(Zaklad.MainPage)}");
        }
    }
}
