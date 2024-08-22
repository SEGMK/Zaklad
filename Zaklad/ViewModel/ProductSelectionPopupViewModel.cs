using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.ViewModel
{
    public class ProductSelectionPopupViewModel
    {
        public ICommand NavigateToAddItemByName => new Command(() => Shell.Current.GoToAsync(@$"{nameof(AddItemByName)}"));
        public ICommand NavigateToAddItemByBarcode => new Command(() => Shell.Current.GoToAsync(nameof(AddItemByBarcode)));
    }
}
