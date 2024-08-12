using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.Interfaces
{
    public interface IMainPageViewModel : INotifyPropertyChanged
    {
        DateManager DateManager { get; }
        public string Proteins { get; }
        string Fat { get; }
        IPopupService PopupService { get; }
        string Carbohydrates { get; }
        ICommand ChangeDateOfWeekCommand { get; }
        ICommand ShowCalendarCommand { get; }
        RangeObservableCollection<IUserProduct> Products { get; }
    }
}
