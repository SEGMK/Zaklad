using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad;

public partial class AddItemByName : ContentPage
{
	public AddItemByName()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.Current.GetService<IAddItemByNameViewModel>();
    }
}