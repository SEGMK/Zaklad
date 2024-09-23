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
    public class ProductEditorViewModel_CreateProd : IProductEditorViewModel
    {
        public ObservableCollection<ButtonWithDecision> DecisionButtonsCollection { get; private set; }
        public ProductEditorViewModel_CreateProd(IProductDataTemplate productTemplate)
        {
            ProductDataTemplate = productTemplate;
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
                        product.ProductTemplate = ProductDataTemplate;
                        product.Gramature = Gramature;
                        UserProductsData.SaveProduct(product, DateManager.CurrentDate);
                    })
                }
            };
        }
        public ProductEditorViewModel_CreateProd(IUserProduct userProduct)
        {
            ProductDataTemplate = userProduct.ProductTemplate;
            Gramature = userProduct.Gramature;
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
        IProductDataTemplate ProductDataTemplate { get; set; }
        public ImageSource ProductImage
        {
            get => ProductDataTemplate.ProductImage;
            set
            { 
                ProductDataTemplate.ProductImage = value;
                OnPropertyChange(nameof(ProductImage));
            }
        }
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
            get => ProductDataTemplate.Kcal;
            set
            {
                ProductDataTemplate.Kcal = value;
                OnPropertyChange(nameof(EditableKcal));
            }
        }
        public int EditableProteins
        {
            get => ProductDataTemplate.Proteins;
            set
            {
                ProductDataTemplate.Proteins = value;
                OnPropertyChange(nameof(EditableProteins));
            }
        }
        public int EditableFat
        {
            get => ProductDataTemplate.Fat;
            set
            {
                ProductDataTemplate.Fat = value;
                OnPropertyChange(nameof(EditableFat));
            }
        }
        public int EditableCarbohydrates
        {
            get => ProductDataTemplate.Carbohydrates;
            set
            {
                ProductDataTemplate.Carbohydrates = value;
                OnPropertyChange(nameof(EditableCarbohydrates));
            }
        }
        public string ProductName
        {
            get => ProductDataTemplate.Name;
            set
            {
                ProductDataTemplate.Name = value;
                OnPropertyChange(nameof(ProductName));
            }
        }

        public bool IsGramatureReadOnly => false;

        public IAsyncCommand TakeNewPhotoCommand => new AsyncCommand(TakeNewPhoto);

        private async Task TakeNewPhoto()
        {
            MessagingCenter.Subscribe<MauiCamera, System.Drawing.Bitmap>(this, "photo", (sender, image) =>
            {
                ProductDataTemplate.ProductImage = CustomProductImagesCRUD.SaveImage(image);
            });
            await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new MauiCamera());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
