namespace WinUX.Sample.Views
{
    using Windows.UI.Xaml.Controls;

    using WinUX.Sample.ViewModels;

    public sealed partial class BehaviorsView
    {
        private BehaviorsViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sample.MainPage"/> class.
        /// </summary>
        public BehaviorsView()
        {
            this.InitializeComponent();

            this.DataContext = this.ViewModel;
        }

        public BehaviorsViewModel ViewModel => this.viewModel ?? (this.viewModel = new BehaviorsViewModel());
    }
}