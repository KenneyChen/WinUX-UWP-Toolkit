﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageFileProperty.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Storage
{
    using Windows.Storage;

    /// <summary>
    /// A model for <see cref="StorageFile"/> properties.
    /// </summary>
    public class StorageFileProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFileProperty"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public StorageFileProperty(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value { get; }
    }
}