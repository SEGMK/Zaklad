using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    public class CreateCustomProductViewModel : ICreateCustomProductViewModel
    {
        public ObservableCollection<IProductDataTemplate> Products { get; private set; } = new ObservableCollection<IProductDataTemplate>();
        public CreateCustomProductViewModel()
        {
            UserCustomProductTemplates.GetCustomTemplates().ForEach(x => Products.Add(x));
        }
        public ICommand SearchCommand => new Command(() => UserCustomProductTemplates.GetCustomTemplates(UserInputProduct).ForEach(x => Products.Add(x)));
        public ICommand AddNewProductTemplateCommand => new Command(async () =>
        {
            await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProdTemp(ServiceHelper.Current.GetService<IProductDataTemplate>())));
        });
        private string _userInputProduct;
        public string UserInputProduct { get => _userInputProduct; set => _userInputProduct = value; }
    }
}
