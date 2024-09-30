using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.CustomControls;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.Views;

namespace Zaklad.ViewModel
{
    internal class ProductEditorViewModel_CreateProd : ProductEditorViewModelBase
    {
        public override bool IsGramatureReadOnly => false;

        public ProductEditorViewModel_CreateProd(IProductDataTemplate productTemplate) : base(productTemplate)
        {
            Gramature = 100;
            InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
            {
                RedirectToEarlierPage = true,
                Text = "Dodaj",
                BackgroundColor = Color.Parse("Green"),
                Command = new Command(() =>
                {
                    ManageImageChanges();
                    DeleteOriginalImageIfImageWasChanged();
                    IUserProduct product = ServiceHelper.Current.GetService<IUserProduct>();
                    product.ProductTemplate = ProductDataTemplate;
                    product.Gramature = Gramature;
                    UserProductsData.SaveProduct(product, DateManager.CurrentDate);
                })
            });
        }
        public ProductEditorViewModel_CreateProd(IUserProduct userProduct) : base(userProduct) 
        {
            InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
            {
                Text = "Edytuj",
                BackgroundColor = Color.Parse("Yellow"),
                Command = new Command(() =>
                {
                    ManageImageChanges();
                    DeleteOriginalImageIfImageWasChanged();
                    userProduct.Gramature = Gramature;
                    UserProductsData.EditProduct(userProduct, DateManager.CurrentDate);
                })
            });
            InternalDecisionButtonsCollection.Add(new ButtonWithDecision()
            {
                Text = "Usuń",
                BackgroundColor = Color.Parse("Red"),
                Command = new Command(() =>
                {
                    ManageImageChanges();
                    DeleteOriginalImageIfImageWasChanged();
                    FileImageSource fileImageSource = ProductImage as FileImageSource;
                    if (fileImageSource != null && fileImageSource.File != "no_product")
                        CustomProductImagesCRUD.DeleteImage(fileImageSource);
                    UserProductsData.DeleteProduct(userProduct.Id, DateManager.CurrentDate);
                })
            });
        }
        private int _gramature;
        public override int Gramature
        {
            get => _gramature;
            set
            {
                _gramature = value;
                OnPropertyChange(nameof(Gramature));
            }
        }
    }
}
