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
        private const string JsonProductsFileList = "UserProducts";
        private const string DateFormat = "dd_MM_yyyy";
        public static List<UserProduct> GetProducts(DateTime date)
        {
            return ReadProductsJSON(JsonProductsFileList + date.ToString(DateFormat));
        }
        public static void SaveProduct(UserProduct product, DateTime date)
        {
            WriteProductsJSON(product, JsonProductsFileList + date.ToString(DateFormat));
        }
        private static List<UserProduct> ReadProductsJSON(string fileName)
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(DataPath, fileName));
                return JsonConvert.DeserializeObject<List<UserProduct>>(json);
            }
            catch (Exception e)
            {
                return new List<UserProduct>();
            }
        }
        private static void WriteProductsJSON(UserProduct product, string fileName)
        {
            string json;
            List<UserProduct> products;
            if (File.Exists(Path.Combine(DataPath, fileName)))
            {
                json = File.ReadAllText(Path.Combine(DataPath, fileName));
                products = JsonConvert.DeserializeObject<List<UserProduct>>(json);
            }
            else
            {
                products = new List<UserProduct>();
            }
            products.Add(product);
            json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(Path.Combine(DataPath, fileName), json);
        }
    }
}
