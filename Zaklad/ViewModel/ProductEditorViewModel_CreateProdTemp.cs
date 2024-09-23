using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    internal class ProductEditorViewModel_CreateProdTemp : IProductEditorViewModel
    {
        public ObservableCollection<ButtonWithDecision> DecisionButtonsCollection { get; private set; }
        public ProductEditorViewModel_CreateProdTemp(IProductDataTemplate productDataTemplate, bool editExisting = false)
        {
            ProductDataTemplate = productDataTemplate;
            if (editExisting)
            {
                DecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>()
                {
                    new ButtonWithDecision()
                    {
                        Text = "Edytuj",
                        BackgroundColor = Color.Parse("Yellow"),
                        Command = new Command(() => UserCustomProductTemplates.UpdateCustomTemplate(ProductDataTemplate))
                    },
                    new ButtonWithDecision()
                    {
                        Text = "Usuń",
                        BackgroundColor = Color.Parse("Red"),
                        Command = new Command(() => UserCustomProductTemplates.DeleteCustomTemplate(productDataTemplate.Id))
                    }
                };
            }
            else
            {
                DecisionButtonsCollection = new ObservableCollection<ButtonWithDecision>()
                {
                    new ButtonWithDecision()
                    {
                        RedirectToEarlierPage = true,
                        Text = "Dodaj",
                        BackgroundColor = Color.Parse("Green"),
                        Command = new Command(() => UserCustomProductTemplates.SaveCustomTemplate(ProductDataTemplate))
                    }
                };
            }
        }
        IProductDataTemplate ProductDataTemplate;
        public int Gramature => 100;
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

        public bool IsGramatureReadOnly => true;

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
