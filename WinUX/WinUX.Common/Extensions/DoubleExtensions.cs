// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Extensions
{
    using System;

    /// <summary>
    /// A collection of <see cref="double"/> extensions.
    /// </summary>
    public static class DoubleExtensions
    {
        #region File sizes

        private const double KiloByte = 1024;

        private const double MegaByte = KiloByte * 1024;

        private const double GigaByte = MegaByte * 1024;

        /// <summary>
        /// Converts a double byte value to a file size represented as a string.
        /// </summary>
        /// <param name="fileSize">
        /// File size represented in bytes.
        /// </param>
        /// <returns>
        /// Returns a string representation of the bytes as a file size.
        /// </returns>
        public static string ToFileSize(this double fileSize)
        {
            string sizeType;
            if (fileSize > GigaByte)
            {
                fileSize /= GigaByte;
                sizeType = "GB";
            }
            else if (fileSize > MegaByte)
            {
                fileSize /= MegaByte;
                sizeType = "MB";
            }
            else if (fileSize > KiloByte)
            {
                fileSize /= KiloByte;
                sizeType = "KB";
            }
            else
            {
                sizeType = "B";
            }

            return $"{Math.Round(fileSize, 2)}{sizeType}";
        }

        #endregion

        #region Geolocation

        /// <summary>
        /// Converts a miles <see cref="double"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this double miles)
        {
            return miles / 0.00062137;
        }

        /// <summary>
        /// Converts a meters <see cref="double"/> value to miles.
        /// </summary>
        /// <param name="meters">
        /// The meters to convert.
        /// </param>
        /// <returns>
        /// Returns an <see cref="int"/> value representing the miles.
        /// </returns>
        public static int ToMiles(this double meters)
        {
            return (int)(meters * 0.00062137);
        }

        /// <summary>
        /// Converts a degrees <see cref="double"/> value to radians.
        /// </summary>
        /// <param name="degrees">
        /// The degrees to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the radians.
        /// </returns>
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        /// <summary>
        /// Calculates the distance between a latitude/longitude of one location with another.
        /// </summary>
        /// <param name="latitudeA">
        /// The latitude for location A.
        /// </param>
        /// <param name="longitudeA">
        /// The longitude for location A.
        /// </param>
        /// <param name="latitudeB">
        /// The latitude for location B.
        /// </param>
        /// <param name="longitudeB">
        /// The longitude for location B.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the distance between.
        /// </returns>
        public static double CalculateDistanceBetween(
            double latitudeA,
            double longitudeA,
            double latitudeB,
            double longitudeB)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance;

            double latRadiansA = latitudeA.ToRadians();
            double lonRadiansA = longitudeA.ToRadians();
            double latRadiansB = latitudeB.ToRadians();
            double lonRadiansB = longitudeB.ToRadians();

            double longitudeDiff = Math.Abs(lonRadiansA - lonRadiansB);

            if (longitudeDiff > Math.PI)
            {
                longitudeDiff = (2.0 * Math.PI) - longitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    (Math.Sin(latRadiansB) * Math.Sin(latRadiansA))
                    + ((Math.Cos(latRadiansB) * Math.Cos(latRadiansA)) * Math.Cos(longitudeDiff)));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        /// <summary>
        /// Incidates whether a given latitude/longitude is within a radius of another.
        /// </summary>
        /// <param name="latitude">
        /// The longitude to check.
        /// </param>
        /// <param name="longitude">
        /// The longitude to check.
        /// </param>
        /// <param name="centreLatitude">
        /// The centre latitude.
        /// </param>
        /// <param name="centreLongitude">
        /// The centre longitude.
        /// </param>
        /// <param name="radiusMeters">
        /// The radius meters.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> value indicating whether the lat/lon is within the radius.
        /// </returns>
        public static bool IsPointWithinRadius(
            double latitude,
            double longitude,
            double centreLatitude,
            double centreLongitude,
            double radiusMeters)
        {
            var distanceMetres = CalculateDistanceBetween(latitude, longitude, centreLatitude, centreLongitude) * 1000;
            return distanceMetres < radiusMeters;
        }

        #endregion
    }
}