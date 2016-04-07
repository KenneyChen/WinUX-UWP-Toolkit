// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the HeaderedTextBlock type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUX.Xaml.Controls
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Exceptions;
    using WinUX.Extensions;

    /// <summary>
    /// A headered text block control.
    /// </summary>
    public sealed class HeaderedTextBlock : Control
    {
        private static readonly DependencyProperty CollapseIfTextNullOrEmptyProperty =
            DependencyProperty.Register(
                nameof(CollapseIfTextNullOrEmpty),
                typeof(bool),
                typeof(HeaderedTextBlock),
                new PropertyMetadata(true));

        private static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(null, HeaderChanged));

        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(null, TextChanged));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        public HeaderedTextBlock()
        {
            this.DefaultStyleKey = typeof(HeaderedTextBlock);
            this.CollapseIfTextNullOrEmpty = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to collapse if text is null or empty.
        /// </summary>
        public bool CollapseIfTextNullOrEmpty
        {
            get
            {
                return (bool)this.GetValue(CollapseIfTextNullOrEmptyProperty);
            }
            set
            {
                this.SetValue(CollapseIfTextNullOrEmptyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get
            {
                return (string)this.GetValue(HeaderProperty);
            }
            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }
            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header presenter.
        /// </summary>
        public TextBlock HeaderPresenter { get; set; }

        /// <summary>
        /// Gets or sets the text value.
        /// </summary>
        public TextBlock TextValue { get; set; }

        private static void HeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as HeaderedTextBlock;
            if (control == null)
            {
                throw new InvalidOperationException("Control must be of type HeaderedTextBlock");
            }

            if (control.HeaderPresenter == null)
            {
                return;
            }

            if (control.CollapseIfTextNullOrEmpty && ShouldCollapse(control.Text))
            {
                // Header has updated but we're requesting to collapse if the text is empty and it currently is, make sure we don't update the header visibility.
                control.HeaderPresenter.Visibility = Visibility.Collapsed;

                // While we're at it, we will collapse the text field if it hasn't already been
                if (control.TextValue != null)
                {
                    control.TextValue.Visibility = Visibility.Collapsed;
                }

                // And for good measure, we'll collapse the control too.
                control.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Header has updated, need to check if the header content should cause the header to collapse or not.
                control.HeaderPresenter.Visibility = string.IsNullOrWhiteSpace(control.Header)
                                                         ? Visibility.Collapsed
                                                         : Visibility.Visible;

                // If the text field is available, make it visible.
                if (control.TextValue != null)
                {
                    control.TextValue.Visibility = Visibility.Visible;
                }

                // And for good measure, show the control.
                control.Visibility = Visibility.Visible;
            }

            control.HeaderPresenter.Text = control.Header;
        }

        private static void TextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as HeaderedTextBlock;
            if (control == null)
            {
                throw new InvalidOperationException("Control must be of type HeaderedTextBlock");
            }

            if (control.TextValue == null)
            {
                return;
            }

            control.UpdateControlVisibility();
        }

        /// <summary>
        /// Called when applying the template.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the template controls are missing.
        /// </exception>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.HeaderPresenter = this.GetTemplateChild("HeaderPresenter") as TextBlock;
            if (this.HeaderPresenter == null)
            {
                throw new ControlNotFoundException(
                    "Cannot find the HeaderPresenter named TextBlock control in the template.");
            }

            this.TextValue = this.GetTemplateChild("TextValue") as TextBlock;
            if (this.TextValue == null)
            {
                throw new ControlNotFoundException("Cannot find the TextValue named TextBlock control in the template.");
            }

            this.HeaderPresenter.Text = this.Header;

            this.UpdateControlVisibility();
        }

        private void UpdateControlVisibility()
        {
            if (this.CollapseIfTextNullOrEmpty && ShouldCollapse(this.Text))
            {
                if (this.HeaderPresenter != null)
                {
                    this.HeaderPresenter.Visibility = Visibility.Collapsed;
                }

                this.TextValue.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (this.HeaderPresenter != null)
                {
                    this.HeaderPresenter.Visibility = !string.IsNullOrWhiteSpace(this.Header)
                                                          ? Visibility.Visible
                                                          : Visibility.Collapsed;
                }

                this.TextValue.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Visible;
            }

            this.TextValue.Text = this.Text ?? string.Empty;
        }

        private static bool ShouldCollapse(string text)
        {
            return text.IsEmpty() || CheckFalseBoolean(text);
        }

        private static bool CheckFalseBoolean(string text)
        {
            bool isTrue;

            var parse = bool.TryParse(text, out isTrue);
            return parse && !isTrue;
        }
    }
}