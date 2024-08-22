using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing;
using ZXing.Net.Maui.Controls;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Zaklad
{
    public partial class AddItemByBarcode : ContentPage
	{
        AddItemByBarcodeViewModel Context;
        public AddItemByBarcode()
		{
			InitializeComponent();
            Context = new AddItemByBarcodeViewModel(ServiceHelper.Current.GetService<IPopupService>());
		}

        private async void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            CameraBarcodeReaderView cameraBarcodeReader = sender as CameraBarcodeReaderView;
            var first = e.Results?.FirstOrDefault();
            if (first == null)
                return;
            cameraBarcodeReader.IsDetecting = false;
            await Context.AddItemByBarcode(first.Value);
            cameraBarcodeReader.IsDetecting = true;
        }
    }
}