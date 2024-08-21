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
        private decimal _kcal;
        public decimal Kcal 
        { 
            get => _kcal; 
            set => _kcal = Math.Round(value, 2); 
        }
        private decimal _carbohydrates;
        public decimal Carbohydrates 
        { 
            get => _carbohydrates; 
            set => _carbohydrates = Math.Round(value, 2); 
        }
        private decimal _fat;
        public decimal Fat
        { 
            get => _fat; 
            set => _fat = Math.Round(value, 2); 
        }
        private decimal _proteins;
        public decimal Proteins 
        { 
            get => _proteins; 
            set => _proteins = Math.Round(value, 2); 
        }

    }
}
