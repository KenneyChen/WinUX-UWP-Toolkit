// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkElementExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   A collection of <see cref="FrameworkElement" /> extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Extensions
{
    using Windows.UI.Xaml;

    /// <summary>
    /// A collection of <see cref="FrameworkElement"/> extensions.
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Get ancestor of framework element
        /// </summary>
        /// <typeparam name="T">Framwork element</typeparam>
        /// <param name="element">Framework element to find ancestor of</param>
        /// <returns>Ancestor framework element</returns>
        public static T GetAncestor<T>(this FrameworkElement element) where T : FrameworkElement
        {
            var ancestor = element as T;
            if (ancestor != null)
            {
                return ancestor;
            }

            var nextElement = element.Parent as FrameworkElement;
            return nextElement == null ? default(T) : GetAncestor<T>(nextElement);
        }
    }
}