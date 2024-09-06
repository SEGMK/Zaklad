using Microsoft.CSharp.RuntimeBinder;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad.ViewModel
{
    internal class AddItemByBarcodeViewModel : IAddItemByBarcodeViewModel
    {
        public IPopupService PopupService { get; set; }
        public AddItemByBarcodeViewModel()
        {
            PopupService = ServiceHelper.Current.GetService<IPopupService>();
        }
        public async Task AddItemByBarcode(string barcode)
        {
            try
            {
                IProductDataTemplate choosenProduct = await FoodAPI.GetFoodDataBarcode(barcode);
                IUserProduct userProduct =  (IUserProduct)await PopupService.ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProd(choosenProduct)));
                if (userProduct == null)
                    return;
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
