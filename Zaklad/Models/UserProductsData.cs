using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    public static class UserProductsData
    {
        private static string DataPath = FileSystem.Current.AppDataDirectory;
        private const string JsonProductsFileList = "UserProducts";
        private const string DateFormat = "dd_MM_yyyy";
        private static readonly JsonSerializerOptions ProductConverter = new JsonSerializerOptions()
        {
            Converters = { new JsonProductCustomConverter() }
        };
        public static void SaveProduct(IUserProduct product, DateTime date)
        {
            string json;
            List<IUserProduct> products = new List<IUserProduct>();
            if (File.Exists(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat))))
            {
                json = File.ReadAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)));
                products = System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductConverter);
            }
            products.Add(product);
            json = System.Text.Json.JsonSerializer.Serialize(products, options: ProductConverter);
            File.WriteAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)), json);
        }
        public static List<IUserProduct> GetProducts(DateTime date)
        {
            try
            {
                List<IUserProduct> listOfResults = new List<IUserProduct>();
                string json = File.ReadAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)));
                return System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductConverter);
            }
            catch (Exception e)
            {
                return new List<IUserProduct>();
            }
        }
        public static void EditProduct(IUserProduct product, DateTime date)
        {
            string json = File.ReadAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)));
            List<IUserProduct> products = System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductConverter);
            int i = 0;
            for (; i < products.Count; i++)
            {
                if (products[i].Id == product.Id)
                { 
                    products.RemoveAt(i);
                    break;
                }
            }
            products.Insert(i, product);
            json = System.Text.Json.JsonSerializer.Serialize(products, options: ProductConverter);
            File.WriteAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)), json);
        }
        public static void DeleteProduct(Guid id, DateTime date)
        {
            string json = File.ReadAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)));
            List<IUserProduct> products = System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductConverter);
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                {
                    products.RemoveAt(i);
                    break;
                }
            }
            json = System.Text.Json.JsonSerializer.Serialize(products, options: ProductConverter);
            File.WriteAllText(Path.Combine(DataPath, JsonProductsFileList + date.ToString(DateFormat)), json);
        }
    }
}
