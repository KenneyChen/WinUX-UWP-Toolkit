// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiInformationTypePresentTrigger.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.StateTriggers
{
    using System;

    using Windows.Foundation.Metadata;
    using Windows.UI.Xaml;

    /// <summary>
    /// A StateTrigger to handle <see cref="ApiInformation"/> types existing.
    /// </summary>
    public class ApiInformationTypePresentTrigger : StateTriggerBase
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type",
            typeof(string),
            typeof(ApiInformationTypePresentTrigger),
            new PropertyMetadata(string.Empty, OnTypeChanged));

        private bool _isActive;

        /// <summary>
        /// Gets or sets the type to trigger on.
        /// </summary>
        public string Type
        {
            get
            {
                return (string)this.GetValue(TypeProperty);
            }
            set
            {
                this.SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the trigger is currently active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            private set
            {
                if (this._isActive == value)
                {
                    return;
                }

                this._isActive = value;
                this.SetActive(value); // Sets the trigger as active causing the UI to update

                this.IsActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private static void OnTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var trigger = (ApiInformationTypePresentTrigger)obj;
            var newVal = (string)args.NewValue;

            trigger.IsActive = !string.IsNullOrWhiteSpace(newVal) && ApiInformation.IsTypePresent(newVal);
        }

        /// <summary>
        ///     Called when the IsActive property changes.
        /// </summary>
        public event EventHandler IsActiveChanged;
    }
}