using System.Runtime.CompilerServices;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductTemplateProperty = BindableProperty.Create(nameof(ProductTemplate), typeof(ProductDataTemplate), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        UpdateViewValues((ProductDataTemplate)newValue, bindable);
    });
    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(UserProduct), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        UpdateViewValues((UserProduct)newValue, bindable);
    });
    //make it generic
    private static void UpdateViewValues(ProductDataTemplate product, object bindable)
    {
        var control = (ProductView)bindable;
        //ObservableList is raising events when data deleted. Cuz of that the ListView is passing a null objects to this control - not critical but would be great to fix that
        if (product == null)
            return;
        control.labelKcal.Text = $"Energia: {((decimal)product.Kcal).ToString("F2")} Kcal";
        control.labelCarbohydrates.Text = $"Cukry: {product.Carbohydrates}g";
        control.labelFat.Text = $"Tłuszcze: {product.Fat}g";
        control.labelProteins.Text = $"Białka: {product.Proteins}g";
        control.labelProductName.Text = product.Name;
        if (product.ProductImage != null)
            control.imageProductImage.Source = product.ProductImage;
    }
    private static void UpdateViewValues(UserProduct product, object bindable)
    {
        var control = (ProductView)bindable;
        //ObservableList is raising events when data deleted. Cuz of that the ListView is passing a null objects to this control - not critical but would be great to fix that
        if (product == null)
            return;
        control.labelGramature.Text = $"wartości odżywcze na {product.Gramature}g:";
        control.labelKcal.Text = $"Energia: {((decimal)product.Kcal).ToString("F2")} Kcal";
        control.labelCarbohydrates.Text = $"Cukry: {product.Carbohydrates}g";
        control.labelFat.Text = $"Tłuszcze: {product.Fat}g";
        control.labelProteins.Text = $"Białka: {product.Proteins}g";
        control.labelProductName.Text = product.Name;
        if (product.ProductImage != null)
            control.imageProductImage.Source = product.ProductImage;
    }
    public ProductView()
    {
        InitializeComponent();
    }
    public ProductDataTemplate ProductTemplate
    {
        get => (ProductDataTemplate)GetValue(ProductTemplateProperty);
        set => SetValue(ProductTemplateProperty, value);
    }
    public UserProduct Product
    {
        get => (UserProduct)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }
}