// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBarToggleButtonGrouping.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the AppBarToggleButtonGrouping type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Controls.ToggleButton
{
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    using WinUX.Extensions;

    /// <summary>
    /// An extension to <see cref="ToggleButton"/> to provide grouping.
    /// </summary>
    public class AppBarToggleButtonGrouping : DependencyObject
    {
        public static readonly DependencyProperty GroupParentProperty =
            DependencyProperty.RegisterAttached(
                "GroupParent",
                typeof(object),
                typeof(AppBarToggleButtonGrouping),
                new PropertyMetadata(null));

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached(
            "GroupName",
            typeof(string),
            typeof(AppBarToggleButtonGrouping),
            new PropertyMetadata(null, OnGroupNameChanged));

        /// <summary>
        /// Gets the group name for the given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="AppBarToggleButton"/>.
        /// </param>
        /// <returns>
        /// Returns the value of the <see cref="GroupNameProperty"/> for the given <see cref="DependencyObject"/>.
        /// </returns>
        public static string GetGroupName(DependencyObject obj)
        {
            return obj.GetValue(GroupNameProperty) as string;
        }

        /// <summary>
        /// Sets the group name for the given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="AppBarToggleButton"/>.
        /// </param>
        /// <param name="value">
        /// The value to set the <see cref="GroupNameProperty"/> for the given <see cref="DependencyObject"/>.
        /// </param>
        public static void SetGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Gets the group parent object for the given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="AppBarToggleButton"/>.
        /// </param>
        /// <returns>
        /// Returns the object representing the parent of the given <see cref="DependencyObject"/>.
        /// </returns>
        public static object GetGroupParent(DependencyObject obj)
        {
            return obj.GetValue(GroupParentProperty);
        }

        /// <summary>
        /// Sets the group parent object for the given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="AppBarToggleButton"/>.
        /// </param>
        /// <param name="value">
        /// The parent object to set the <see cref="GroupParentProperty"/> for the given <see cref="DependencyObject"/>.
        /// </param>
        public static void SetGroupParent(DependencyObject obj, object value)
        {
            obj.SetValue(GroupParentProperty, value);
        }

        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var toggleButton = d as AppBarToggleButton;
            if (toggleButton != null)
            {
                if (args.OldValue == null && args.NewValue != null)
                {
                    toggleButton.Checked += OnToggleButtonChecked;
                }
            }
        }

        private static void OnToggleButtonChecked(object sender, RoutedEventArgs args)
        {
            var toggleButton = sender as AppBarToggleButton;
            if (toggleButton != null)
            {
                var groupName = GetGroupName(toggleButton);
                var groupParent = GetGroupParent(toggleButton);

                var parent = groupParent == null ? toggleButton.GetAncestor<CommandBar>() : groupParent as UIElement;

                UpdateToggleState(parent, groupName, toggleButton);
            }
        }

        private static void UpdateToggleState(
            DependencyObject parentObject,
            string groupName,
            ToggleButton toggleButton)
        {
            var childGroupItems =
                parentObject?.GetDescendantsOfType<AppBarToggleButton>()
                    .Where(x => x != null && x != toggleButton)
                    .ToList();

            if (childGroupItems?.Count > 0)
            {
                if (toggleButton.IsChecked != null && toggleButton.IsChecked.Value)
                {
                    foreach (var toggle in from toggle in childGroupItems
                                           let toggleGroupName = GetGroupName(toggle)
                                           where toggleGroupName != null
                                           where toggleGroupName == groupName
                                           select toggle)
                    {
                        toggle.IsChecked = false;
                    }
                }
            }
        }
    }
}