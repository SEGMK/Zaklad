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
        private static readonly JsonSerializerOptions ProductSerializer = new JsonSerializerOptions() 
        {
            Converters = { new JsonProductCustomConverter() },
        };
        public static List<IUserProduct> GetProducts(DateTime date)
        {
            return ReadProductsJSON(JsonProductsFileList + date.ToString(DateFormat));
        }
        public static void SaveProduct(IUserProduct product, DateTime date)
        {
            WriteProductsJSON(product, JsonProductsFileList + date.ToString(DateFormat));
        }
        private static List<IUserProduct> ReadProductsJSON(string fileName)
        {
            try
            {
                List<IUserProduct> listOfResults = new List<IUserProduct>();
                string json = File.ReadAllText(Path.Combine(DataPath, fileName));
                return System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductSerializer);
            }
            catch (Exception e)
            {
                return new List<IUserProduct>();
            }
        }
        private static void WriteProductsJSON(IUserProduct product, string fileName)
        {
            string json;
            List<IUserProduct> products = new List<IUserProduct>();
            if (File.Exists(Path.Combine(DataPath, fileName)))
            {
                json = File.ReadAllText(Path.Combine(DataPath, fileName));
                products = System.Text.Json.JsonSerializer.Deserialize<List<IUserProduct>>(json, options: ProductSerializer);
            }
            products.Add(product);
            json = System.Text.Json.JsonSerializer.Serialize(products, options: ProductSerializer);
            File.WriteAllText(Path.Combine(DataPath, fileName), json);
        }
    }
}
