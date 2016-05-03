// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatConverter.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the DoubleToIntegerValueConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    public class IntegerToDoubleValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts a int value to double value.
        /// </summary>
        /// <returns>
        /// Returns double value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToDouble(value);
        }


        /// <summary>
        /// Converts a double value to int value.
        /// </summary>
        /// <returns>
        /// Returns int value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToInt32(value);
        }
    }
}
