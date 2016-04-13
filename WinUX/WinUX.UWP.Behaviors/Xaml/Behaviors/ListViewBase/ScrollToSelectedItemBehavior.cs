// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrollToSelectedItemBehavior.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the ScrollToSelectedItemBehavior type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Behaviors.ListViewBase
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// A <see cref="Behavior"/> that scrolls to a selected item within a <see cref="ListViewBase"/> control such as <see cref="GridView"/> or <see cref="ListView"/>.
    /// </summary>
    public class ScrollToSelectedItemBehavior : Behavior
    {
        private ListViewBase ListViewBase => this.AssociatedObject as ListViewBase;

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(ScrollToSelectedItemBehavior),
            new PropertyMetadata(null, OnSelectedItemChanged));

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return this.GetValue(SelectedItemProperty);
            }
            set
            {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ScrollToSelectedItemBehavior;
            behavior?.ScrollToSelectedItem(e.NewValue);
        }

        private void ScrollToSelectedItem(object selectedItem)
        {
            if (this.ListViewBase != null && selectedItem != null)
            {
                this.ListViewBase.ScrollIntoView(selectedItem, ScrollIntoViewAlignment.Leading);
            }
        }
    }
}