using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.Interfaces.IViewModels
{
    public interface IAddItemByNameViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IProductDataTemplate> Products { get; }
        public ICommand SearchCommand { get; }
    }
}
