using CommunityToolkit.Maui.Views;
using Zaklad.CustomControls;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;

namespace Zaklad;

public partial class ProductEditor : Popup
{
    public ProductEditor(IProductEditorViewModel viewModel)
	{
        SetUpColorsInCollection();
        InitializeComponent();
        BindingContext = viewModel;
        AddDecisionButtons();
    }
    private void SetUpColorsInCollection()
    {
        App.Current.Resources.TryGetValue("KcalColor", out var kcalColor);
        App.Current.Resources.TryGetValue("ProteinsColor", out var proteinsColor);
        App.Current.Resources.TryGetValue("FatColor", out var fatColor);
        App.Current.Resources.TryGetValue("SugarColor", out var sugarColor);
        EditableTextColors.Add("labelEditableKcal", (Color)kcalColor);
        EditableTextColors.Add("labelEditableProteins", (Color)proteinsColor);
        EditableTextColors.Add("labelEditableFat", (Color)fatColor);
        EditableTextColors.Add("labelEditableCarbohydrates", (Color)sugarColor);
    }
    Dictionary<string, Color> EditableTextColors = new Dictionary<string, Color>();
    private void AddDecisionButtons()
    {
        foreach (ButtonWithDecision i in ((IProductEditorViewModel)BindingContext).DecisionButtonsCollection)
        {
            i.Clicked += (s, e) => Close(i.RedirectToEarlierPage);
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