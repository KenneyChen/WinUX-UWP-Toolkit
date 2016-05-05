// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleToTimeSpanConverter.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines a value converter for converting a double to a TimeSpan.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for converting a double to a TimeSpan.
    /// </summary>
    public class DoubleToTimeSpanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a double value into a TimeSpan value.
        /// </summary>
        /// <param name="value">
        /// A <see cref="double"/> value.
        /// </param>
        /// <returns>
        /// Returns the TimeSpan representation of the double value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided value is null.
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = TimeSpan.Zero;

            double val;
            var parsed = double.TryParse(value.ToString(), out val);

            if (parsed)
            {
                result = TimeSpan.FromSeconds(val);
            }

            return result;
        }

        /// <summary>
        /// Converts a TimeSpan value back to a double value.
        /// </summary>
        /// <param name="value">
        /// A <see cref="TimeSpan"/> value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided value is null.
        /// </exception>
        /// <returns>
        /// Returns the double representation of the TimeSpan value in seconds.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            double result = 0;

            var timeSpan = value as TimeSpan? ?? TimeSpan.Zero;
            if (timeSpan == null)
            {
                result = timeSpan.TotalSeconds;
            }

            return result;
        }
    }
}
