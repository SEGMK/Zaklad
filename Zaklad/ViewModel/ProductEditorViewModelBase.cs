using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.CustomControls;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.Views;

namespace Zaklad.ViewModel
{
    internal abstract class ProductEditorViewModelBase : IProductEditorViewModel
    {
        protected ObservableCollection<ButtonWithDecision> InternalDecisionButtonsCollection { get; set; }
        public ReadOnlyObservableCollection<ButtonWithDecision> DecisionButtonsCollection { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ProductEditorViewModelBase(IProductDataTemplate productDataTemplate)
        {
            ProductDataTemplate = productDataTemplate;
            InternalDecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>();
            DecisionButtonsCollection = new ReadOnlyObservableCollection<ButtonWithDecision>(InternalDecisionButtonsCollection);
        }
        public ProductEditorViewModelBase(IUserProduct userProduct)
        {
            ProductDataTemplate = userProduct.ProductTemplate;
            Gramature = userProduct.Gramature;
            InternalDecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>();
            DecisionButtonsCollection = new ReadOnlyObservableCollection<ButtonWithDecision>(InternalDecisionButtonsCollection);
        }
        protected IProductDataTemplate ProductDataTemplate;
        public abstract int Gramature { get; set; }
        public abstract bool IsGramatureReadOnly { get; }

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

        public ImageSource ProductImage
        {
            get => ProductDataTemplate.ProductImage;
            set
            {
                ProductDataTemplate.ProductImage = value;
                OnPropertyChange(nameof(ProductImage));
            }
        }
        public IAsyncCommand TakeNewPhotoCommand => new AsyncCommand(TakeNewPhoto);
        private async Task TakeNewPhoto()
        {
            ManageImageChanges();
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    if (ProductImage as FileImageSource != null && (ProductImage as FileImageSource).File != "no_product.png")
                        OldImageSource = (ProductImage as FileImageSource).File;
                    using Stream sourceStream = await photo.OpenReadAsync();
                    ProductImage = CustomProductImagesCRUD.SaveImage(sourceStream);
                }
                else
                {
                    ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", $"Zdjęcie nie zostało zrobione");
                }
            }
            else
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", $"Urządzenie nie wspiera kamery");
            }
        }
        protected string OldImageSource = "";
        protected async Task ManageImageChanges()
        {
            if (OldImageSource != "")
                CustomProductImagesCRUD.DeleteImage(OldImageSource);
        }
    }
}
