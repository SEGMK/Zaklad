using Microsoft.Maui.Controls.Compatibility;
using System.Collections.Specialized;
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
            ChooseButtonDayFromDay(DateTime.Now.DayOfWeek);
        }
        //EVERYTHING BELOW IS TEMP PIECE OF SHIT
        private void ChooseButtonDayFromDay(DayOfWeek day)
        {
            VisualElement grid = FindByName("DatesGrid") as VisualElement;
            VisualElement element = grid.FindByName(day.ToString() + "TapFrameBackground") as VisualElement;
            element.BackgroundColor = Color.FromHex("#121212");
        }
        //shitty code - learn how to do it correctly
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
