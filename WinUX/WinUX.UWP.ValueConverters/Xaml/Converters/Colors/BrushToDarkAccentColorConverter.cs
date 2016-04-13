﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrushToDarkAccentColorConverter.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the BrushToDarkAccentColorConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Converters.Colors
{
    using System;

    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    using WinUX.Enums;
    using WinUX.Extensions;

    /// <summary>
    /// Converter for converting a <see cref="SolidColorBrush"/> to an internal dark <see cref="AccentColor"/>.
    /// </summary>
    public class BrushToDarkAccentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = (SolidColorBrush)value;
            var accentColor = brush.Color.ToAccentColor();

            return accentColor.ToDarkAccentColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
