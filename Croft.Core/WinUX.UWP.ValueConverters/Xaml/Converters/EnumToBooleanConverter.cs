// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumToBooleanConverter.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converter that checks if a given Enum value matches the parameter value.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Convert enum to visibility if enum value is equal to enum parameter return true otherwise false
        /// </summary>
        /// <param name="value">property value</param>
        /// <param name="targetType">target control type</param>
        /// <param name="parameter">parameter enum</param>
        /// <param name="language">default language</param>
        /// <returns>Is control visible</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
            {
                return false;
            }

            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return false;
            }

            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Convert enum to visibility
        /// </summary>
        /// <param name="value">property value</param>
        /// <param name="targetType">target control type</param>
        /// <param name="parameter">parameter value</param>
        /// <param name="language">default language</param>
        /// <returns>Enum value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}