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
        public RangeObservableCollection<UserProduct> Products { get; private set; } = new RangeObservableCollection<UserProduct>();
        public string Proteins { get; private set; } = string.Empty;
        public string Fat { get; private set; } = string.Empty;
        public string Carbohydrates { get; private set; } = string.Empty;
        public IPopupService PopupService { get; private set; } = ServiceHelper.Current.GetService<IPopupService>();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ICommand ChangeDateOfWeekCommand => new Command<string>(GetProductsCollection);
        public ICommand ShowCalendarCommand => new Command(() => PopupService.ShowPopup(new CalendarPopup()));
        public ICommand OpenProductEditorCommand => new Command<ProductDataTemplate>( async (product) => await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(product)));
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
            List<UserProduct> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            Products.ReplaceRange(products);
        }
        private void GetProductsCollection()
        {
            List<UserProduct> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            Products.ReplaceRange(products);
        }
        private void CalculateMakro()
        {
            decimal proteins = 0;
            decimal fat = 0;
            decimal carbohydrates = 0;
            foreach (var i in Products)
            {
                proteins += i.Proteins;
                fat += i.Fat;
                carbohydrates += i.Carbohydrates;
            }
            Proteins = proteins.ToString() + "g";
            Fat = fat.ToString() + "g";
            Carbohydrates = carbohydrates.ToString() + "g";
        }
    }
}
