// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppLogHandler.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines a handler for logging as part of the <see cref="Application"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Diagnostics
{
    using System;
    using System.Diagnostics.Tracing;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.UI.Xaml;

    using WinUX.Extensions;
    using WinUX.Storage;

    /// <summary>
    /// Defines a handler for logging as part of the <see cref="Application"/>.
    /// </summary>
    public static class AppLogHandler
    {
        private static bool isHandling;

        private static StorageFile logFile;

        /// <summary>
        /// Starts the handler.
        /// </summary>
        public static async Task StartAsync()
        {
            if (Application.Current == null || isHandling)
            {
                return;
            }

            await SetupEventListener();

            Application.Current.UnhandledException += OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException += OnAppUnobservedTaskExceptionThrown;

            isHandling = true;
        }

        private static async Task SetupEventListener()
        {
            logFile =
                await
                ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt",
                    CreationCollisionOption.OpenIfExists);

            var listener = new LocalStorageEventListener(logFile);
            listener.EnableEvents(Logger.Log, EventLevel.Verbose);
        }

        /// <summary>
        /// Stops the handler.
        /// </summary>
        public static void Stop()
        {
            if (Application.Current == null || !isHandling)
            {
                return;
            }

            Application.Current.UnhandledException -= OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException -= OnAppUnobservedTaskExceptionThrown;

            isHandling = false;
        }

        private static void OnAppUnobservedTaskExceptionThrown(object sender, UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();

            if (args.Exception != null)
            {
                Logger.Log.Critical($"Unobserved task exception thrown. Error: '{args.Exception}'");

                if (!args.Exception.StackTrace.IsEmpty())
                {
                    Logger.Log.Info($"StackTrace: {args.Exception.StackTrace}");
                }
            }
            else
            {
                Logger.Log.Critical("Unobserved task exception thrown. No exception information available.");
            }
        }

        private static void OnAppUnhandledExceptionThrown(object sender, UnhandledExceptionEventArgs args)
        {
            args.Handled = true;

            Logger.Log.Critical(
                args.Exception != null
                    ? $"Unhandled exception thrown. Error: '{args.Exception}'"
                    : "Unhandled exception thrown. No exception information available.");
        }
    }
}