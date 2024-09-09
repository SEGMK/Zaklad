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
            if (productImage == null)
                ProductImage = "no_product.png";
            else
                ProductImage = productImage;
            if (String.IsNullOrEmpty(name))
                Name = "product_name";
            else
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
