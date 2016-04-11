// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the IntExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Extensions
{
    /// <summary>
    /// A collection of <see cref="int"/> extensions.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Converts a radius <see cref="int"/> value to a zoom level.
        /// </summary>
        /// <param name="radius">
        /// The radius to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the zoom level.
        /// </returns>
        public static double ToZoomLevel(this int radius)
        {
            double zoomLevel;

            if (radius >= 30)
            {
                zoomLevel = 8;
            }
            else if (radius >= 20)
            {
                zoomLevel = 9;
            }
            else if (radius >= 10)
            {
                zoomLevel = 10;
            }
            else if (radius >= 5)
            {
                zoomLevel = 11;
            }
            else if (radius >= 3)
            {
                zoomLevel = 12;
            }
            else
            {
                zoomLevel = 13;
            }

            return zoomLevel;
        }

        /// <summary>
        /// Converts a miles <see cref="int"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this int miles)
        {
            return miles / 0.00062137;
        }
    }
}