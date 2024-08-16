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
                ImageSource productImage = null;
                decimal energyKcal = -1;
                decimal carbohydrates = -1;
                decimal fat = -1;
                decimal proteins = -1;
                if (i.image_front_small_url != null)
                    productImage = ImageSource.FromUri(new Uri(i.image_front_small_url));
                //Clear this catch < if
                try
                {
                    energyKcal = i.nutriments.energy_value / 4.184m;//its returned from API in KJ...
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    carbohydrates = i.nutriments.carbohydrates;
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    fat = i.nutriments.fat;
                }
                catch (RuntimeBinderException ex)
                { }
                try
                {
                    proteins = i.nutriments.proteins;
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
            decimal energyKcal = -1;
            decimal carbohydrates = -1;
            decimal fat = -1;
            decimal proteins = -1;
            string name = string.Empty;
            try
            {
                energyKcal = jsonObj.product.nutriments.energy_value;
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                carbohydrates = jsonObj.product.nutriments.carbohydrates_100g;
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                fat = jsonObj.product.nutriments.fat_100g;
            }
            catch (RuntimeBinderException ex)
            { }
            try
            {
                proteins = jsonObj.product.nutriments.proteins_100g;
            }
            catch (RuntimeBinderException ex)
            { }
            return new ProductDataTemplate(jsonObj.product.product_name, energyKcal, carbohydrates, fat, proteins);
        }
    }
}
