using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing.Net.Maui;

namespace Zaklad
{
    public partial class AddItemByBarcode : ContentPage
	{
        AddItemByBarcodeViewModel Context;
        public AddItemByBarcode()
		{
            InitializeComponent();
            BarcodeReaderOptions barcodeReaderOptions = new BarcodeReaderOptions()
            {
                AutoRotate = false,
                TryHarder = true,
                TryInverted = false,
                Formats = BarcodeFormat.Ean13,
                Multiple = false
            };
            this.cameraView.Options = barcodeReaderOptions;
            BindingContext = ServiceHelper.Current.GetService<IAddItemByBarcodeViewModel>();
        }

        private async void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results.First();
            if (first == null)
                return;
            this.cameraView.IsDetecting = false;
            await ((IAddItemByBarcodeViewModel)BindingContext).AddItemByBarcode(first.Value);
            Thread.Sleep(3000); //its cuz await GoToAsync() has a bug and returns before pages are chenged
            this.cameraView.IsDetecting = true;
        }
    }
}