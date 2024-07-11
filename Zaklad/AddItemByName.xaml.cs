using ZXing;

namespace Zaklad
{
	public partial class AddItemByName : ContentPage
	{
        AddItemByNameViewModel Context;
        public AddItemByName()
		{
			InitializeComponent();
            Context = new AddItemByNameViewModel();
		}

        private void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results?.FirstOrDefault();
            if (first == null)
                return;
            Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Barcode detected", first.Value, "OK");
            });
            Context.AddItemByBarcode(first.Value);
        }
    }
}