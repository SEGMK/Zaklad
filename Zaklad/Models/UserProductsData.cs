using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    public static class UserProductsData
    {
        private static string DataPath = FileSystem.Current.AppDataDirectory;
        public static List<Product> GetProducts(string fileName)
        {
            return ReadProductsJSON(fileName);
        }
        public static void SaveProduct(Product product, string fileName)
        {
            WriteProductsJSON(product, fileName);
        }
        private static List<Product> ReadProductsJSON(string fileName)
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(DataPath, fileName));
                return JsonConvert.DeserializeObject<List<Product>>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static void WriteProductsJSON(Product product, string fileName)
        {
            string json = File.ReadAllText(Path.Combine(DataPath, fileName));
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            products.Add(product);
            json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(json, Path.Combine(DataPath, fileName));
        }
    }
}
