using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    internal class AddItemByNameViewModel : INotifyPropertyChanged
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
        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        public ICommand SearchCommand => new Command<string>(GetProducts);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private async void GetProducts(string productName)
        {
            try
            {
                Products.Clear();
                List<Product> products = await FoodAPI.GetFoodDataByName(productName);
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
    }
}
