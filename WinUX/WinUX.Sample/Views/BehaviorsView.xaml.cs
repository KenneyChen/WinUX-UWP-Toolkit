// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorsView.xaml.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the view for demonstrating the use of WinUX behaviors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Sample.Views
{
    using System;

    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the view for demonstrating the use of WinUX behaviors.
    /// </summary>
    public sealed partial class BehaviorsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sample.MainPage"/> class.
        /// </summary>
        public BehaviorsView()
        {
            this.InitializeComponent();
        }

        private async void OnFileThumbnailButtonClicked(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
                                 {
                                     ViewMode = PickerViewMode.List,
                                     CommitButtonText = "Open file",
                                     SuggestedStartLocation = PickerLocationId.DocumentsLibrary
                                 };

            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".docx");
            openPicker.FileTypeFilter.Add(".pdf");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".ini");
            openPicker.FileTypeFilter.Add(".psd");
            openPicker.FileTypeFilter.Add(".exe");
            openPicker.FileTypeFilter.Add(".ppt");
            openPicker.FileTypeFilter.Add(".zip");
            openPicker.FileTypeFilter.Add(".vsix");
            openPicker.FileTypeFilter.Add(".nupkg");
            openPicker.FileTypeFilter.Add(".msi");


            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                this.FileThumbnailBehavior.File = file;
            }
        }
    }
}