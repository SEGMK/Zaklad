using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing;

namespace Zaklad
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        //this 4 properties exist cuz Im stupid and I can't fix bug that locks setters of MakroProgressBarDrawable after changing values of properties from inside
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private float _proteins;
        public float ProgressBarProteins
        {
            get
            {
                float result = (float)((IMainPageViewModel)BindingContext).Proteins / ((IMainPageViewModel)BindingContext).UserMakroIntakeSettings.Proteins;
                if(result < 0)
                    result = 0;
                else if(2 < result)
                    result = 2;
                return result;
            }
        }
        private float _fat = 0;
        public float ProgressBarFat
        {
            get
            {
                float result = (float)((IMainPageViewModel)BindingContext).Fat / ((IMainPageViewModel)BindingContext).UserMakroIntakeSettings.Fat;
                if (result < 0)
                    result = 0;
                else if (2 < result)
                    result = 2;
                return result;
            }
        }
        private float _carbohydrates = 0;
        public float ProgressBarCarbohydrates
        {
            get
            {
                float result = (float)((IMainPageViewModel)BindingContext).Carbohydrates / ((IMainPageViewModel)BindingContext).UserMakroIntakeSettings.Carbohydrates;
                if (result < 0)
                    result = 0;
                else if (2 < result)
                    result = 2;
                return result;
            }
        }
        private float _kcal = 0;
        public float ProgressBarKcal
        {
            get
            {
                float result = (float)((IMainPageViewModel)BindingContext).Kcal / ((IMainPageViewModel)BindingContext).UserMakroIntakeSettings.Kcal;
                if (result < 0)
                    result = 0;
                else if (2 < result)
                    result = 2;
                return result;
            }
        }
        private void UpdateProgressBarData()
        {
            OnPropertyChange(nameof(ProgressBarProteins));
            OnPropertyChange(nameof(ProgressBarFat));
            OnPropertyChange(nameof(ProgressBarKcal));
            OnPropertyChange(nameof(ProgressBarCarbohydrates));
        }
        public MainPage()
        {
            BindingContext = ServiceHelper.Current.GetService<IMainPageViewModel>();
            ((IMainPageViewModel)BindingContext).PropertyChanged += (s, e) => UpdateProgressBarData();
            InitializeComponent();
            ChangeDaysButtonsBackgroundOnDateChanged(null, DateTime.Now);
            CalendarPopupViewModel.DateChanged += ChangeDaysButtonsBackgroundOnDateChanged;
        }
        private void ChangeDaysButtonsBackgroundOnDateChanged(object sender, DateTime date)
        {
            App.Current.Resources.TryGetValue("StandardBackground_Shade_3", out var standardBackground_Shade_3);
            VisualElement button = FindByName(date.DayOfWeek.ToString() + "TapFrameBackground") as VisualElement;
            Microsoft.Maui.Controls.Grid grid = button.Parent as Microsoft.Maui.Controls.Grid;
            foreach (VisualElement i in grid.Children)
            {
                i.BackgroundColor = (Color)standardBackground_Shade_3;
            }
            button.BackgroundColor = Color.FromHex("#121212");
        }
        private void ChangeDaysButtonsBackgroundOnPress(object sender, EventArgs e)
        {
            App.Current.Resources.TryGetValue("StandardBackground_Shade_3", out var standardBackground_Shade_3);
            VisualElement clickedElement = sender as VisualElement;
            Microsoft.Maui.Controls.Grid grid = clickedElement.Parent as Microsoft.Maui.Controls.Grid;
            foreach (VisualElement i in grid.Children)
            {
                i.BackgroundColor = (Color)standardBackground_Shade_3;
            }
            clickedElement.BackgroundColor = Color.FromHex("#121212");
        }
        protected override void OnAppearing()
        {
            ((IMainPageViewModel)BindingContext).GetProductsCollection();
            ((IMainPageViewModel)BindingContext).UpdateUserMakro();
            base.OnAppearing();
        }
    }

}
