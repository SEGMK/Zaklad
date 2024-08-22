using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad.Views;

public partial class ProductSelectionPopup : Popup
{ 
	public ProductSelectionPopup()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.Current.GetService<IProductSelectionPopupViewModel>();
    }

    private void NavigateToAddItemByName(object sender, EventArgs e)
    {
        ((ProductSelectionPopupViewModel)BindingContext).NavigateToAddItemByName.Execute(null);
        Close();
    }
    private void NavigateToAddItemByBarcode(object sender, EventArgs e)
    {
        ((ProductSelectionPopupViewModel)BindingContext).NavigateToAddItemByBarcode.Execute(null);
        Close();
    }
}