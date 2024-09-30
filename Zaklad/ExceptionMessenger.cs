using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad
{
    class ExceptionMessenger : IErrorHandler
    {
        public void HandleError(Exception ex) => ServiceHelper.Current.GetService<IAlertService>().ShowAlert("Error", $"Wystąpił nieoczekiwny błąd: {ex.Message}");
    }
}
