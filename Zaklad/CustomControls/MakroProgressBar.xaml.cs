using System.Runtime.CompilerServices;

namespace Zaklad.CustomControls;

public partial class MakroProgressBar : ContentView
{
    public MakroProgressBar()
    {
        InitializeComponent();
        ProgressBarWidth = 100;
        ProgressBarHeight = 50;
        PercentageOfProgress = 0;
        ProgressBarColor = Color.Parse("Blue");
    }
    public static readonly BindableProperty ProgressBarColorProperty = BindableProperty.Create(nameof(ProgressBarColor), typeof(Color), typeof(MakroProgressBar), null);
    public static readonly BindableProperty PercentageOfProgressProperty = BindableProperty.Create(nameof(PercentageOfProgress), typeof(float), typeof(MakroProgressBar), null);
    public static readonly BindableProperty ProgressBarWidthProperty = BindableProperty.Create(nameof(ProgressBarWidth), typeof(int), typeof(MakroProgressBar), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (MakroProgressBar)bindable;
        control.ProgressBar.WidthRequest = (int)newValue;
    });
    public static readonly BindableProperty ProgressBarHeightProperty = BindableProperty.Create(nameof(ProgressBarHeight), typeof(int), typeof(MakroProgressBar), null, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (MakroProgressBar)bindable;
        control.ProgressBar.HeightRequest = (int)newValue;
    });
    public Color ProgressBarColor
    {
        get => (Color)GetValue(ProgressBarColorProperty);
        set
        {
            SetValue(ProgressBarColorProperty, value);
            ProgressBar.Invalidate();
        }
    }
    public float PercentageOfProgress
    {
        get => (float)GetValue(PercentageOfProgressProperty);
        set
        {
            SetValue(PercentageOfProgressProperty, value);
            ProgressBar.Invalidate();
        }
    }
    public int ProgressBarWidth
    {
        get => (int)GetValue(ProgressBarWidthProperty);
        set
        {
            SetValue(ProgressBarWidthProperty, value);
            ProgressBar.Invalidate();
        }
    }
    public int ProgressBarHeight
    {
        get => (int)GetValue(ProgressBarHeightProperty);
        set
        {
            SetValue(ProgressBarHeightProperty, value);
            ProgressBar.Invalidate();
        }
    }
}