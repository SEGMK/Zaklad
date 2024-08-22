using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    //class for displaying users added product with custom gramature
    public class UserProduct : IUserProduct
    {
        public UserProduct() { } //used for deserialization
        public UserProduct(int gramature, IProductDataTemplate productTemplate)
        {
            ProductTemplate = productTemplate;
            Gramature = gramature;
        }
        public int Gramature { get; set; }
        public IProductDataTemplate ProductTemplate { get; set; }
        public ImageSource ProductImage => ProductTemplate.ProductImage;
        public string Name => ProductTemplate.Name;
        public int Kcal => ProductTemplate.Kcal * Gramature / 100;
        public int Carbohydrates => ProductTemplate.Carbohydrates * Gramature / 100;
        public int Fat => ProductTemplate.Fat * Gramature / 100;
        public int Proteins => ProductTemplate.Proteins * Gramature / 100;
        private Guid? _id = null;
        public Guid Id 
        { 
            get 
            {
                if (_id != null)
                    return (Guid)_id;
                else
                    return Guid.NewGuid();
            } 
            set 
            {
                if (_id == null)
                    _id = value;
                else
                    throw new Exception("Value is allready assigned");
            }
        }
    }
}
