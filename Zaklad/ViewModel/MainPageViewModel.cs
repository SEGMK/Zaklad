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
using Zaklad.Interfaces;
using Zaklad.Views;
using System.Linq.Expressions;
using Zaklad.Interfaces.IViewModels;

namespace Zaklad.ViewModel
{
    internal class MainPageViewModel : IMainPageViewModel
    {
        public DateManager DateManager { get; private set; } = new DateManager();
        public ObservableCollection<IUserProduct> Products { get; private set; } = new ObservableCollection<IUserProduct>();
        private decimal _proteins = 0;
        public decimal Proteins
        {
            get { return _proteins; }
            private set
            {
                _proteins = value;
                OnPropertyChange(nameof(Proteins));
            }
        }
        private decimal _fat = 0;
        public decimal Fat
        {
            get { return _fat; }
            private set
            {
                _fat = value;
                OnPropertyChange(nameof(Fat));
            }
        }
        private decimal _carbohydrates = 0;
        public decimal Carbohydrates
        {
            get { return _carbohydrates; }
            private set
            {
                _carbohydrates = value;
                OnPropertyChange(nameof(Carbohydrates));
            }
        }
        private decimal _kcal = 0;
        public decimal Kcal
        {
            get { return _kcal; }
            private set
            {
                _kcal = value;
                OnPropertyChange(nameof(Kcal));
            }
        }
        public IPopupService PopupService { get; private set; } = ServiceHelper.Current.GetService<IPopupService>();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ICommand ChangeDateOfWeekCommand => new Command<string>(GetProductsCollection);
        public ICommand ShowCalendarCommand => new Command(() => PopupService.ShowPopup(new CalendarPopup()));
        public ICommand ShowProductSelection => new Command(() => PopupService.ShowPopup(new ProductSelectionPopup()));
        public ICommand OpenProductEditorCommand => new Command<IUserProduct>((product) => OpenProductEditor(product).FireAndForgetSafeAsync());
        private async Task OpenProductEditor(IUserProduct product)
        {
            bool? redirectToEarlierPage = (bool?)await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProd(product)));
            //check if popup was closed by button or by DissmisedByTappingOutsideOfPopup
            if (redirectToEarlierPage != null)
                GetProductsCollection();
        }
        private IMakroSettingsData _userMakroIntakeSettings;
        public IMakroSettingsData UserMakroIntakeSettings
        {
            get => _userMakroIntakeSettings;
            private set
            {
                _userMakroIntakeSettings = value;
                OnPropertyChange(nameof(UserMakroIntakeSettings));
            }
        }

        public MainPageViewModel()
        {
            Products.Clear();
            DateManager = new DateManager();
            CalendarPopupViewModel.DateChanged += (sender, date) => DateManager.ChangeCurrentDate(date);
            Products.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) => CalculateMakro());
            DateManager.ChangeCurrentDate(DateTime.Now);
            GetProductsCollection();
            UpdateUserMakro();
        }
        public void UpdateUserMakro() => UserMakroIntakeSettings = UserSettingsCRUD.GetUserSettingsData();
        private void GetProductsCollection(string date)
        {
            Products.Clear();
            DateManager.ChangeCurrentDate(DateTime.ParseExact(date, DateManager.DateFormat, null));
            List<IUserProduct> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            foreach (var i in products)
            {
                Products.Add(i);
            }
        }
        public void GetProductsCollection()
        {
            Products.Clear();
            List<IUserProduct> products = UserProductsData.GetProducts(DateManager.CurrentDate);
            foreach (var i in products)
            {
                Products.Add(i);
            }
        }
        private void CalculateMakro()
        {
            decimal proteins = 0;
            decimal fat = 0;
            decimal carbohydrates = 0;
            decimal kcal = 0;
            foreach (var i in Products)
            {
                proteins += i.Proteins;
                fat += i.Fat;
                carbohydrates += i.Carbohydrates;
                kcal += i.Kcal;
            }
            Proteins = proteins;
            Fat = fat;
            Carbohydrates = carbohydrates;
            Kcal = kcal;
        }
    }
}
