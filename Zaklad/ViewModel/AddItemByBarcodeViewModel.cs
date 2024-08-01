using Microsoft.CSharp.RuntimeBinder;
using Zaklad.Models;

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
        public async Task AddItemByBarcode(string barcode)
        {
            try
            {
                ChoosenProduct = await FoodAPI.GetFoodDataBarcode(barcode);
                await PopupService.ShowPopupAsync(new ProductEditor(ChoosenProduct));
                UserProductsData.SaveProduct(ChoosenProduct, DateManager.CurrentDate);
                await Shell.Current.GoToAsync($"///{nameof(Zaklad.MainPage)}");
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Błąd połączenia z serwerem", $"wystąpił problem podczas próby połączenia z serwerem: {ex.StatusCode}");
            }
            catch (RuntimeBinderException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Brak produktu", "podany produkt nie istnieje w bazie");
            }
            catch (Exception ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Wystąpił nieoczekiwany błąd", ex.Message);
            }
        }
    }
}
