using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Zaklad.Interfaces;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing.Net.Maui.Controls;

namespace Zaklad
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseBarcodeReader();
            builder.Services.AddSingleton<IPopupService, Models.PopupService>();
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IAddItemByNameViewModel, AddItemByNameViewModel>();
            builder.Services.AddTransient<IUserProduct, UserProduct>();
            builder.Services.AddTransient<IProductDataTemplate, ProductDataTemplate>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build(); 
        }
    }
}
