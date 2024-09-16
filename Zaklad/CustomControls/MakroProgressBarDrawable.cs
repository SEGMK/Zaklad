using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.CustomControls
{
    public class MakroProgressBarDrawable : BindableObject, IDrawable
    {
        public static readonly BindableProperty BarProgressPercentageProperty = BindableProperty.Create(nameof(BarProgressPercentage), typeof(float), typeof(MakroProgressBarDrawable));
        public static readonly BindableProperty ProgressBarColorProperty = BindableProperty.Create(nameof(ProgressBarColor), typeof(Color), typeof(MakroProgressBarDrawable));
        public static readonly BindableProperty ProgressBarWidthProperty = BindableProperty.Create(nameof(ProgressBarWidth), typeof(int), typeof(MakroProgressBarDrawable));
        public static readonly BindableProperty ProgressBarHeightProperty = BindableProperty.Create(nameof(ProgressBarHeight), typeof(int), typeof(MakroProgressBarDrawable));
        /// <summary>
        /// accepts values <0, 2>. 0: empty, 1: full, 2: full red
        /// </summary>
        public float BarProgressPercentage
        {
            get => (float)GetValue(BarProgressPercentageProperty);
            set => SetValue(BarProgressPercentageProperty, value);
        }
        public Color ProgressBarColor
        {
            get => (Color)GetValue(ProgressBarColorProperty);
            set => SetValue(ProgressBarColorProperty, value);
        }
        public int ProgressBarWidth
        {
            get => (int)GetValue(ProgressBarWidthProperty);
            set => SetValue(ProgressBarWidthProperty, value);
        }
        public int ProgressBarHeight
        {
            get => (int)GetValue(ProgressBarHeightProperty);
            set => SetValue(ProgressBarHeightProperty, value);
        }
        private Color OverflowColor = Color.Parse("Red");
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeSize = 2;
            canvas.StrokeColor = ProgressBarColor;
            int cornerRadius = 10;
            canvas.DrawRectangle(0, 0, ProgressBarWidth, ProgressBarHeight);
            if (BarProgressPercentage < 0)
                BarProgressPercentage = 0;
            else if (2 < BarProgressPercentage)
                BarProgressPercentage = 2;
            int integralPart = (int)BarProgressPercentage;
            float fractionalPart = (float)BarProgressPercentage - integralPart;
            canvas.FillColor = ProgressBarColor;
            canvas.FillRectangle(0, 0, cornerRadius, ProgressBarHeight);
            if (BarProgressPercentage <= 1)
                canvas.FillRoundedRectangle(0, 0, ProgressBarWidth * BarProgressPercentage, ProgressBarHeight, cornerRadius);
            if (1 <= BarProgressPercentage)
            {
                canvas.FillRectangle(ProgressBarWidth - cornerRadius, 0, cornerRadius, ProgressBarHeight);
            }
            if (1 < BarProgressPercentage)
            {
                canvas.FillRoundedRectangle(0, 0, ProgressBarWidth, ProgressBarHeight, cornerRadius);
                canvas.FillColor = OverflowColor;
                canvas.FillRoundedRectangle(0, 0, ProgressBarWidth * (BarProgressPercentage - 1), ProgressBarHeight, cornerRadius);
                canvas.FillRectangle(0, 0, cornerRadius, ProgressBarHeight);
            }
            if (BarProgressPercentage == 2)
            {
                canvas.FillRectangle(ProgressBarWidth - cornerRadius, 0, cornerRadius, ProgressBarHeight);
            }
        }
    }
}
