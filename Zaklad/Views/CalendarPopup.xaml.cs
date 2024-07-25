using CommunityToolkit.Maui.Views;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class CalendarPopup : Popup
{
	public CalendarPopup()
	{
		InitializeComponent();
		BindingContext = new CalendarPopupViewModel();
	}
}