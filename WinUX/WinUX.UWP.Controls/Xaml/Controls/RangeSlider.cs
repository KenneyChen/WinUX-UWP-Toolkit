namespace WinUX.Xaml.Controls
{
    using System;
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;

    public class RangeSlider : Control
    {
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            nameof(MaxValue),
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0, OnMaxValueChanged));

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            nameof(MinValue),
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0, OnMinValueChanged));

        public static readonly DependencyProperty MaxSelectedValueProperty =
            DependencyProperty.Register(
                nameof(MaxSelectedValue),
                typeof(double),
                typeof(RangeSlider),
                new PropertyMetadata(0, OnMaxSelectedValueChanged));

        public static readonly DependencyProperty MinSelectedValueProperty =
            DependencyProperty.Register(
                nameof(MinSelectedValue),
                typeof(double),
                typeof(RangeSlider),
                new PropertyMetadata(0, OnMinSelectedValueChanged));

        private double minStartPoint;

        private double maxStartPoint;

        private bool isTemplateApplied;

        private bool hasLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeSlider"/> class.
        /// </summary>
        public RangeSlider()
        {
            this.DefaultStyleKey = typeof(RangeSlider);

            this.Loaded += this.OnRangeSliderLoaded;
            this.Unloaded += this.OnRangeSliderUnloaded;
        }

        /// <summary>
        /// Event fired when the max selected value has changed.
        /// </summary>
        public event Action<object, double> MaxSelectedValueChanged;

        /// <summary>
        /// Command fired when the max selected value has changed.
        /// </summary>
        public ICommand MaxSelectedValueChangedCommand;

        /// <summary>
        /// Event fired when the min selected value has changed.
        /// </summary> 
        public event Action<object, double> MinSelectedValueChanged;

        /// <summary>
        /// Command fired when the min selected value has changed.
        /// </summary>
        public ICommand MinSelectedValueChangedCommand;

        /// <summary>
        /// Gets or sets the max value available by the slider.
        /// </summary>
        public double MaxValue
        {
            get
            {
                return (double)this.GetValue(MaxValueProperty);
            }
            set
            {
                this.SetValue(MaxValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the min value available by the slider.
        /// </summary>
        public double MinValue
        {
            get
            {
                return (double)this.GetValue(MinValueProperty);
            }
            set
            {
                this.SetValue(MinValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected value that is currently the maximum.
        /// </summary>
        public double MaxSelectedValue
        {
            get
            {
                return (double)this.GetValue(MaxSelectedValueProperty);
            }
            set
            {
                this.SetValue(MaxSelectedValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected value that is currently the minimum.
        /// </summary>
        public double MinSelectedValue
        {
            get
            {
                return (double)this.GetValue(MinSelectedValueProperty);
            }
            set
            {
                this.SetValue(MinSelectedValueProperty, value);
            }
        }

        private Grid DragArea { get; set; }

        private Path MinSlider { get; set; }

        private RangeSliderFlyout MinSliderFlyout { get; set; }

        private Path MaxSlider { get; set; }

        private RangeSliderFlyout MaxSliderFlyout { get; set; }

        /// <summary>
        /// Called on applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (this.isTemplateApplied)
            {
                return;
            }

            this.DragArea = this.GetTemplateChild("DragArea") as Grid;
            this.MinSlider = this.GetTemplateChild("MinSlider") as Path;
            this.MaxSlider = this.GetTemplateChild("MaxSlider") as Path;
            this.MinSliderFlyout = this.GetTemplateChild("MinSliderFlyout") as RangeSliderFlyout;
            this.MaxSliderFlyout = this.GetTemplateChild("MaxSliderFlyout") as RangeSliderFlyout;

            this.isTemplateApplied = true;

            this.InitializeControl();

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Called on max selected value changed.
        /// </summary>
        /// <exception cref="Exception">A delegate callback could throw an exception.</exception>
        protected virtual void OnMaxSelectedValueChanged()
        {
            this.MaxSelectedValueChanged?.Invoke(this, this.MaxSelectedValue);
            this.MaxSelectedValueChangedCommand?.Execute(this.MaxSelectedValue);
        }

        /// <summary>
        /// Called on min selected value changed.
        /// </summary>
        /// <exception cref="Exception">A delegate callback could throw an exception.</exception>
        protected virtual void OnMinSelectedValueChanged()
        {
            this.MinSelectedValueChanged?.Invoke(this, this.MinSelectedValue);
            this.MinSelectedValueChangedCommand?.Execute(this.MinSelectedValue);
        }

        private static void OnMinSelectedValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as RangeSlider;
            if (control == null) throw new InvalidOperationException("Control must be of type RangeSlider.");

            control.OnMinSelectedValueChanged();
        }

        private static void OnMaxSelectedValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as RangeSlider;
            if (control == null) throw new InvalidOperationException("Control must be of type RangeSlider.");

            control.OnMaxSelectedValueChanged();
        }

        private static void OnMaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as RangeSlider;
            if (control == null) throw new InvalidOperationException("Control must be of type RangeSlider");

            control.MaxSelectedValue = control.MaxValue;

            if (control.MaxSlider == null) return;
            if (control.MaxSliderFlyout == null) return;

            var sliderTransform = control.MaxSlider.RenderTransform as TranslateTransform;
            if (sliderTransform == null) return;

            var flyoutTransform = control.MaxSliderFlyout.RenderTransform as TranslateTransform;
            if (flyoutTransform == null) return;

            flyoutTransform.X = control.DragArea.ActualWidth - GetControlBounds(control.MaxSliderFlyout);
            sliderTransform.X = control.DragArea.ActualWidth - GetControlBounds(control.MaxSlider);
        }

        private static void OnMinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RangeSlider;
            if (control == null) throw new InvalidOperationException("Control must be of type RangeSlider");

            control.MinSelectedValue = control.MinValue;

            if (control.MinSlider == null) return;
            if (control.MinSliderFlyout == null) return;

            var sliderTransform = control.MinSlider.RenderTransform as TranslateTransform;
            if (sliderTransform == null) return;

            var flyoutTransform = control.MinSliderFlyout.RenderTransform as TranslateTransform;
            if (flyoutTransform == null) return;

            flyoutTransform.X = control.DragArea.ActualWidth - GetControlBounds(control.MinSliderFlyout);
            sliderTransform.X = control.DragArea.ActualWidth - GetControlBounds(control.MinSlider);
        }

        private void OnRangeSliderLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateRangeSlider();

            this.SizeChanged += this.OnSizeChanged;
        }

        private void OnRangeSliderUnloaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged -= this.OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            this.UpdateRangeSlider(true);
        }

        private void UpdateRangeSlider(bool resetValues = false)
        {
            if (this.isTemplateApplied)
            {
                var maxSliderTransform = this.MaxSlider.RenderTransform as TranslateTransform;
                if (maxSliderTransform == null)
                {
                    return;
                }

                var maxSliderFlyoutTransform = this.MaxSliderFlyout.RenderTransform as TranslateTransform;
                if (maxSliderFlyoutTransform == null)
                {
                    return;
                }

                var minSliderTransform = this.MinSlider.RenderTransform as TranslateTransform;
                if (minSliderTransform == null)
                {
                    return;
                }

                var minSliderFlyoutTransform = this.MinSliderFlyout.RenderTransform as TranslateTransform;
                if (minSliderFlyoutTransform == null)
                {
                    return;
                }

                maxSliderTransform.X = this.DragArea.ActualWidth - GetControlBounds(this.MaxSlider);
                maxSliderFlyoutTransform.X = this.DragArea.ActualWidth - GetControlBounds(this.MaxSliderFlyout);

                minSliderFlyoutTransform.X =
                    -((GetControlBounds(this.MinSliderFlyout) - GetControlBounds(this.MinSlider)) / 2);

                this.MinSliderFlyout.Visibility = Visibility.Collapsed;
                this.MaxSliderFlyout.Visibility = Visibility.Collapsed;

                if (resetValues)
                {
                    this.MinSelectedValue = this.MinValue;
                    this.MaxSelectedValue = this.MaxValue;
                }

                this.hasLoaded = true;
            }
        }

        private void InitializeControl()
        {
            this.MinSlider.RenderTransform = (this.MinSlider.RenderTransform as TranslateTransform)
                                             ?? new TranslateTransform();
            this.MaxSlider.RenderTransform = (this.MaxSlider.RenderTransform as TranslateTransform)
                                             ?? new TranslateTransform();

            this.MinSliderFlyout.RenderTransform = (this.MinSliderFlyout.RenderTransform as TranslateTransform)
                                                   ?? new TranslateTransform();
            this.MaxSliderFlyout.RenderTransform = (this.MaxSliderFlyout.RenderTransform as TranslateTransform)
                                                   ?? new TranslateTransform();

            this.MinSlider.ManipulationMode = ManipulationModes.TranslateX;
            this.MinSlider.ManipulationDelta += this.OnMinSliderManipulationDelta;
            this.MinSlider.ManipulationStarted += this.OnMinSliderManipulationStarted;
            this.MinSlider.ManipulationCompleted += this.OnMinSliderManipulationCompleted;

            this.MaxSlider.ManipulationMode = ManipulationModes.TranslateX;
            this.MaxSlider.ManipulationDelta += this.OnMaxSliderManipulationDelta;
            this.MaxSlider.ManipulationStarted += this.OnMaxSliderManipulationStarted;
            this.MaxSlider.ManipulationCompleted += this.OnMaxSliderManipulationCompleted;

            if (!this.hasLoaded)
            {
                this.OnRangeSliderLoaded(null, null);
            }
        }

        private void OnMaxSliderManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.MaxSliderFlyout.Visibility = Visibility.Collapsed;
        }

        private void OnMinSliderManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.MinSliderFlyout.Visibility = Visibility.Collapsed;
        }

        private void OnMinSliderManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (this.MinSlider == null) return;

            var translateTransform = this.MinSlider.RenderTransform as TranslateTransform;
            if (translateTransform == null) return;

            this.minStartPoint = translateTransform.X;
            this.MinSliderFlyout.Visibility = Visibility.Visible;
        }

        private void OnMaxSliderManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (this.MaxSlider == null) return;

            var translateTransform = this.MaxSlider.RenderTransform as TranslateTransform;
            if (translateTransform == null) return;

            this.maxStartPoint = translateTransform.X;
            this.MaxSliderFlyout.Visibility = Visibility.Visible;
        }

        private void OnMinSliderManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (this.MinSlider == null) return;

            var translateTransform = this.MinSlider.RenderTransform as TranslateTransform;
            if (translateTransform == null) return;

            var maxTranslateTransform = this.MaxSlider.RenderTransform as TranslateTransform;
            if (maxTranslateTransform == null) return;

            var minLabelTransform = this.MinSliderFlyout.RenderTransform as TranslateTransform;
            if (minLabelTransform == null) return;

            var cumulative = this.minStartPoint + e.Cumulative.Translation.X;
            if (cumulative < 0.0)
            {
                translateTransform.X = 0.0;
                minLabelTransform.X = -((GetControlBounds(this.MinSliderFlyout) - GetControlBounds(this.MinSlider)) / 2);
                var value = this.GetSelectedValue(translateTransform);
                this.MinSelectedValue = value;
            }
            else if (cumulative + GetControlBounds(this.MinSlider) >= this.DragArea.ActualWidth)
            {
                return;
            }
            else if (cumulative + (GetControlBounds(this.MinSlider) / 2) >= maxTranslateTransform.X)
            {
                return;
            }
            else
            {
                translateTransform.X = cumulative;
                minLabelTransform.X = cumulative
                                      - ((GetControlBounds(this.MinSliderFlyout) - GetControlBounds(this.MinSlider)) / 2);
                var value = this.GetSelectedValue(translateTransform);
                this.MinSelectedValue = value;
            }

            e.Handled = true;
        }

        private void OnMaxSliderManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (this.MaxSlider == null) return;

            var translateTransform = this.MaxSlider.RenderTransform as TranslateTransform;
            if (translateTransform == null) return;

            var minTranslateTransform = this.MinSlider.RenderTransform as TranslateTransform;
            if (minTranslateTransform == null) return;

            var maxLabelTransform = this.MaxSliderFlyout.RenderTransform as TranslateTransform;
            if (maxLabelTransform == null) return;

            var cumulative = this.maxStartPoint + e.Cumulative.Translation.X;
            if (cumulative < 0.0) return;

            if (cumulative + GetControlBounds(this.MaxSlider) >= this.DragArea.ActualWidth)
            {
                translateTransform.X = this.DragArea.ActualWidth - GetControlBounds(this.MaxSlider);
                maxLabelTransform.X = translateTransform.X
                                      - ((GetControlBounds(this.MaxSliderFlyout) - GetControlBounds(this.MaxSlider)) / 2);
                var value = this.GetSelectedValue(translateTransform);
                this.MaxSelectedValue = value;
            }
            else if (cumulative + (GetControlBounds(this.MaxSlider) / 2)
                     <= minTranslateTransform.X + GetControlBounds(this.MinSlider))
            {
                return;
            }
            else
            {
                translateTransform.X = cumulative;
                maxLabelTransform.X = cumulative
                                      - ((GetControlBounds(this.MaxSliderFlyout) - GetControlBounds(this.MaxSlider))
                                         / 2);
                var value = this.GetSelectedValue(translateTransform);
                this.MaxSelectedValue = value;
            }

            e.Handled = true;
        }

        private double GetSelectedValue(TranslateTransform translateTransform)
        {
            var fraction = translateTransform.X / (this.DragArea.ActualWidth - GetControlBounds(this.MaxSlider));
            var range = this.MaxValue - this.MinValue;
            var calculatedValue = range * fraction;
            var value = this.MinValue + calculatedValue;
            return value;
        }

        private static double GetControlBounds(FrameworkElement element)
        {
            return element.ActualWidth + element.Margin.Left + element.Margin.Right;
        }

        /// <summary>
        /// Resets the range slider.
        /// </summary>
        public void ResetControl()
        {
            this.MinSlider.RenderTransform = new TranslateTransform();
            this.MaxSlider.RenderTransform = new TranslateTransform();

            this.MinSliderFlyout.RenderTransform = new TranslateTransform();
            this.MaxSliderFlyout.RenderTransform = new TranslateTransform();

            try
            {
                this.MinSelectedValue = this.MinValue;
                this.MaxSelectedValue = this.MaxValue;
            }
            catch (InvalidCastException)
            {
                this.MinSelectedValue = 0;
                this.MaxSelectedValue = 0;
            }

            this.OnApplyTemplate();
        }
    }
}