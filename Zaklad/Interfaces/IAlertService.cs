using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Interfaces
{
    internal interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel = "OK");
        void ShowAlert(string title, string message, string cancel = "OK");
    }
}
