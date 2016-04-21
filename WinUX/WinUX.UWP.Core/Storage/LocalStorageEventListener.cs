// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalStorageEventListener.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines an EventListener which stores the fired events within a StorageFile local to the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;

    /// <summary>
    /// Defines an EventListener which stores the fired events within a StorageFile local to the application.
    /// </summary>
    public sealed class LocalStorageEventListener : EventListener
    {
        private readonly StorageFile eventLog;

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public LocalStorageEventListener(StorageFile eventLogFile)
        {
            this.eventLog = eventLogFile;
        }

        /// <summary>
        /// Called whenever an event has been written by an event source for which the event listener has enabled events.
        /// </summary>
        /// <param name="eventData">The event arguments that describe the event.</param>
        protected override async void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (this.eventLog == null)
            {
                return;
            }

            var newLogEntry = new[]
                                  {
                                      $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\t{eventData.Level}\tMessage: '{eventData.Payload[0]}'"
                                  };

            await this.WriteLogEntryToFileAsync(newLogEntry);
        }

        private async Task WriteLogEntryToFileAsync(IEnumerable<string> logEntries)
        {
            await this.semaphore.WaitAsync();

            await Task.Run(
                async () =>
                    {
                        try
                        {
                            await FileIO.AppendLinesAsync(this.eventLog, logEntries);
                        }
                        catch (Exception)
                        {
                            // We're having an issue saving to the file but if we bubble this up, we'll get caught in a loop.
                        }
                        finally
                        {
                            this.semaphore.Release();
                        }
                    });
        }
    }
}