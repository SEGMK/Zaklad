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
        AddDecisionButtons();
    }
    private void AddDecisionButtons()
    {
        foreach (Button i in ((IProductEditorViewModel)BindingContext).DecisionButtonsCollection)
        {
            i.Clicked += (s, e) => Close();
            DecisionButtons.Add(i);
        }
    }
} 