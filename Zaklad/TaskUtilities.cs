using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaklad.Interfaces;

namespace Zaklad
{
    public static class TaskUtilities
    {
        //John Thiriet - for more informations
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            if (handler == null)
                handler = new ExceptionMessenger();
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
