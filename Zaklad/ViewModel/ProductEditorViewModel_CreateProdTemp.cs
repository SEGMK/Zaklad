using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    internal class ProductEditorViewModel_CreateProdTemp : IProductEditorViewModel
    {
        public ObservableCollection<Button> DecisionButtonsCollection { get; private set; }
        public ProductEditorViewModel_CreateProdTemp(IProductDataTemplate productDataTemplate, bool editExisting = false)
        {
            ProductDataTemplate = productDataTemplate;
            if (editExisting)
            {
                DecisionButtonsCollection = new ObservableCollection<Button>()
                {
                    new Button()
                    {
                        Text = "Edytuj",
                        BackgroundColor = Color.Parse("Yellow"),
                        Command = new Command(() => UserCustomProductTemplates.UpdateCustomTemplate(ProductDataTemplate))
                    },
                    new Button()
                    {
                        Text = "Usuń",
                        BackgroundColor = Color.Parse("Red"),
                        Command = new Command(() => UserCustomProductTemplates.DeleteCustomTemplate(productDataTemplate.Id))
                    }
                };
            }
            else
            {
                DecisionButtonsCollection = new ObservableCollection<Button>()
                {
                    new Button()
                    {
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

        public ImageSource ProductImage => throw new NotImplementedException();//will be used for displaying product image

        public bool IsGramatureReadOnly => true;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
