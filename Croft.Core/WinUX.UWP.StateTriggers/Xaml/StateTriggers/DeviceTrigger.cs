﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceTrigger.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.StateTriggers
{
    using System;

    using Windows.ApplicationModel.Wallet;
    using Windows.Foundation.Metadata;
    using Windows.Graphics.Display;
    using Windows.System.Profile;
    using Windows.UI.Xaml;

    using WinUX.Enums;

    /// <summary>
    /// A StateTrigger handling the device type.
    /// </summary>
    public class DeviceTrigger : StateTriggerBase
    {
        private static readonly string CurrentDevice;

        private static readonly DependencyProperty DeviceTypeProperty = DependencyProperty.Register(
            nameof(DeviceType),
            typeof(DeviceType),
            typeof(DeviceTrigger),
            new PropertyMetadata(DeviceType.Unknown, OnDeviceTypeChanged));

        public static readonly DependencyProperty SupportsContinuumProperty =
            DependencyProperty.Register(
                nameof(SupportsContinuum),
                typeof(bool),
                typeof(DeviceTrigger),
                new PropertyMetadata(false));

        private bool _isActive;

        /// <summary>
        /// Initializes static members of the <see cref="DeviceTrigger"/> class.
        /// </summary>
        static DeviceTrigger()
        {
            CurrentDevice = AnalyticsInfo.VersionInfo.DeviceFamily;
        }

        public bool SupportsContinuum
        {
            get
            {
                return (bool)this.GetValue(SupportsContinuumProperty);
            }
            set
            {
                this.SetValue(SupportsContinuumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        public DeviceType DeviceType
        {
            get
            {
                return (DeviceType)this.GetValue(DeviceTypeProperty);
            }
            set
            {
                this.SetValue(DeviceTypeProperty, value);
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

        private static void OnDeviceTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var trigger = (DeviceTrigger)obj;
            var newVal = (DeviceType)args.NewValue;

            switch (CurrentDevice)
            {
                case "Windows.Desktop":
                    trigger.IsActive = newVal == DeviceType.Desktop;
                    break;
                case "Windows.Mobile":
                    trigger.IsActive = IsInContinuum() && trigger.SupportsContinuum
                                           ? newVal == DeviceType.ContinuumPhone
                                           : newVal == DeviceType.Mobile;
                    break;
                case "Windows.Team":
                    trigger.IsActive = newVal == DeviceType.SurfaceHub;
                    break;
                case "Windows.IoT":
                    trigger.IsActive = newVal == DeviceType.IoT;
                    break;
                case "Windows.Xbox":
                    trigger.IsActive = newVal == DeviceType.Xbox;
                    break;
                case "Windows.HoloLens":
                    trigger.IsActive = newVal == DeviceType.Hololens;
                    break;
                default:
                    trigger.IsActive = newVal == DeviceType.Unknown;
                    break;
            }
        }

        private static double ScreenDiagonal
        {
            get
            {
                var di = DisplayInformation.GetForCurrentView();

                if (ApiInformation.IsPropertyPresent(
                    typeof(DisplayInformation).ToString(),
                    nameof(di.DiagonalSizeInInches)))
                {
                    if (di.DiagonalSizeInInches != null)
                    {
                        return di.DiagonalSizeInInches.Value;
                    }
                }

                return 7;
            }
        }

        private static bool IsInContinuum()
        {
            if (CurrentDevice != "Windows.Mobile")
            {
                return false;
            }

            return ScreenDiagonal > 7;
        }

        /// <summary>
        ///  Called when the IsActive property changes.
        /// </summary>
        public event EventHandler IsActiveChanged;
    }
}