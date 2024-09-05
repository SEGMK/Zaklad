using System;
using System.Collections.Generic;
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
    public class ProductSelectionPopupViewModel : IProductSelectionPopupViewModel
    {
        public ICommand NavigateToAddItemByName => new Command(() => Shell.Current.GoToAsync(nameof(AddItemByName)));
        public ICommand NavigateToAddItemByBarcode => new Command(() => Shell.Current.GoToAsync(nameof(AddItemByBarcode)));
        public ICommand NavigateToCreateCustomProduct => new Command(() => Shell.Current.GoToAsync(nameof(CreateCustomProduct)));
    }
}
