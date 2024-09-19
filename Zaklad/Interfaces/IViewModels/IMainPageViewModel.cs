using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad.Interfaces.IViewModels
{
    public interface IMainPageViewModel : INotifyPropertyChanged
    {
        DateManager DateManager { get; }
        decimal Kcal { get; }
        decimal Proteins { get; }
        decimal Fat { get; }
        decimal Carbohydrates { get; }
        IPopupService PopupService { get; }
        IMakroSettingsData UserMakroIntakeSettings { get; }
        ICommand ChangeDateOfWeekCommand { get; }
        ICommand ShowCalendarCommand { get; }
        ICommand OpenProductEditorCommand { get; }
        ObservableCollection<IUserProduct> Products { get; }
        void GetProductsCollection();
        void UpdateUserMakro();
    }
}
