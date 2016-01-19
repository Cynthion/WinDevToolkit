using Windows.UI.Xaml;
using WPDevToolkit.UI;
using WPDevToolkit.ViewModel;

namespace WPDevToolkit.Views
{
    public sealed partial class ReleaseNotesPage : BasePage
    {
        //private readonly AboutViewModel _aboutVm;

        public ReleaseNotesPage()
        {
            this.InitializeComponent();

            //// set data context
            //_aboutVm = SingletonLocator.Get<AboutViewModel>();
            //DataContext = _aboutVm;

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await GetViewModel<IAboutViewModel>().LoadAsync();
        }
    }
}
