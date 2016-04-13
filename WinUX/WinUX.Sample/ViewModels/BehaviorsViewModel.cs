// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorsViewModel.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the BehaviorsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample.ViewModels
{
    using System;

    using Windows.Devices.Geolocation;

    using GalaSoft.MvvmLight;

    using WinUX.Extensions;
    using WinUX.Location;

    /// <summary>
    /// The <see cref="MainPage"/> view model.
    /// </summary>
    public class BehaviorsViewModel : ViewModelBase
    {
        private Geopoint currentPosition;

        private double searchRadius;

        private readonly GeolocationHelper locationHelper;

        private Geopoint mapCenter;

        private double mapZoomLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorsViewModel"/> class.
        /// </summary>
        public BehaviorsViewModel()
        {
            this.locationHelper = new GeolocationHelper { OnPositionChanged = this.OnPositionChanged };
            this.locationHelper.Initialize();

            this.SearchRadius = 10.ToMeters();
            this.MapZoomLevel = this.SearchRadius.ToMiles().ToZoomLevel();

            this.OnPositionChanged(this.locationHelper.CurrentPosition);
        }

        /// <summary>
        /// Gets or sets the zoom level.
        /// </summary>
        public double MapZoomLevel
        {
            get
            {
                return this.mapZoomLevel;
            }
            set
            {
                this.Set(ref this.mapZoomLevel, value);
            }
        }

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        public Geopoint CurrentPosition
        {
            get
            {
                return this.currentPosition;
            }
            set
            {
                this.Set(() => this.CurrentPosition, ref this.currentPosition, value);
            }
        }

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        public Geopoint MapCenter
        {
            get
            {
                return this.mapCenter;
            }
            set
            {
                this.Set(ref this.mapCenter, value);
            }
        }

        /// <summary>
        /// Gets or sets the search radius.
        /// </summary>
        public double SearchRadius
        {
            get
            {
                return this.searchRadius;
            }
            set
            {
                this.Set(ref this.searchRadius, value);
            }
        }

        private void OnPositionChanged(Geoposition geoposition)
        {
            if (geoposition?.Coordinate != null)
            {
                this.CurrentPosition = geoposition.Coordinate.Point;
                this.MapCenter = this.CurrentPosition;
            }
        }
    }
}