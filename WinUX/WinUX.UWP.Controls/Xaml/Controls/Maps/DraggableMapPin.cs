// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableMapPin.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines a draggable pin control for the UWP MapControl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Controls.Maps
{
    using System;

    using Windows.Devices.Geolocation;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Maps;
    using Windows.UI.Xaml.Input;

    /// <summary>
    /// Defines a draggable pin control for the UWP MapControl.
    /// </summary>
    public class DraggableMapPin : Control
    {
        private readonly MapControl map;

        private bool isDragging;

        private Geopoint center;

        public DraggableMapPin(MapControl map)
        {
            this.DefaultStyleKey = typeof(DraggableMapPin);

            this.map = map;
        }

        public bool IsDraggable { get; set; }

        public Action<Geopoint> Dragging;

        public Action<Geopoint> DragStarted;

        public Action<Geopoint> DragCompleted;

        private MapPanInteractionMode previousPanMode;

        private MapInteractionMode previousRotateMode;

        private MapInteractionMode previousTiltMode;

        private MapInteractionMode previousZoomMode;

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (this.IsDraggable)
            {
                if (this.map != null)
                {
                    // Store the map's center position
                    this.center = this.map.Center;

                    // Store the map's previous interaction modes
                    this.previousPanMode = this.map.PanInteractionMode;
                    this.previousRotateMode = this.map.RotateInteractionMode;
                    this.previousTiltMode = this.map.TiltInteractionMode;
                    this.previousZoomMode = this.map.ZoomInteractionMode;

                    // Turn off any map interaction modes while we move the pin
                    this.map.PanInteractionMode = MapPanInteractionMode.Disabled;
                    this.map.RotateInteractionMode = MapInteractionMode.Disabled;
                    this.map.TiltInteractionMode = MapInteractionMode.Disabled;
                    this.map.ZoomInteractionMode = MapInteractionMode.Disabled;

                    // Attach events to the map to track manipulation events
                    this.map.ActualCameraChanged += this.OnMapCameraChanged;
                    this.map.ActualCameraChanging += this.OnMapCameraChanging;
                    this.map.PointerReleased += this.OnMapPointerReleased;
                    this.map.PointerMoved += this.OnMapPointerMoved;
                    this.map.PointerExited += this.OnMapPointerReleased;
                }

                var pointerPosition = e.GetCurrentPoint(this.map);

                Geopoint location = null;

                if (this.map != null)
                {
                    this.map.GetLocationFromOffset(pointerPosition.Position, out location);
                    MapControl.SetLocation(this, location);
                }

                this.DragStarted?.Invoke(location);
                this.isDragging = true;
            }
        }

        public void RemoveEventHandlers()
        {
            if (this.map != null)
            {
                this.map.ActualCameraChanged -= this.OnMapCameraChanged;
                this.map.ActualCameraChanging -= this.OnMapCameraChanging;
                this.map.PointerReleased -= this.OnMapPointerReleased;
                this.map.PointerMoved -= this.OnMapPointerMoved;
                this.map.PointerExited -= this.OnMapPointerReleased;
            }
        }

        private void OnMapCameraChanging(MapControl sender, MapActualCameraChangingEventArgs args)
        {
            if (this.isDragging)
            {
                // Reset the map center to the stored center value.
                // This prevents the map from panning when we drag across it.
                this.map.Center = this.center;
            }
        }

        private void OnMapPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            // Check if the user is currently dragging the pin
            if (this.isDragging)
            {
                // If so, move the pin to where the pointer is.
                var pointerPosition = e.GetCurrentPoint(this.map);

                Geopoint location = null;
                if (this.map != null)
                {
                    // Convert the point pixel to a coordinate and set the location of the pin
                    this.map.GetLocationFromOffset(pointerPosition.Position, out location);
                    MapControl.SetLocation(this, location);
                }

                this.Dragging?.Invoke(location);
            }
        }

        private void OnMapPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // Pin released, remove dragging events
            this.RemoveEventHandlers();

            var pointerPosition = e.GetCurrentPoint(this.map);

            Geopoint location = null;

            if (this.map != null)
            {
                // Convert the point pixel to a coordinate and set the location of the pin
                this.map.GetLocationFromOffset(pointerPosition.Position, out location);
                MapControl.SetLocation(this, location);

                // Reset the interaction modes back to their previous settings
                this.map.PanInteractionMode = this.previousPanMode;
                this.map.RotateInteractionMode = this.previousRotateMode;
                this.map.TiltInteractionMode = this.previousTiltMode;
                this.map.ZoomInteractionMode = this.previousZoomMode;
            }

            this.DragCompleted?.Invoke(location);

            this.isDragging = false;
        }

        private void OnMapCameraChanged(MapControl mapControl, MapActualCameraChangedEventArgs args)
        {
            if (this.isDragging)
            {
                // Reset the map center to the stored center value.
                // This prevents the map from panning when we drag across it.
                this.map.Center = this.center;
            }
        }
    }
}