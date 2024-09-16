using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ServiceHelper.Current.GetService<IMainPageViewModel>();
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
            base.OnAppearing();
        }
    }

}
