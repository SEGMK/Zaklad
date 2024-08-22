using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class CalendarPopup : Popup
{
	public CalendarPopup()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.Current.GetService<ICalendarPopupViewModel>();
	}
}