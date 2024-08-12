using System.Runtime.CompilerServices;
using System.Windows.Input;
using Zaklad.Interfaces;
using Zaklad.Models;

namespace Zaklad;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductTemplateProperty = BindableProperty.Create(nameof(ProductTemplate), typeof(IProductDataTemplate), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        UpdateViewValues((IProductDataTemplate)newValue, bindable);
    });
    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(IUserProduct), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        UpdateViewValues((IUserProduct)newValue, bindable);
    });
    //make it generic
    private static void UpdateViewValues(IProductDataTemplate product, object bindable)
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
    private static void UpdateViewValues(IUserProduct product, object bindable)
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