using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.Interfaces
{
    public interface IAddItemByNameViewModel : INotifyPropertyChanged
    {
        public RangeObservableCollection<IProductDataTemplate> Products { get; }
        public ICommand SearchCommand { get; }
    }
}
