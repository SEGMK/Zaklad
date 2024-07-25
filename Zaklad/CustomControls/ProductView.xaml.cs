using System.Runtime.CompilerServices;

namespace Zaklad;

public partial class ProductView : ContentView
{
    //this is probably done to easly extened possibilities in case of property value changed. Can be done with just default properties.
    public static readonly BindableProperty ProductNameProperty = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(ProductView), "product_name", propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ProductView)bindable;
            if (!string.IsNullOrWhiteSpace((string)newValue))
            {
                control.labelProductName.TextColor = Color.FromHex("#575757");
                control.labelProductName.Text = (string)newValue;
            }
            else
            {
                control.labelProductName.TextColor = Color.FromHex("#4d0000");
                control.labelProductName.Text = "Brak nazwy";
            }
        });
    public static readonly BindableProperty ProteinsProperty = BindableProperty.Create(nameof(Proteins), typeof(decimal), typeof(ProductView), 0m, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        control.labelProteins.Text = $"Białka: {newValue}g";
    });
    public static readonly BindableProperty FatProperty = BindableProperty.Create(nameof(Fat), typeof(decimal), typeof(ProductView), 0m, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        control.labelFat.Text = $"Tłuszcze: {newValue}g";
    });
    public static readonly BindableProperty CarbohydratesProperty = BindableProperty.Create(nameof(Carbohydrates), typeof(decimal), typeof(ProductView), 0m, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        control.labelCarbohydrates.Text = $"Cukry: {newValue}g";
    });
    public static readonly BindableProperty ProductImageSourceProperty = BindableProperty.Create(nameof(ProductImageSource), typeof(ImageSource), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        if ((ImageSource)newValue != null)
            control.imageProductImage.Source = (ImageSource)newValue;
    });
    public static readonly BindableProperty KcalProperty = BindableProperty.Create(nameof(Kcal), typeof(decimal), typeof(ProductView), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (ProductView)bindable;
        string formatedValue = ((decimal)newValue).ToString("F2");
        control.labelKcal.Text = $"Energia: {formatedValue} Kcal";
    });
    public ProductView()
    {
        InitializeComponent();
    }
    public string ProductName
    {
        get => (string)GetValue(ProductNameProperty);
        set => SetValue(ProductNameProperty, value);
    }
    public decimal Proteins
    {
        get => (decimal)GetValue(ProteinsProperty);
        set => SetValue(ProteinsProperty, value);
    }
    public decimal Fat
    {
        get => (decimal)GetValue(FatProperty);
        set => SetValue(FatProperty, value);
    }
    public decimal Carbohydrates
    {
        get => (decimal)GetValue(CarbohydratesProperty);
        set => SetValue(CarbohydratesProperty, value);
    }
    public ImageSource ProductImageSource
    {
        get => (ImageSource)GetValue(ProductImageSourceProperty);
        set => SetValue(ProductImageSourceProperty, value);
    }
    public decimal Kcal
    {
        get => (decimal)GetValue(ProductImageSourceProperty);
        set => SetValue(ProductImageSourceProperty, value);
    }
}