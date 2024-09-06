using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.Interfaces.IViewModels
{
    public interface ICreateCustomProductViewModel
    {
        public ObservableCollection<IProductDataTemplate> Products { get; }
        public string UserInputProduct { get; set; }
        public ICommand SearchCommand { get; }
        public ICommand AddNewProductTemplateCommand { get; }
        public ICommand OpenProductEditorCommand { get; }
    }
}
