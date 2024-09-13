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
        public async Task<bool> AddItemByBarcode(string barcode)
        {
            try
            {
                IProductDataTemplate choosenProduct = await FoodAPI.GetFoodDataBarcode(barcode);
                bool? redirectToEarlierPage = (bool?)await PopupService.ShowPopupAsync(new ProductEditor(new ProductEditorViewModel_CreateProd(choosenProduct)));
                if (redirectToEarlierPage == true)
                {
                    Shell.Current.GoToAsync(@$"..");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Błąd połączenia z serwerem", $"wystąpił problem podczas próby połączenia z serwerem: {ex.StatusCode}");
            }
            catch (RuntimeBinderException ex)
            {
                ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Brak produktu", "podany produkt nie istnieje w bazie");
            }
            return true;
        }
    }
}
