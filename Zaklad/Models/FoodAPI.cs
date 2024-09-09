using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zaklad.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zaklad.Models
{
    public class FoodAPI
    {
        private static readonly HttpClient HttpClientBarcodeAPI = new HttpClient()
        {
            BaseAddress = new Uri("https://world.openfoodfacts.net/api/v2/product/")
        };
        private static readonly HttpClient HttpClientWordSearchAPI = new HttpClient()
        {
            BaseAddress = new Uri("https://world.openfoodfacts.org/cgi/")
        };
        private static int NumberOfProductsdInList = 20;
        private static NumberFormatInfo NumberFormatProvider = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };
        //KISS
        public enum SearchMode
        { 
            Name = 0,
            Barcode = 1
        }
        public static async Task<List<IProductDataTemplate>> GetFoodByMode(string data, SearchMode mode)
        {
            switch (mode)
            {
                default:
                case SearchMode.Name:
                    return await GetFoodDataByName(data);
                    break;
                case SearchMode.Barcode:
                    var product = await GetFoodDataBarcode(data);
                    return new List<IProductDataTemplate>() { product };
                    break;
            }
        }
        public static async Task<List<IProductDataTemplate>> GetFoodDataByName(string productName)
        {
            productName = productName.ToLower().Replace(" ", "+");
            HttpResponseMessage response = await HttpClientWordSearchAPI.GetAsync($@"search.pl?action=process&search_terms=" + productName + @$"&sort_by=unique_scans_n&page_size={NumberOfProductsdInList}&json=1");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(responseBody);
            List<IProductDataTemplate> products = new List<IProductDataTemplate>();
            foreach (dynamic i in obj.products)
            {
                ImageSource productImage = "no_product.png";
                int energyKcal = -1;
                int carbohydrates = -1;
                int fat = -1;
                int proteins = -1;
                if (i.image_front_small_url != null)
                    productImage = ImageSource.FromUri(new Uri(i.image_front_small_url));
                //Clear this catch < if
                try
                {
                    energyKcal = (int)(Convert.ToDecimal(i.nutriments.energy_value, NumberFormatProvider) / 4.184m);//its returned from API in KJ...
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    carbohydrates = (int)Convert.ToDecimal(i.nutriments.carbohydrates, NumberFormatProvider);
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    fat = (int)Convert.ToDecimal(i.nutriments.fat, NumberFormatProvider);
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    proteins = (int)Convert.ToDecimal(i.nutriments.proteins, NumberFormatProvider);
                }
                catch (RuntimeBinderException ex)
                { }
                products.Add(new ProductDataTemplate(i.product_name, energyKcal, carbohydrates, fat, proteins, productImage));
            }
            return products;
        }
        public static async Task<IProductDataTemplate> GetFoodDataBarcode(string barcode)
        {
            HttpResponseMessage response = await HttpClientBarcodeAPI.GetAsync(barcode + @"?fields=product_name,nutriscore_data,nutriments,nutrition_grades");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic jsonObj = JsonConvert.DeserializeObject(responseBody);
            //Clear this catch < if
            int energyKcal = -1;
            int carbohydrates = -1;
            int fat = -1;
            int proteins = -1;
            string name = "product_name";
            try
            {
                energyKcal = (int)Convert.ToDecimal(jsonObj.product.nutriments.energy_value, NumberFormatProvider);
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                carbohydrates = (int)Convert.ToDecimal(jsonObj.product.nutriments.carbohydrates_100g, NumberFormatProvider);
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                fat = (int)Convert.ToDecimal(jsonObj.product.nutriments.fat_100g, NumberFormatProvider);
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                proteins = (int)Convert.ToDecimal(jsonObj.product.nutriments.proteins_100g, NumberFormatProvider);
            }
            catch (RuntimeBinderException ex)
            { }
            return new ProductDataTemplate(jsonObj.product.product_name, energyKcal, carbohydrates, fat, proteins);
        }
    }
}
