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
        public decimal Kcal { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fat { get; set; }
        public decimal Proteins { get; set; }
    }
}
