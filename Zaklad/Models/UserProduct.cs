using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    public class UserProduct
    {
        //class for displaying users added product with custom gramature
        public UserProduct(int gramature, ProductDataTemplate productTemplate)
        {
            ProductTemplate = productTemplate;
            Gramature = gramature;
        }
        public int Gramature { get; set; }
        public ProductDataTemplate ProductTemplate { get; private set; }
        public ImageSource ProductImage => ProductTemplate.ProductImage;
        public string Name => ProductTemplate.Name;
        public decimal Kcal => ProductTemplate.Kcal * Gramature / 100;
        public decimal Carbohydrates => ProductTemplate.Carbohydrates * Gramature / 100;
        public decimal Fat => ProductTemplate.Fat * Gramature / 100;
        public decimal Proteins => ProductTemplate.Proteins * Gramature / 100;
    }
}
