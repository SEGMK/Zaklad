using System.Runtime.CompilerServices;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductTemplateProperty = BindableProperty.Create(nameof(ProductTemplate), typeof(IProductDataTemplate), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        IProductDataTemplate product = (IProductDataTemplate)newValue;
        //ObservableList is raising events when data deleted. Cuz of that the ListView is passing a null objects to this control - not critical but would be great to fix that
        if (product == null)
            return;
        SetProductTemplateData(control, product);
        SetUserProductPartVisible(control, false);
    });
    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(IUserProduct), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        IUserProduct product = (IUserProduct)newValue;
        //ObservableList is raising events when data deleted. Cuz of that the ListView is passing a null objects to this control - not critical but would be great to fix that
        if (product == null)
            return;
        SetProductTemplateData(control, product.ProductTemplate);
        SetUserProductPartVisible(control, true);
        control.labelGramature.Text = $"Spożyta porcja produktu: {product.Gramature}g";
        control.labelKcal.Text = $"Energia: {((decimal)product.Kcal).ToString("F2")} Kcal";
        control.labelCarbohydrates.Text = $"Cukry: {product.Carbohydrates}g";
        control.labelFat.Text = $"Tłuszcze: {product.Fat}g";
        control.labelProteins.Text = $"Białka: {product.Proteins}g";
        if (product.Kcal < 0)
            control.labelKcal.TextColor = Color.Parse("Red");
        if (product.Carbohydrates < 0)
            control.labelCarbohydrates.TextColor = Color.Parse("Red");
        if (product.Proteins < 0)
            control.labelProteins.TextColor = Color.Parse("Red");
        if (product.Fat < 0)
            control.labelFat.TextColor = Color.Parse("Red");
    });

    private static void SetProductTemplateData(ProductView control, IProductDataTemplate product)
    {
        control.labelKcal_100g.Text = $"Energia: {((decimal)product.Kcal).ToString("F2")} Kcal";
        control.labelCarbohydrates_100g.Text = $"Cukry: {product.Carbohydrates}g";
        control.labelFat_100g.Text = $"Tłuszcze: {product.Fat}g";
        control.labelProteins_100g.Text = $"Białka: {product.Proteins}g";
        control.labelProductName.Text = product.Name;
        if (product.Kcal < 0)
            control.labelKcal_100g.TextColor = Color.Parse("Red");
        if (product.Carbohydrates < 0)
            control.labelCarbohydrates_100g.TextColor = Color.Parse("Red");
        if (product.Proteins < 0)
            control.labelProteins_100g.TextColor = Color.Parse("Red");
        if (product.Fat < 0)
            control.labelFat_100g.TextColor = Color.Parse("Red");
        if (product.ProductImage != null)
            control.imageProductImage.Source = product.ProductImage;
    }
    private static void SetUserProductPartVisible(ProductView control, bool turnVisible)
    {
        control.labelFat.IsVisible = turnVisible;
        control.labelCarbohydrates.IsVisible = turnVisible;
        control.labelGramature.IsVisible = turnVisible;
        control.labelProteins.IsVisible = turnVisible;
        control.labelKcal.IsVisible = turnVisible;
    }
    public ProductView()
    {
        InitializeComponent();
    }
    public IProductDataTemplate ProductTemplate
    {
        get => (IProductDataTemplate)GetValue(ProductTemplateProperty);
        set => SetValue(ProductTemplateProperty, value);
    }
    public IUserProduct Product
    {
        get => (IUserProduct)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }
}