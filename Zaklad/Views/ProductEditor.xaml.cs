using CommunityToolkit.Maui.Views;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class ProductEditor : Popup
{
	public ProductEditor(ProductDataTemplate product)
	{
		InitializeComponent();
		BindingContext = new ProductEditorViewModel(product);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        Close(((ProductEditorViewModel)BindingContext).UserProduct);
    }
}