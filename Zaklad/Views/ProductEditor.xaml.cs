using CommunityToolkit.Maui.Views;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class ProductEditor : Popup
{
	public ProductEditor(Product product)
	{
		InitializeComponent();
		BindingContext = new ProductEditorViewModel(product);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        Close();
    }
}