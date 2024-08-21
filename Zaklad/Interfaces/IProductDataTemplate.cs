using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaklad.Models;

namespace Zaklad.Interfaces
{
    //this attribute is made for deserializing interface to typed object
    [JsonDerivedType(typeof(ProductDataTemplate), typeDiscriminator: nameof(ProductDataTemplate))]
    public interface IProductDataTemplate
    {
        public ImageSource ProductImage { get; set;}
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int Proteins { get; set; }
    }
}
