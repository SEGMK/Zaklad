using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.Interfaces.IViewModels
{
    internal interface ICalendarPopupViewModel
    {
        public static event EventHandler<DateTime> DateChanged;
        public ICommand DateSelectedCommand { get; }
    }
}
