using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    //not static cuz of binding properties that can not be static in order to be shown in XAML
    public class DateManager : INotifyPropertyChanged
    {
        public static DateTime CurrentDate { get; private set; } = DateTime.Now;
        public const string DateFormat = "dd.MM.yyyy";
        //looping throught properties will kick my ass in the future, this code has to be repetetive
        public void ChangeCurrentDate(DateTime date)
        {
            CurrentDate = date;
            int i = 0;
            int day = (int)date.DayOfWeek - 1;
            if(day == -1)
                day = 6;
            Monday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Tuesday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Wednesday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Thursday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Friday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Saturday = date.Date.AddDays(i - day).ToString(DateFormat);
            i++;
            Sunday = date.Date.AddDays(i - day).ToString(DateFormat);
        }
        private string _monday;
        public string Monday { get { return _monday; } set { _monday = value; OnPropertyChange(nameof(Monday)); } }
        private string _tuesday;
        public string Tuesday { get { return _tuesday; } set { _tuesday = value; OnPropertyChange(nameof(Tuesday)); } }
        private string _wednesday;
        public string Wednesday { get { return _wednesday; } set { _wednesday = value; OnPropertyChange(nameof(Wednesday)); } }
        private string _thursday;
        public string Thursday { get { return _thursday; } set { _thursday = value; OnPropertyChange(nameof(Thursday)); } }
        private string _friday;
        public string Friday { get { return _friday; } set { _friday = value; OnPropertyChange(nameof(Friday)); } }
        private string _saturday;
        public string Saturday { get { return _saturday; } set { _saturday = value; OnPropertyChange(nameof(Saturday)); } }
        private string _sunday;
        public string Sunday { get { return _sunday; } set { _sunday = value; OnPropertyChange(nameof(Sunday)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
