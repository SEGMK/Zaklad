using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class ProductEditor : Popup
{
	public ProductEditor(IProductDataTemplate product)
	{
		InitializeComponent();
		BindingContext = new ProductEditorViewModel(product);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        Close(((ProductEditorViewModel)BindingContext).UserProduct);
    }
}