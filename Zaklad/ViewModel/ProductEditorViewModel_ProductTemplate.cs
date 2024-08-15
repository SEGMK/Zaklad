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
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    public class ProductEditorViewModel_ProductTemplate : IProductEditorViewModel
    {
        public ObservableCollection<Button> DecisionButtonsCollection { get; private set; }
        IProductDataTemplate ChoosenProduct { get; set; }
        private IUserProduct _userProduct;
        public IUserProduct UserProduct => new UserProduct(Gramature, ChoosenProduct);
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ImageSource ProductImage { get; private set; }
        private decimal _editableKcal;
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
        public decimal EditableKcal
        {
            get
            {
                return _editableKcal;
            }
            set
            {
                _editableKcal = value;
                ChoosenProduct.Kcal = value;
                OnPropertyChange(nameof(EditableKcal));
            }
        }
        private decimal _editableProteins;
        public decimal EditableProteins
        {
            get
            {
                return _editableProteins;
            }
            set
            {
                _editableProteins = value;
                ChoosenProduct.Proteins = value;
                OnPropertyChange(nameof(EditableProteins));
            }
        }
        private decimal _editableFat;
        public decimal EditableFat
        {
            get
            {
                return _editableFat;
            }
            set
            {
                _editableFat = value;
                ChoosenProduct.Fat = value;
                OnPropertyChange(nameof(EditableFat));
            }
        }
        private decimal _editableCarbohydrates;
        public decimal EditableCarbohydrates
        {
            get
            {
                return _editableCarbohydrates;
            }
            set
            {
                _editableCarbohydrates = value;
                ChoosenProduct.Carbohydrates = value;
                OnPropertyChange(nameof(EditableCarbohydrates));
            }
        }
        private string _productName;
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
                ChoosenProduct.Name = value;
            }
        }
        public ProductEditorViewModel_ProductTemplate(IProductDataTemplate product)
        {
            ChoosenProduct = product;
            Gramature = 100;
            _editableCarbohydrates = product.Carbohydrates;
            _editableFat = product.Fat;
            _editableKcal = product.Kcal;
            _editableProteins = product.Proteins;
            _productName = string.IsNullOrWhiteSpace(product.Name) ? "product_name" : product.Name;
            ProductImage = product.ProductImage ?? "no_product.png";
            DecisionButtonsCollection = new ObservableCollection<Button>();
            DecisionButtonsCollection.Add(new Button()
            {
                Text = "Dodaj",
                BackgroundColor = Color.Parse("Green"),
                Command = new Command(() => UserProductsData.SaveProduct(UserProduct, DateManager.CurrentDate))
            });
        }
    }
}
