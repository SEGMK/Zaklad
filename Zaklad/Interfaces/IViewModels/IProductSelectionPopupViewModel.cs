using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.Interfaces.IViewModels
{
    internal interface IProductSelectionPopupViewModel
    {
        public ICommand NavigateToAddItemByName { get; }
        public ICommand NavigateToAddItemByBarcode { get; }
        public ICommand NavigateToCreateCustomProduct { get; }
    }
}
