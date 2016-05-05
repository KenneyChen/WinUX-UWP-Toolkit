// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlsView.xaml.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample.Views
{
    using System;

    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// The controls view.
    /// </summary>
    public sealed partial class ControlsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlsView"/> class.
        /// </summary>
        public ControlsView()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Called when the view is navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation args.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RegisterDefaultRangeSlider();
            this.RegisterTimeSpanRangeSlider();
        }

        /// <summary>
        /// Called when the view is navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation args.
        /// </param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.DefaultRangeSlider.MinSelectedValueChanged -= this.DefaultRangeSliderOnMinSelectedValueChanged;
            this.DefaultRangeSlider.MaxSelectedValueChanged -= this.DefaultRangeSliderOnMaxSelectedValueChanged;

            this.TimeSpanRangeSlider.MinSelectedValueChanged -= this.TimeSpanRangeSliderOnMinSelectedValueChanged;
            this.TimeSpanRangeSlider.MaxSelectedValueChanged -= this.TimeSpanRangeSliderOnMaxSelectedValueChanged;

            base.OnNavigatedFrom(e);
        }

        #region TimeSpanRangeSlider

        private void RegisterTimeSpanRangeSlider()
        {
            this.TimeSpanRangeSliderMinimumValue.Text = ConvertToTimeSpanString(this.TimeSpanRangeSlider.MinSelectedValue);
            this.TimeSpanRangeSliderMaximumValue.Text = ConvertToTimeSpanString(this.TimeSpanRangeSlider.MaxSelectedValue);

            this.TimeSpanRangeSlider.MinSelectedValueChanged += this.TimeSpanRangeSliderOnMinSelectedValueChanged;
            this.TimeSpanRangeSlider.MaxSelectedValueChanged += this.TimeSpanRangeSliderOnMaxSelectedValueChanged;
        }

        private void TimeSpanRangeSliderOnMaxSelectedValueChanged(object o, double d)
        {
            this.TimeSpanRangeSliderMaximumValue.Text = ConvertToTimeSpanString(d);
        }

        private void TimeSpanRangeSliderOnMinSelectedValueChanged(object o, double d)
        {
            this.TimeSpanRangeSliderMinimumValue.Text = ConvertToTimeSpanString(d);
        }

        private static string ConvertToTimeSpanString(double value)
        {
            var timeSpan = TimeSpan.FromSeconds(value);
            return timeSpan.ToString(@"mm\:ss\.ff");
        }

        #endregion

        #region DefaultRangeSlider

        private void RegisterDefaultRangeSlider()
        {
            this.DefaultRangeSliderMinimumValue.Text = this.DefaultRangeSlider.MinSelectedValue.ToString();
            this.DefaultRangeSliderMaximumValue.Text = this.DefaultRangeSlider.MaxSelectedValue.ToString();

            this.DefaultRangeSlider.MinSelectedValueChanged += this.DefaultRangeSliderOnMinSelectedValueChanged;
            this.DefaultRangeSlider.MaxSelectedValueChanged += this.DefaultRangeSliderOnMaxSelectedValueChanged;
        }

        private void DefaultRangeSliderOnMinSelectedValueChanged(object o, double d)
        {
            this.DefaultRangeSliderMinimumValue.Text = d.ToString();
        }

        private void DefaultRangeSliderOnMaxSelectedValueChanged(object o, double d)
        {
            this.DefaultRangeSliderMaximumValue.Text = d.ToString();
        }

        #endregion
    }
}
