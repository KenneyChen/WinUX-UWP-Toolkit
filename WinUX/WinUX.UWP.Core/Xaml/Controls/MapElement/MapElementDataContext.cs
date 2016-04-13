// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapElementDataContext.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the MapElementDataContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Controls.MapElement
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Maps;

    /// <summary>
    /// An extension to <see cref="MapElement"/> to provide a DataContext such as a view model.
    /// </summary>
    public static class MapElementDataContext
    {
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.RegisterAttached(
                "DataContext",
                typeof(object),
                typeof(MapElementDataContext),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the DataContext for the given <see cref="MapElement"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="MapElement"/>.
        /// </param>
        /// <returns>
        /// Returns the data as an object.
        /// </returns>
        public static object GetDataContext(DependencyObject obj)
        {
            return obj.GetValue(DataContextProperty);
        }

        /// <summary>
        /// Sets the DataContext of the given <see cref="MapElement"/>.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetDataContext(DependencyObject obj, object value)
        {
            obj.SetValue(DataContextProperty, value);
        }

        /// <summary>
        /// Adds a DataContext to a <see cref="MapElement"/>.
        /// </summary>
        /// <param name="element">
        /// The element to add the data to.
        /// </param>
        /// <param name="data">
        /// The data to add.
        /// </param>
        public static void AddDataContext(this MapElement element, object data)
        {
            SetDataContext(element, data);
        }

        /// <summary>
        /// Retrieves the DataContext from a <see cref="MapElement"/>.
        /// </summary>
        /// <param name="element">
        /// The element to read from.
        /// </param>
        /// <typeparam name="T">
        /// The data's type.
        /// </typeparam>
        /// <returns>
        /// Returns the data as it's original type.
        /// </returns>
        public static T RetrieveDataContext<T>(this MapElement element) where T : class
        {
            return GetDataContext(element) as T;
        }
    }
}
