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
    Dictionary<string, Color> EditableTextColors = new Dictionary<string, Color>()
    {
        { "labelEditableKcal", Color.Parse("White") },
        { "labelEditableProteins", Color.Parse("Blue") },
        { "labelEditableFat", Color.Parse("Yellow") },
        { "labelEditableCarbohydrates", Color.Parse("White") }
    };
    private void AddDecisionButtons()
    {
        foreach (Button i in ((IProductEditorViewModel)BindingContext).DecisionButtonsCollection)
        {
            i.Clicked += (s, e) => Close();
            DecisionButtons.Add(i);
        }
    }
    private void ValueCheckEntry(object sender, EventArgs e)
    { 
        Entry entry = sender as Entry;
        if (entry == null)
            throw new Exception("Method is used for uncorrect VisualElement");
        decimal entryValue;
        if (!decimal.TryParse(entry.Text, out entryValue))
            return;
        if (entryValue < 0)
            entry.TextColor = Color.Parse("Red");
        else
            entry.TextColor = EditableTextColors[entry.ClassId];
    }
} 