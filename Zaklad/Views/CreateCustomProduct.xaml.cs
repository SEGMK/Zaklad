using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;

namespace Zaklad.Views;

public partial class CreateCustomProduct : ContentPage
{
	public CreateCustomProduct()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.Current.GetService<ICreateCustomProductViewModel>();
	}
}