using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad.Views;

public partial class UserDataSettings : ContentPage
{
	public UserDataSettings()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.Current.GetService<IUserDataSettingsViewModel>();
    }
}