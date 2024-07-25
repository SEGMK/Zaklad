using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;
using CommunityToolkit.Maui.Views;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Zaklad.ViewModel
{
    internal class MainPageViewModel : IMainPageViewModel
    {
        public DateManager DateManager { get; private set; } = new DateManager();
        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        public string Proteins { get; private set; } = string.Empty;
        public string Fat { get; private set; } = string.Empty;
        public string Carbohydrates { get; private set; } = string.Empty;
        public IPopupService PopupService { get; private set; } = ServiceHelper.Current.GetService<IPopupService>();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ICommand ChangeDateOfWeekCommand => new Command<string>(GetProductsCollection);
        public ICommand ShowCalendarCommand => new Command(() => PopupService.ShowPopup(new CalendarPopup()));
        public MainPageViewModel()
        {
            DateManager = new DateManager();
            CalendarPopupViewModel.DateChanged += (sender, date) => DateManager.ChangeCurrentDate(date);
            Products.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) => CalculateMakro());
            DateManager.ChangeCurrentDate(DateTime.Now);
            GetProductsCollection();
        }
        private void GetProductsCollection(string date)
        {
            DateManager.ChangeCurrentDate(DateTime.ParseExact(date, DateManager.DateFormat, null));
            Products.Clear();
            List<Product> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }
        private void GetProductsCollection()
        {
            Products.Clear();
            List<Product> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }
        private void CalculateMakro()
        {
            decimal proteins = 0;
            decimal fat = 0;
            decimal carbohydrates = 0;
            foreach (var i in Products)
            {
                proteins += i.Proteins ?? 0;
                fat += i.Fat ?? 0;
                carbohydrates += i.Carbohydrates ?? 0;
            }
            Proteins = proteins.ToString() + "g";
            Fat = fat.ToString() + "g";
            Carbohydrates = carbohydrates.ToString() + "g";
        }
    }
}
