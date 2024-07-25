using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    public class Product
    {
        public Product(string name, decimal? kcal, decimal? carbohydrates, decimal? fat, decimal? proteins, ImageSource productImage = null)
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
        public decimal? Kcal { get; set; }
        public decimal? Carbohydrates { get; set; }
        public decimal? Fat { get; set; }
        public decimal? Proteins { get; set; }
    }
}
