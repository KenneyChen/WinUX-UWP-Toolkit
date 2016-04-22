// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the NotificationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Messaging.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NotificationsExtensions.Toasts;

    using Windows.UI.Notifications;

    /// <summary>
    /// A service for providing Windows <see cref="ToastNotification"/>.
    /// </summary>
    public class NotificationService
    {
        private static NotificationService instance;

        /// <summary>
        /// Gets a static instance of the <see cref="NotificationService"/>.
        /// </summary>
        public static NotificationService Instance => instance ?? (instance = new NotificationService());

        /// <summary>
        /// Shows a toast notification without launch events.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        public void ShowNotification(string title, string body)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var visualPart = new ToastVisual { TitleText = new ToastText { Text = title } };

            if (!string.IsNullOrWhiteSpace(body))
            {
                visualPart.BodyTextLine1 = new ToastText { Text = body };
            }

            var notification = new ToastContent { Visual = visualPart, Scenario = ToastScenario.Default };

            this.ShowNotification(notification);
        }

        /// <summary>
        /// Shows a launchable toast notification.
        /// </summary>
        /// <param name="launchString">
        /// The launch string.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        public void ShowNotificationWithLaunchArgs(string launchString, string title, string body)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (string.IsNullOrWhiteSpace(launchString))
            {
                throw new ArgumentNullException(nameof(launchString));
            }

            var visualPart = new ToastVisual { TitleText = new ToastText { Text = title } };

            if (!string.IsNullOrWhiteSpace(body))
            {
                visualPart.BodyTextLine1 = new ToastText { Text = body };
            }

            var notification = new ToastContent
                                   {
                                       Visual = visualPart,
                                       Launch = launchString,
                                       Scenario = ToastScenario.Default
                                   };

            this.ShowNotification(notification);
        }

        /// <summary>
        /// Shows an actionable toast notification.
        /// </summary>
        /// <param name="launchString">
        /// The launch string.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="buttons">
        /// The buttons.
        /// </param>
        public void ShowActionableNotification(
            string launchString,
            string title,
            string body,
            List<ToastButton> buttons)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (string.IsNullOrWhiteSpace(launchString))
            {
                throw new ArgumentNullException(nameof(launchString));
            }

            if (buttons == null)
            {
                throw new ArgumentNullException(nameof(buttons));
            }

            if (!buttons.Any())
            {
                throw new InvalidOperationException("No buttons passed through.");
            }

            if (buttons.Count > 5)
            {
                throw new InvalidOperationException("Cannot have more than 5 buttons.");
            }

            var visualPart = new ToastVisual { TitleText = new ToastText { Text = title } };

            if (!string.IsNullOrWhiteSpace(body))
            {
                visualPart.BodyTextLine1 = new ToastText { Text = body };
            }

            var actionPart = new ToastActionsCustom();

            foreach (var button in buttons)
            {
                actionPart.Buttons.Add(button);
            }

            var notification = new ToastContent
                                   {
                                       Visual = visualPart,
                                       Launch = launchString,
                                       Scenario = ToastScenario.Default,
                                       Actions = actionPart
                                   };

            this.ShowNotification(notification);
        }

        /// <summary>
        /// Fires a notification.
        /// </summary>
        /// <param name="notification">
        /// The notification.
        /// </param>
        public void ShowNotification(ToastContent notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(notification.GetXml()));
        }
    }
}