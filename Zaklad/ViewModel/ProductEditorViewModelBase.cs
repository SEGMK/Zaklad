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
        public abstract int Gramature { get; protected set; }
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
            //it subs and unsubs on photo taken cuz GC does not collect this class upon popup close and can't be forced to
            SubscribeForPhoto();
            await ServiceHelper.Current.GetService<IPopupService>().ShowPopupAsync(new MauiCamera());
            MessagingCenter.Unsubscribe<MauiCamera, System.Drawing.Bitmap>(this, "photo");
        }
        private void SubscribeForPhoto() => MessagingCenter.Subscribe<MauiCamera, System.Drawing.Bitmap>(this, "photo", (sender, image) =>
        {
            //can't hold image as a Stream cuz ImageSource.FromStream() has a bug 
            ManageImageChanges();
            FileImageSource fileImageSource = ProductImage as FileImageSource;
            if (fileImageSource != null)
                OldImageSourceFilePath = fileImageSource.File;
            ProductImage = CustomProductImagesCRUD.SaveImage(image);
            IsNewPhotoTaken = true;
        });
        private string OldImageSourceFilePath = "";
        private bool IsNewPhotoTaken = false;
        protected async Task ManageImageChanges()
        {
            if (IsNewPhotoTaken)
                CustomProductImagesCRUD.DeleteImage(OldImageSourceFilePath);
        }
    }
}
