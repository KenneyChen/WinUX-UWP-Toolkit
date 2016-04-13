// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeopointExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   A collection of <see cref="Geopoint" /> extensions to support <see cref="MapControl" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Extensions
{
    using System;
    using System.Collections.Generic;

    using Windows.Devices.Geolocation;

    /// <summary>
    /// A collection of <see cref="Geopoint"/> extensions to support <see cref="Windows.UI.Xaml.Controls.Maps.MapControl"/>.
    /// </summary>
    public static class GeopointExtensions
    {
        /// <summary>
        /// Gets a point at a given distance and bearing from another.
        /// </summary>
        /// <param name="point">
        /// The point.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        /// <param name="bearing">
        /// The bearing.
        /// </param>
        /// <returns>
        /// Returns a <see cref="BasicGeoposition"/> value.
        /// </returns>
        public static BasicGeoposition GetPointAtDistanceAndBearing(
            this Geopoint point,
            double distance,
            double bearing)
        {
            const double DegreesToRadian = Math.PI / 180.0;
            const double RadianToDegrees = 180.0 / Math.PI;
            const double EarthRadius = 6378137.0;

            var latA = point.Position.Latitude * DegreesToRadian;
            var lonA = point.Position.Longitude * DegreesToRadian;
            var angularDistance = distance / EarthRadius;
            var trueCourse = bearing * DegreesToRadian;

            var lat =
                Math.Asin(
                    Math.Sin(latA) * Math.Cos(angularDistance)
                    + Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            var dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            var lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            var result = new BasicGeoposition { Latitude = lat * RadianToDegrees, Longitude = lon * RadianToDegrees };

            return result;
        }

        /// <summary>
        /// Gets a collection of points representing a circle around a given point.
        /// </summary>
        /// <param name="center">
        /// The center point.
        /// </param>
        /// <param name="radius">
        /// The radius.
        /// </param>
        /// <param name="numberOfPoints">
        /// The number of points to generate.
        /// </param>
        /// <returns>
        /// Returns a collection of <see cref="BasicGeoposition"/> values representing a circle.
        /// </returns>
        public static IEnumerable<BasicGeoposition> GetCirclePoints(
            this Geopoint center,
            double radius,
            int numberOfPoints = 180)
        {
            var angle = 360.0 / numberOfPoints;
            var locations = new List<BasicGeoposition>();

            for (var i = 0; i <= numberOfPoints; i++)
            {
                locations.Add(center.GetPointAtDistanceAndBearing(radius, angle * i));
            }

            return locations;
        }
    }
}