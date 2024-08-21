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
        public ProductDataTemplate(string name, int kcal, int carbohydrates, int fat, int proteins, ImageSource productImage = null)
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
        private int _kcal;
        public int Kcal 
        { 
            get => _kcal; 
            set => _kcal = value; 
        }
        private int _carbohydrates;
        public int Carbohydrates 
        { 
            get => _carbohydrates; 
            set => _carbohydrates = value; 
        }
        private int _fat;
        public int Fat
        { 
            get => _fat; 
            set => _fat = value; 
        }
        private int _proteins;
        public int Proteins 
        { 
            get => _proteins; 
            set => _proteins = value; 
        }

    }
}
