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
    [JsonDerivedType(typeof(UserProduct), typeDiscriminator: nameof(UserProduct))]
    public interface IUserProduct
    {
        public int Gramature { get; set; }
        public ImageSource ProductImage { get; }
        public string Name { get; }
        public decimal Kcal { get; }
        public decimal Carbohydrates { get; }
        public decimal Fat { get; }
        public decimal Proteins { get; }
        public IProductDataTemplate ProductTemplate { get; set; }
        public Guid Id { get; }
    }
}
