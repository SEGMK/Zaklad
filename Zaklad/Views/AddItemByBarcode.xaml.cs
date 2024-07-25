using CommunityToolkit.Maui.Views;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing;
using ZXing.Net.Maui.Controls;

namespace Zaklad
{
    public partial class AddItemByBarcode : ContentPage
	{
        AddItemByBarcodeViewModel Context;
        public AddItemByBarcode()
		{
			InitializeComponent();
            Context = new AddItemByBarcodeViewModel();
		}

        private void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            CameraBarcodeReaderView cameraBarcodeReader = sender as CameraBarcodeReaderView;
            var first = e.Results?.FirstOrDefault();
            if (first == null)
                return;
            Context.AddItemByBarcode(first.Value, cameraBarcodeReader);
        }
    }
}