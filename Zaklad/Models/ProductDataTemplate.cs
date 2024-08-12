using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    public class ProductDataTemplate : IProductDataTemplate
    {
        //class for displaying product's data for 100g 
        public ProductDataTemplate() { } //used for deserialization
        public ProductDataTemplate(string name, decimal kcal, decimal carbohydrates, decimal fat, decimal proteins, ImageSource productImage = null)
        {
            ProductImage = productImage;
            Name = name;
            Kcal = kcal;
            Carbohydrates = carbohydrates;
            Fat = fat;
            Proteins = proteins;
        }
        public ImageSource ProductImage { get; set; }
        public string Name { get; set; }
        public decimal Kcal { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fat { get; set; }
        public decimal Proteins { get; set; }
    }
}
