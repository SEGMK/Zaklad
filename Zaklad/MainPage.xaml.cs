using Microsoft.Maui.Controls.Compatibility;
using System.Collections.Specialized;

namespace Zaklad
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            ChooseButtonDayFromDay(DateTime.Now.DayOfWeek);
        }
        private void ChooseButtonDayFromDay(DayOfWeek day)
        { 
            Button butt = FindByName(day.ToString() + "Button") as Button;
            butt.BackgroundColor = Color.FromHex("#121212");
        }
        private void ChangeButtonBackgroundOnPress(object sender, EventArgs e)
        {
            Button butt = sender as Button;
            Microsoft.Maui.Controls.Layout container = butt.Parent as Microsoft.Maui.Controls.Layout;
            foreach (Button i in container)
            {
                i.BackgroundColor = Color.FromHex("#212121");
            }
            Color foreColor = butt.TextColor;
            butt.BackgroundColor = Color.FromHex("#121212");
        }
    }

}
