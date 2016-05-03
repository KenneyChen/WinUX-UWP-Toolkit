// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeSliderFlyout.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the flyout that appears when changing the RangeSlider range.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class RangeSliderFlyout : Control
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(RangeSliderFlyout),
            new PropertyMetadata(string.Empty));

        public RangeSliderFlyout()
        {
            this.DefaultStyleKey = typeof(RangeSliderFlyout);
        }

        public string Value
        {
            get
            {
                return (string)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }
    }
}