using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ServiceHelper.Current.GetService<IMainPageViewModel>();
            ChooseButtonDayFromDay(DateTime.Now.DayOfWeek);
        }
        private void ChooseButtonDayFromDay(DayOfWeek day)
        {
            VisualElement grid = FindByName("DatesGrid") as VisualElement;
            VisualElement element = grid.FindByName(day.ToString() + "TapFrameBackground") as VisualElement;
            element.BackgroundColor = Color.FromHex("#121212");
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
    }

}
