using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using System.Drawing;

namespace Zaklad.Views;

public partial class MauiCamera : Popup
{
	public MauiCamera()
	{
		InitializeComponent();
	}

    private void MyCamera_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        //ImageSource img = ImageSource.FromStream(() => e.Media);
        Bitmap bmp = (Bitmap)Bitmap.FromStream(e.Media);
        MessagingCenter.Send<MauiCamera, Bitmap>(this, "photo", bmp);
        Close();
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        await MyCamera.CaptureImage(CancellationToken.None);
    }
}