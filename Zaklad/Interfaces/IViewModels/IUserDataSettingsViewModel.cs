using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaklad.Interfaces.IViewModels
{
    public interface IUserDataSettingsViewModel : INotifyPropertyChanged
    {
        int Kcal { get; set; }
        int Proteins { get; set; }
        int Fat { get; set; }
        int Sugar { get; set; }
        ICommand SaveChangesCommand { get; }
        ICommand OpenMakroQuestionnaire { get; }
    }
}
