// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapControlRadiusBehavior.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the MapControlRadiusBehavior type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Behaviors.MapControl
{
    using System.Linq;

    using Windows.Devices.Geolocation;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Maps;

    using Microsoft.Xaml.Interactivity;

    using WinUX.Extensions;

    /// <summary>
    /// A behavior to draw a radius on the <see cref="MapControl"/>.
    /// </summary>
    public class MapControlRadiusBehavior : Behavior
    {
        private MapControl MapControl => this.AssociatedObject as MapControl;

        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
            nameof(Center),
            typeof(Geopoint),
            typeof(MapControlRadiusBehavior),
            new PropertyMetadata(null, OnCenterChanged));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius",
            typeof(double),
            typeof(MapControlRadiusBehavior),
            new PropertyMetadata(0.0, OnRadiusChanged));

        public Color RadiusFillColor { get; set; }

        public Color RadiusBorderColor { get; set; }

        private static void OnRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as MapControlRadiusBehavior;
            behavior?.UpdateRadius();
        }

        public double Radius
        {
            get
            {
                return (double)this.GetValue(RadiusProperty);
            }
            set
            {
                this.SetValue(RadiusProperty, value);
            }
        }

        private MapPolygon MapRadius { get; set; }

        private static void OnCenterChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var behavior = d as MapControlRadiusBehavior;
            behavior?.UpdateRadius();
        }

        private void UpdateRadius()
        {
            if (this.MapControl != null && this.Center != null)
            {
                var radiusCirclePoints = this.Center.GetCirclePoints(this.Radius);

                if (this.MapRadius != null)
                {
                    var element = this.MapControl.MapElements.FirstOrDefault(x => x.Equals(this.MapRadius));
                    if (element != null)
                    {
                        this.MapControl.MapElements.Remove(element);
                    }
                }

                this.MapRadius = new MapPolygon
                                     {
                                         Path = new Geopath(radiusCirclePoints),
                                         ZIndex = 0,
                                         FillColor = this.RadiusFillColor,
                                         StrokeColor = this.RadiusBorderColor
                                     };

                this.MapControl.MapElements.Add(this.MapRadius);
            }
        }

        public Geopoint Center
        {
            get
            {
                return (Geopoint)this.GetValue(CenterProperty);
            }
            set
            {
                this.SetValue(CenterProperty, value);
            }
        }
    }
}