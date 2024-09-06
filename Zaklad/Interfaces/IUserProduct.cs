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
        public ImageSource ProductImage => ProductTemplate.ProductImage;
        public string Name => ProductTemplate.Name;
        public int Kcal => ProductTemplate.Kcal;
        public int Carbohydrates => ProductTemplate.Carbohydrates;
        public int Fat => ProductTemplate.Fat;
        public int Proteins => ProductTemplate.Proteins;
        public IProductDataTemplate ProductTemplate { get; set; }
        public Guid Id { get; }
    }
}
