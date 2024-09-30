using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad.Models
{
    internal class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message, string cancel = "OK")
        {
            Application.Current.MainPage.Dispatcher.Dispatch(() => ShowAlertAsync(title, message, cancel).FireAndForgetSafeAsync());
        }
        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
