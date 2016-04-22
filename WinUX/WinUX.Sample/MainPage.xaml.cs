// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample
{
    using System;

    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Messaging.Dialogs;
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

        private async void OnThrowExceptionClicked(object sender, RoutedEventArgs e)
        {
            await MessageDialogHelper.Instance.ShowAsync($"Log file can be found, if AppLogHandler has been started, here: '{ApplicationData.Current.LocalFolder.Path}'");

            throw new NotSupportedException("An exception has been thrown due to a button being pressed. This exception will be handled by the handler.");
        }
    }
}