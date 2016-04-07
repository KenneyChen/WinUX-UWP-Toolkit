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
        /// Get the ancestor of a given <see cref="FrameworkElement"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of ancestor to find.
        /// </typeparam>
        /// <param name="element">
        /// The <see cref="FrameworkElement"/> to find the ancestor from.
        /// </param>
        /// <returns>
        /// Returns the ancestor <see cref="FrameworkElement"/> if it exists, else null.
        /// </returns>
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