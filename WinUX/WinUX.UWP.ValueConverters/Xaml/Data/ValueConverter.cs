namespace WinUX.Xaml.Data
{
    using System;
    using System.Globalization;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class ValueConverter : DependencyObject
    {
        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            nameof(Converter),
            typeof(IValueConverter),
            typeof(ValueConverter),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ConverterParameterProperty =
            DependencyProperty.Register(
                nameof(ConverterParameter),
                typeof(object),
                typeof(ValueConverter),
                new PropertyMetadata(default(object)));

        public object ConverterParameter
        {
            get
            {
                return this.GetValue(ConverterParameterProperty);
            }
            set
            {
                this.SetValue(ConverterParameterProperty, value);
            }
        }

        public IValueConverter Converter
        {
            get
            {
                return (IValueConverter)this.GetValue(ConverterProperty);
            }
            set
            {
                this.SetValue(ConverterProperty, value);
            }
        }

        public object Convert(object value, Type targetType)
        {
            return this.Converter?.Convert(
                value,
                targetType,
                this.ConverterParameter,
                CultureInfo.CurrentCulture.ToString());
        }

        public object ConvertBack(object value, Type targetType)
        {
            return this.Converter?.ConvertBack(
                value,
                targetType,
                this.ConverterParameter,
                CultureInfo.CurrentCulture.ToString());
        }
    }
}