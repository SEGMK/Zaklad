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
        Product ChoosenProduct { get; set; }
        public ImageSource ProductImage { get; private set; }
        private decimal _editableKcal;
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
        public ProductEditorViewModel(Product product)
        {
            ChoosenProduct = product;
            _editableCarbohydrates = product.Carbohydrates ?? 0;
            _editableFat = product.Fat ?? 0;
            _editableKcal = product.Kcal ?? 0;
            _editableProteins = product.Proteins ?? 0;
            ProductImage = product.ProductImage ?? "no_product.png";
        }
    }
}
