using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class AddItemByName : ContentPage
{
	public AddItemByName()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.Current.GetService<IAddItemByNameViewModel>();
    }
}