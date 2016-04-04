// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample
{
    using NotificationsExtensions.Toasts;

    using System;

    using Windows.UI.Notifications;
    using Windows.UI.Xaml;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
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

        private void OnSnoozeAndDismissClicked(object sender, RoutedEventArgs e)
        {
            var notification = new ToastContent
            {
                Visual = new ToastVisual
                {
                    TitleText = new ToastText
                    {
                        Text = "Hello, World!"
                    },
                    BodyTextLine1 = new ToastText
                    {
                        Text = string.Format("Alarm - {0}", DateTime.Now.ToString("hh:mm tt"))
                    }
                },
                Launch = "HelloWorldAlarm",
                Scenario = ToastScenario.Reminder,
                Actions = new ToastActionsSnoozeAndDismiss()
            };

            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(notification.GetXml()));
        }
    }
}