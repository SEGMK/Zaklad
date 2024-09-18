using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.Views;

namespace Zaklad.ViewModel
{
    public class UserDataSettingsViewModel : IUserDataSettingsViewModel
    {
        public UserDataSettingsViewModel()
        {
            var makro = UserSettingsCRUD.GetUserSettingsData();
            Kcal = makro.Kcal;
            Proteins = makro.Proteins;
            Fat = makro.Fat;
            Sugar = makro.Sugar;
        }
        private int _kcal;
        public int Kcal
        {
            get => _kcal;
            set
            {
                _kcal = value;
                OnPropertyChange(nameof(Kcal));
            }
        }
        private int _proteins;
        public int Proteins
        {
            get => _proteins;
            set
            {
                _proteins = value;
                OnPropertyChange(nameof(Proteins));
            }
        }
        private int _fat;
        public int Fat
        {
            get => _fat;
            set
            {
                _fat = value;
                OnPropertyChange(nameof(Fat));
            }
        }
        private int _sugar;
        public int Sugar
        {
            get => _sugar;
            set
            {
                _sugar = value;
                OnPropertyChange(nameof(Sugar));
            }
        }

        public ICommand SaveChangesCommand => new Command(() =>
        {
            IMakroSettingsData makro = ServiceHelper.Current.GetService<IMakroSettingsData>();
            makro.Kcal = Kcal;
            makro.Proteins = Proteins;
            makro.Fat = Fat;
            makro.Sugar = Sugar;
            UserSettingsCRUD.SaveUserSettingsData(makro);
        });

        public ICommand OpenMakroQuestionnaire => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
