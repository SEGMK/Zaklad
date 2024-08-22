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
            VisualElement button = FindByName(date.DayOfWeek.ToString() + "TapFrameBackground") as VisualElement;
            Microsoft.Maui.Controls.Grid grid = button.Parent as Microsoft.Maui.Controls.Grid;
            foreach (VisualElement i in grid.Children)
            {
                i.BackgroundColor = Color.FromHex("#1c1c1c");
            }
            button.BackgroundColor = Color.FromHex("#121212");
        }
        private void ChangeDaysButtonsBackgroundOnPress(object sender, EventArgs e)
        {
            VisualElement clickedElement = sender as VisualElement;
            Microsoft.Maui.Controls.Grid grid = clickedElement.Parent as Microsoft.Maui.Controls.Grid;
            foreach (VisualElement i in grid.Children)
            { 
                i.BackgroundColor = Color.FromHex("#1c1c1c");
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
