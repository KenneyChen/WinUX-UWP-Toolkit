namespace WinUX.Xaml.Behaviors.Button
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    public class ButtonClickedBehavior : Behavior
    {
        public Button Button => this.AssociatedObject as Button;

        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            "Actions",
            typeof(ActionCollection),
            typeof(ButtonClickedBehavior),
            new PropertyMetadata(default(ActionCollection)));

        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)this.GetValue(ActionsProperty);
                if (actions != null)
                {
                    return actions;
                }

                actions = new ActionCollection();
                this.SetValue(ActionsProperty, actions);

                return actions;
            }
        }

        protected override void OnAttached()
        {
            if (this.Button != null)
            {
                this.Button.Click += this.OnButtonClicked;
            }
        }

        private void OnButtonClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            Interaction.ExecuteActions(this.Button, this.Actions, routedEventArgs);
        }

        protected override void OnDetaching()
        {
            if (this.Button != null)
            {
                this.Button.Click -= this.OnButtonClicked;
            }
        }
    }
}