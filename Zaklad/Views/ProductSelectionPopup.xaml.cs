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
        ((IProductSelectionPopupViewModel)BindingContext).NavigateToAddItemByName.Execute(null);
        Close();
    }
    private void NavigateToAddItemByBarcode(object sender, EventArgs e)
    {
        ((IProductSelectionPopupViewModel)BindingContext).NavigateToAddItemByBarcode.Execute(null);
        Close();
    }
    private void NavigateToCreateCustomProduct(object sender, EventArgs e)
    {
        ((IProductSelectionPopupViewModel)BindingContext).NavigateToCreateCustomProduct.Execute(null);
        Close();
    }
}