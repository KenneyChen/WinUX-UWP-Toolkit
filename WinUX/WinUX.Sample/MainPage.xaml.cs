// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Sample.Views;

    /// <summary>
    /// The main page.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnBehaviorsClicked(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(BehaviorsView));
        }

        private void OnControlsClicked(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ControlsView));
        }

        private void OnStateTriggersClicked(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(StateTriggersView));
        }

        private void OnValidationClicked(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ValidationView));
        }

        private void OnValueConvertersClicked(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ValueConverterView));
        }
    }
}