using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;
using ZXing.Net.Maui;

namespace Zaklad
{
    internal class AddItemByNameViewModel
    {
        public async void AddItemByBarcode(string barcode)
        { 
            Product product = await FoodAPI.GetFoodDataBarcode(barcode);
            UserProductsData.SaveProduct(product, "All.txt");
        }
    }
}
