using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Interfaces;
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
        public ObservableCollection<IProductDataTemplate> Products { get; private set; } = new ObservableCollection<IProductDataTemplate>();
        public ICommand SearchCommand => new Command<string>(GetProducts);
        public ICommand OpenProductEditorCommand => new Command<IProductDataTemplate>(OpenProductEditor);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private async void GetProducts(string productName)
        {
            if(String.IsNullOrWhiteSpace(productName))
                return;
            try
            {
                Products.Clear();
                List<IProductDataTemplate> products = await FoodAPI.GetFoodDataByName(productName);
                foreach (var i in products)
                { 
                    Products.Add(i);
                }
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", "Nie znaleziono produktu");
            }
        }
        private async void OpenProductEditor(IProductDataTemplate product)
        {
            IUserProduct userProduct = (IUserProduct)await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(product));
            UserProductsData.SaveProduct(userProduct, DateManager.CurrentDate);
            await Shell.Current.GoToAsync(@$"..");
        }
    }
}
