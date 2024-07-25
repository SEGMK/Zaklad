using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.ViewModel
{
    public class CalendarPopupViewModel
    {
        public static event EventHandler<DateTime> DateChanged;
        public ICommand DateSelectedCommand => new Command(RiseDateSelectedEvents);
        private void RiseDateSelectedEvents(object date)
        {
            DateChanged?.Invoke(this, (DateTime)date);
        }
    }
}