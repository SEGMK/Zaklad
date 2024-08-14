using Microsoft.CSharp.RuntimeBinder;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    internal class AddItemByBarcodeViewModel
    {
        IPopupService PopupService { get; set; }
        private IProductDataTemplate ChoosenProduct { get; set; }
        public AddItemByBarcodeViewModel()
        {
            PopupService = ServiceHelper.Current.GetService<IPopupService>();
        }
        public async Task AddItemByBarcode(string barcode)
        {
            try
            {
                ChoosenProduct = await FoodAPI.GetFoodDataBarcode(barcode);
                IUserProduct userProduct =  (IUserProduct)await PopupService.ShowPopupAsync(new ProductEditor(ChoosenProduct));
                UserProductsData.SaveProduct(userProduct, DateManager.CurrentDate);
                await Shell.Current.GoToAsync(@$"..");
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
