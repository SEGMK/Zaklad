﻿using System;
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
        public ICommand SearchCommand => new Command(UpdateProductsList);
        public ICommand AddNewProductTemplateCommand => new Command(() => AddNewProductTemplate().FireAndForgetSafeAsync());
        private async Task AddNewProductTemplate()
        {
            bool? redirectToEarlierPage = (bool?)await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProdTemp(ServiceHelper.Current.GetService<IProductDataTemplate>())));
            if (redirectToEarlierPage != null)
                UpdateProductsList();
        }
        public ICommand OpenProductEditorCommand => new Command<IProductDataTemplate>((productTemplate) => OpenProductEditor(productTemplate).FireAndForgetSafeAsync());
        private async Task OpenProductEditor(IProductDataTemplate productTemplate)
        {
            bool? redirectToEarlierPage = (bool?)await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProdTemp(productTemplate, true)));
            if (redirectToEarlierPage != null)
                UpdateProductsList();
        }
        private string _userInputProduct;
        public string UserInputProduct { get => _userInputProduct; set => _userInputProduct = value; }
        private void UpdateProductsList()
        {
            Products.Clear();
            UserCustomProductTemplates.GetCustomTemplates(UserInputProduct).ForEach(x => Products.Add(x));
        }
    }
}
