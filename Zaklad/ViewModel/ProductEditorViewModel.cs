using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    public class ProductEditorViewModel
    {
        ProductDataTemplate ChoosenProduct { get; set; }
        private UserProduct _userProduct;
        public UserProduct UserProduct => new UserProduct(Gramature, ChoosenProduct);
        public ImageSource ProductImage { get; private set; }
        private decimal _editableKcal;
        private int _gramature;
        public int Gramature
        {
            get => _gramature;
            set => _gramature = value;
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
        public ProductEditorViewModel(ProductDataTemplate product)
        {
            ChoosenProduct = product;
            Gramature = 100;
            _editableCarbohydrates = product.Carbohydrates;
            _editableFat = product.Fat;
            _editableKcal = product.Kcal;
            _editableProteins = product.Proteins;
            _productName = string.IsNullOrWhiteSpace(product.Name) ? "product_name" : product.Name;
            ProductImage = product.ProductImage ?? "no_product.png";
        }
    }
}
