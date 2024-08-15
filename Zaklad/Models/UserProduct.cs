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
        public decimal Kcal => Math.Round(ProductTemplate.Kcal * Gramature / 100, 2);
        public decimal Carbohydrates => Math.Round(ProductTemplate.Carbohydrates * Gramature / 100, 2);
        public decimal Fat => Math.Round(ProductTemplate.Fat * Gramature / 100, 2);
        public decimal Proteins => Math.Round(ProductTemplate.Proteins * Gramature / 100, 2);

        public Guid Id => new Guid();
    }
}
