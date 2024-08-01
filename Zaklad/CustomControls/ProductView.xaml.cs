using System.Runtime.CompilerServices;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(Product), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        Product product = (Product)newValue;
        //ObservableList is raising events when data deleted. Cuz of that the ListView is passing a null objects to this control - not critical but would be great to fix that
        if (product == null)
            return;
        control.labelKcal.Text = $"Energia: {((decimal)product.Kcal).ToString("F2")} Kcal";
        control.labelCarbohydrates.Text = $"Cukry: {product.Carbohydrates}g";
        control.labelFat.Text = $"Tłuszcze: {product.Fat}g";
        control.labelProteins.Text = $"Białka: {product.Proteins}g";
        control.labelProductName.Text = product.Name;
        if(product.ProductImage != null)
            control.imageProductImage.Source = product.ProductImage;
    });
    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(Product), typeof(Product), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        control.MyTapGestrueRecognizer.Command = (ICommand)newValue;
    });
    public ProductView()
    {
        InitializeComponent();
    }
    public Product Product
    {
        get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
}