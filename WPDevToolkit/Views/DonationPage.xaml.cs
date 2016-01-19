using Windows.UI.Xaml.Controls;
using WPDevToolkit.UI;
using WPDevToolkit.ViewModel;

namespace WPDevToolkit.Views
{
    public sealed partial class DonationPage : BasePage
    {
        //private readonly AboutViewModel _aboutVm;

        public DonationPage()
        {
            this.InitializeComponent();

            // set data context
            //_aboutVm = SingletonLocator.Get<AboutViewModel>();
            //DataContext = _aboutVm;
        }

        private async void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var lv = sender as ListView;
            if (lv != null)
            {
                var pi = e.ClickedItem as PurchaseItem;
                if (pi != null)
                {
                    await GetViewModel<IAboutViewModel>().BuyDonationAsync(pi);
                }
            }
        }
    }
}
