using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zaklad.Models
{
    public class FoodAPI
    {
        private static readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://world.openfoodfacts.net/api/v2/product/")
        };
        public static async Task<Product> GetFoodDataBarcode(string barcode)
        {
            HttpResponseMessage response = await httpClient.GetAsync(barcode + @"?fields=product_name,nutriscore_data,nutriments,nutrition_grades");
            try
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject(responseBody);
                var formatted = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return ShittyCodeAsFuck(formatted);
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }
        private static Product ShittyCodeAsFuck(string data)
        {
            string[] values = data.Split(System.Environment.NewLine);
            string name = "";
            decimal carb = 0m;
            decimal kcal = 0m;
            decimal fat = 0m;
            decimal prot = 0m;
            foreach (string s in values)
            {
                if (s.Contains("product_name"))
                {
                    string newString;
                    newString = s.Replace("\"product_name\":", "");
                    newString = newString.Replace("\"", "");
                    newString = newString.Trim();
                    name = newString;
                }
                else if (s.Contains("carbohydrates_value"))
                {
                    string pattern = @"-?\d+(\.\d+)?";
                    Regex regex = new Regex(pattern);
                    Match m = regex.Match(s);
                    carb = Convert.ToDecimal(m.Value, new CultureInfo("en-US"));
                }
                else if (s.Contains("energy-kcal_value"))
                {
                    string pattern = @"-?\d+(\.\d+)?";
                    Regex regex = new Regex(pattern);
                    Match m = regex.Match(s);
                    kcal = Convert.ToDecimal(m.Value, new CultureInfo("en-US"));
                }
                else if (s.Contains("fat_value"))
                {
                    string pattern = @"-?\d+(\.\d+)?";
                    Regex regex = new Regex(pattern);
                    Match m = regex.Match(s);
                    fat = Convert.ToDecimal(m.Value, new CultureInfo("en-US"));
                }
                else if (s.Contains("proteins_value"))
                {
                    string pattern = @"-?\d+(\.\d+)?";
                    Regex regex = new Regex(pattern);
                    Match m = regex.Match(s);
                    prot = Convert.ToDecimal(m.Value, new CultureInfo("en-US"));
                }
            }
            return new Product(name, kcal, carb, fat, prot);
        }
    }
}
