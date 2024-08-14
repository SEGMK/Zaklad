using CommunityToolkit.Maui.Views;
using Zaklad.Interfaces;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class ProductEditor : Popup
{
	public ProductEditor(IProductEditorViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        Close(((IProductEditorViewModel)BindingContext).UserProduct);
    }
} 