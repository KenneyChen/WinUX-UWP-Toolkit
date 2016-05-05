// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiIValueConverterConverter.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines a converter that supports converting a value through multiple ValueConverters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Converters
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    using WinUX.Collections;

    using System;
    using System.Linq;

    /// <summary>
    /// Defines a converter that supports converting a value through multiple IValueConverters.
    /// </summary>
    public class MultiIValueConverterConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty ConverterCollectionProperty =
            DependencyProperty.Register(
                nameof(ConverterCollection),
                typeof(ValueConverterCollection),
                typeof(MultiIValueConverterConverter),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the value converter collection.
        /// </summary>
        public ValueConverterCollection ConverterCollection
        {
            get
            {
                return (ValueConverterCollection)this.GetValue(ConverterCollectionProperty);
            }
            set
            {
                this.SetValue(ConverterCollectionProperty, value);
            }
        }

        /// <summary>
        /// Runs a value through a collection of IValueConverters sequentially to get a modified result.
        /// </summary>
        /// <param name="value">
        /// The value to manipulate.
        /// </param>
        /// <returns>
        /// Returns the converted value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (this.ConverterCollection != null)
            {
                value =
                    this.ConverterCollection.Converters.Where(converter => converter.Converter != null)
                        .Aggregate(value, (current, converter) => converter.Convert(current, targetType));
            }

            return value;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}