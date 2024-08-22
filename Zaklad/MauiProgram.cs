using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing.Net.Maui.Controls;

namespace Zaklad
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseBarcodeReader();
            //ViewModels
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<IAddItemByNameViewModel, AddItemByNameViewModel>();
            builder.Services.AddSingleton<IAddItemByBarcodeViewModel, AddItemByBarcodeViewModel>();
            builder.Services.AddSingleton<ICalendarPopupViewModel, CalendarPopupViewModel>();
            builder.Services.AddSingleton<IProductSelectionPopupViewModel, ProductSelectionPopupViewModel>();
            //Sevices
            builder.Services.AddSingleton<IPopupService, Models.PopupService>();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            //Models
            builder.Services.AddTransient<IUserProduct, UserProduct>();
            builder.Services.AddTransient<IProductDataTemplate, ProductDataTemplate>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build(); 
        }
    }
}
