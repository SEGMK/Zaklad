using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zaklad.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zaklad.Models
{
    public static class UserCustomProductTemplates
    {
        private static string AppPath = FileSystem.Current.AppDataDirectory;
        private const string UserProductsFile = "UserCustomProducts";
        private static readonly string FilePath = Path.Combine(AppPath + UserProductsFile);
        private static readonly JsonSerializerOptions ProductConverter = new JsonSerializerOptions()
        {
            Converters = { new JsonProductCustomConverter() }
        };
        public static void SaveCustomTemplate(IProductDataTemplate productDataTemplate)
        {
            List<IProductDataTemplate> productTemplates = new List<IProductDataTemplate>();
            if (File.Exists(FilePath))
            {
                productTemplates = JsonSerializer.Deserialize<List<IProductDataTemplate>>(File.ReadAllText(FilePath), options: ProductConverter);
            }
            productTemplates.Add(productDataTemplate);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(productTemplates, options: ProductConverter));
        }
        public static List<IProductDataTemplate> GetCustomTemplates(string name = "")
        {
            try
            {
                List<IProductDataTemplate> result = JsonSerializer.Deserialize<List<IProductDataTemplate>>(File.ReadAllText(FilePath), options: ProductConverter);
                if(name != "")
                    result.Select(x => x.Name.ToLower().Contains(name.ToLower()));
                return result;
            }
            catch (Exception e)
            {
                return new List<IProductDataTemplate>();
            }
        }
        public static void UpdateCustomTemplate(IProductDataTemplate product)
        {
            List<IProductDataTemplate> products = System.Text.Json.JsonSerializer.Deserialize<List<IProductDataTemplate>>(File.ReadAllText(FilePath), options: ProductConverter);
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == product.Id)
                {
                    products.RemoveAt(i);
                    products.Insert(i, product);
                    break;
                }
            }
            File.WriteAllText(FilePath, JsonSerializer.Serialize(products, options: ProductConverter));
        }
        public static void DeleteCustomTemplate(Guid id)
        {
            List<IUserProduct> products = System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(File.ReadAllText(FilePath), options: ProductConverter);
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                {
                    products.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FilePath, JsonSerializer.Serialize(products, options: ProductConverter));
        }
    }
}
