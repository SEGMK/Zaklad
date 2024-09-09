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

namespace Zaklad.ViewModel
{
    public class ProductEditorViewModel_CreateProd : IProductEditorViewModel
    {
        public ObservableCollection<ButtonWithDecision> DecisionButtonsCollection { get; private set; }
        public ProductEditorViewModel_CreateProd(IProductDataTemplate productTemplate)
        {
            ProductTemplate = productTemplate;
            Gramature = 100;
            DecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>()
            {
                new ButtonWithDecision()
                {
                    RedirectToEarlierPage = true,
                    Text = "Dodaj",
                    BackgroundColor = Color.Parse("Green"),
                    Command = new Command(() =>
                    {
                        IUserProduct product = ServiceHelper.Current.GetService<IUserProduct>();
                        product.ProductTemplate = ProductTemplate;
                        product.Gramature = Gramature;
                        UserProductsData.SaveProduct(product, DateManager.CurrentDate);
                    })
                }
            };
        }
        public ProductEditorViewModel_CreateProd(IUserProduct userProduct)
        {
            ProductTemplate = userProduct.ProductTemplate;
            Gramature = 100;
            DecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>()
            {
                new ButtonWithDecision()
                {
                    Text = "Edytuj",
                    BackgroundColor = Color.Parse("Yellow"),
                    Command = new Command(() =>
                    {
                        userProduct.Gramature = Gramature;
                        UserProductsData.EditProduct(userProduct, DateManager.CurrentDate);
                    })
                },
                new ButtonWithDecision()
                {
                    Text = "Usuń",
                    BackgroundColor = Color.Parse("Red"),
                    Command = new Command(() =>
                    {
                        UserProductsData.DeleteProduct(userProduct.Id, DateManager.CurrentDate);
                    })
                }
            };
        }
        IProductDataTemplate ProductTemplate { get; set; }
        public ImageSource ProductImage => ProductTemplate.ProductImage;
        private int _gramature;
        public int Gramature
        {
            get => _gramature;
            set
            {
                _gramature = value;
                OnPropertyChange(nameof(Gramature));
            }
        }
        public int EditableKcal
        {
            get => ProductTemplate.Kcal;
            set
            {
                ProductTemplate.Kcal = value;
                OnPropertyChange(nameof(EditableKcal));
            }
        }
        public int EditableProteins
        {
            get => ProductTemplate.Proteins;
            set
            {
                ProductTemplate.Proteins = value;
                OnPropertyChange(nameof(EditableProteins));
            }
        }
        public int EditableFat
        {
            get => ProductTemplate.Fat;
            set
            {
                ProductTemplate.Fat = value;
                OnPropertyChange(nameof(EditableFat));
            }
        }
        public int EditableCarbohydrates
        {
            get => ProductTemplate.Carbohydrates;
            set
            {
                ProductTemplate.Carbohydrates = value;
                OnPropertyChange(nameof(EditableCarbohydrates));
            }
        }
        public string ProductName
        {
            get => ProductTemplate.Name;
            set
            {
                ProductTemplate.Name = value;
                OnPropertyChange(nameof(ProductName));
            }
        }

        public bool IsGramatureReadOnly => false;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
