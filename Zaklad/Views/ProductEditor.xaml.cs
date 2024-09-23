using CommunityToolkit.Maui.Views;
using Zaklad.CustomControls;
using Zaklad.Interfaces.IViewModels;
using Zaklad.Models;
using Zaklad.ViewModel;
using ZXing;

namespace Zaklad;

public partial class ProductEditor : Popup
{
    public ProductEditor(IProductEditorViewModel viewModel)
	{
        SetUpColorsInCollection();
        BindingContext = viewModel;
        InitializeComponent();
        AddDecisionButtons();
        
    }
    //private void CheckImageLayoutMode()
    //{
    //    ImageLayout.Clear();
    //    ImageSource img = ((IProductEditorViewModel)BindingContext).ProductImage;
    //    ImageLayout.Add(new Image()
    //    {
    //        HorizontalOptions = LayoutOptions.Center,
    //        VerticalOptions = LayoutOptions.Center,
    //        Source = ((IProductEditorViewModel)BindingContext).ProductImage
    //    });
    //    if (img as FileImageSource != null)
    //    {
    //        FileImageSource image = img as FileImageSource;
    //        if (image.File.ToString() == "no_product.png")
    //        {
    //            Button btn = new Button()
    //            {
    //                BackgroundColor = Color.Parse("ForestGreen"),
    //                Text = "Dodaj zdjęcie",
    //                FontAttributes = FontAttributes.Bold,
    //                FontSize = 16,
    //                TextColor = Color.Parse("White"),
    //                WidthRequest = 200,
    //            };
    //            btn.Pressed += (s, e) => TakeProductPhoto();
    //            ImageLayout.Add(btn);
    //        }
    //    }
    //}
    //private async Task TakeProductPhoto()
    //{
    //    await ((IProductEditorViewModel)BindingContext).TakeNewPhotoCommand.ExecuteAsync();
    //    CheckImageLayoutMode();
    //}
    private void SetUpColorsInCollection()
    {
        App.Current.Resources.TryGetValue("KcalColor", out var kcalColor);
        App.Current.Resources.TryGetValue("ProteinsColor", out var proteinsColor);
        App.Current.Resources.TryGetValue("FatColor", out var fatColor);
        App.Current.Resources.TryGetValue("CarbohydratesColor", out var CarbohydratesColor);
        EditableTextColors.Add("labelEditableKcal", (Color)kcalColor);
        EditableTextColors.Add("labelEditableProteins", (Color)proteinsColor);
        EditableTextColors.Add("labelEditableFat", (Color)fatColor);
        EditableTextColors.Add("labelEditableCarbohydrates", (Color)CarbohydratesColor);
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