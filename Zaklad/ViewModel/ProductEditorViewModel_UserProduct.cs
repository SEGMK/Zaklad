using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.ViewModel
{
    class ProductEditorViewModel_UserProduct : IProductEditorViewModel
    {
        private IUserProduct _userProduct;
        public IUserProduct UserProduct 
        {
            get => _userProduct;
            private set 
            {
                _userProduct = value;
                OnPropertyChange(nameof(Gramature));
                OnPropertyChange(nameof(EditableKcal));
                OnPropertyChange(nameof(EditableProteins));
                OnPropertyChange(nameof(EditableFat));
                OnPropertyChange(nameof(EditableCarbohydrates));
                OnPropertyChange(nameof(ProductName));
            } 
        }

        public int Gramature { get => UserProduct.Gramature; set { UserProduct.Gramature = value; OnPropertyChange(nameof(Gramature)); } }
        public decimal EditableKcal { get => UserProduct.Kcal; set { UserProduct.ProductTemplate.Kcal = value; OnPropertyChange(nameof(EditableKcal)); } }
        public decimal EditableProteins { get => UserProduct.Proteins; set { UserProduct.ProductTemplate.Proteins = value; OnPropertyChange(nameof(EditableProteins)); } }
        public decimal EditableFat { get => UserProduct.Fat; set { UserProduct.ProductTemplate.Fat = value; OnPropertyChange(nameof(EditableFat)); } }
        public decimal EditableCarbohydrates { get => UserProduct.Carbohydrates; set { UserProduct.ProductTemplate.Carbohydrates = value; OnPropertyChange(nameof(EditableCarbohydrates)); } }
        public string ProductName { get => UserProduct.Name; set { UserProduct.ProductTemplate.Name = value; OnPropertyChange(nameof(ProductName)); } }

        public ImageSource ProductImage => UserProduct.ProductImage;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ProductEditorViewModel_UserProduct(IUserProduct product)
        {
            UserProduct = product;
        }
    }
}
