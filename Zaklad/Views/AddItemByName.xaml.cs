using Zaklad.ViewModel;

namespace Zaklad;

public partial class AddItemByName : ContentPage
{
	public AddItemByName()
	{
		InitializeComponent();
		BindingContext = new AddItemByNameViewModel();

    }
}