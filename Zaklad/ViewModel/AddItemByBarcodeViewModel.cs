using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Zaklad.ViewModel
{
    internal class AddItemByBarcodeViewModel
    {
        IPopupService PopupService { get; set; }
        private Product ChoosenProduct { get; set; }
        public AddItemByBarcodeViewModel()
        {
            PopupService = ServiceHelper.Current.GetService<IPopupService>();
        }
        public async void AddItemByBarcode(string barcode, CameraBarcodeReaderView camera)
        {
            try
            {
                camera.IsDetecting = false;
                ChoosenProduct = await FoodAPI.GetFoodDataBarcode(barcode);
                await PopupService.ShowPopupAsync(new ProductEditor(ChoosenProduct));
                UserProductsData.SaveProduct(ChoosenProduct, DateManager.CurrentDate);
                camera.IsDetecting = true;
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Błąd połączenia z serwerem", $"wystąpił problem podczas próby połączenia z serwerem: {ex.StatusCode}");
            }
            catch (RuntimeBinderException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Brak produktu", "podany produkt nie istnieje w bazie lub baza nie posiada wszystkich informacji na temat produktu");
            }
            catch (Exception ex)
            {
                camera.IsDetecting = true;
            }
        }
    }
}
