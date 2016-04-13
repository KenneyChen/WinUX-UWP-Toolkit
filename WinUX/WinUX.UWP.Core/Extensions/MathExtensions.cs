// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   A collection of extensions which provide mathematic functions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Extensions
{
    /// <summary>
    /// A collection of extensions which provide mathematic functions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Basic calculation of linear interpolation between two values.
        /// </summary>
        /// <param name="start">
        /// The start point.
        /// </param>
        /// <param name="end">
        /// The end point.
        /// </param>
        /// <param name="amount">
        /// The amount to adjust by.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float Lerp(this float start, float end, float amount)
        {
            var difference = end - start;
            var adjusted = difference * amount;
            return start + adjusted;
        }
    }
}