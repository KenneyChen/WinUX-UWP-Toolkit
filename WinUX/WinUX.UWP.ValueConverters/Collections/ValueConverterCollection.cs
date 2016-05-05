// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueConverterCollection.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines a collection of ValueConverter objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Collections
{
    using System.Collections.Generic;

    using WinUX.Xaml.Data;

    /// <summary>
    /// Defines a collection of ValueConverter objects.
    /// </summary>
    public class ValueConverterCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterCollection"/> class.
        /// </summary>
        public ValueConverterCollection()
        {
            this.Converters = new List<ValueConverter>();
        }

        /// <summary>
        /// Gets or sets the collection of value converters.
        /// </summary>
        public List<ValueConverter> Converters { get; set; }
    }
}