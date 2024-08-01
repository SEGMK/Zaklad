namespace Zaklad
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Zaklad.MainPage), typeof(Zaklad.MainPage));
        }
    }
}