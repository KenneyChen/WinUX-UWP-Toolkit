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
    public class AppLogHandler
    {
        private static AppLogHandler instance;

        /// <summary>
        /// Gets a static instance of the <see cref="AppLogHandler"/>.
        /// </summary>
        public static AppLogHandler Instance => instance ?? (instance = new AppLogHandler());

        private bool isHandling;

        private StorageFile logFile;

        /// <summary>
        /// Starts the handler.
        /// </summary>
        public async Task StartAsync()
        {
            if (Application.Current == null || this.isHandling)
            {
                return;
            }

            await this.SetupEventListener();

            Application.Current.UnhandledException += this.OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException += this.OnAppUnobservedTaskExceptionThrown;

            this.isHandling = true;
        }

        private async Task SetupEventListener()
        {
            this.logFile =
                await
                ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt",
                    CreationCollisionOption.OpenIfExists);

            var listener = new LocalStorageEventListener(this.logFile);
            listener.EnableEvents(Logger.Log, EventLevel.Verbose);
        }

        /// <summary>
        /// Stops the handler.
        /// </summary>
        public void Stop()
        {
            if (Application.Current == null || !this.isHandling)
            {
                return;
            }

            Application.Current.UnhandledException -= this.OnAppUnhandledExceptionThrown;
            TaskScheduler.UnobservedTaskException -= this.OnAppUnobservedTaskExceptionThrown;

            this.isHandling = false;
        }

        private void OnAppUnobservedTaskExceptionThrown(object sender, UnobservedTaskExceptionEventArgs args)
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

        private void OnAppUnhandledExceptionThrown(object sender, UnhandledExceptionEventArgs args)
        {
            args.Handled = true;

            Logger.Log.Critical(
                args.Exception != null
                    ? $"Unhandled exception thrown. Error: '{args.Exception}'"
                    : "Unhandled exception thrown. No exception information available.");
        }
    }
}