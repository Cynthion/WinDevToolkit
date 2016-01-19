using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.System;
using Ninject;
using WPDevToolkit.Interfaces;
using WPDevToolkit.Services;

namespace WPDevToolkit.ViewModel
{
    public class AboutViewModel : AsyncLoader, ILocatable, IAboutViewModel
    {
        public static IConstantsService Constants { get; private set; }
        
        public string Version { get; private set; }

        private IList<PurchaseItem> _donationAmounts;

        public AboutViewModel()
        {
            Constants = IocContainer.GetKernel().Get<IConstantsService>();
            Version = PhoneInteraction.GetAppVersion();

            var dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += DataTransferManager_OnDataRequested;
        }

        private static void DataTransferManager_OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();

            args.Request.Data.Properties.Title = Constants.AppName;
            args.Request.Data.SetText(string.Format("Hey, check out the {0} app for Windows Phone. {1}", Constants.AppName, Constants.TwitterHashtag));
#if DEBUG
            var marketplaceUri = CurrentAppSimulator.LinkUri;
#else
            var maretplaceUri = CurrentApp.LinkUri;
#endif
            args.Request.Data.SetUri(marketplaceUri);
            deferral.Complete();
        }

        protected override async Task<LoadResult> DoLoadAsync()
        {
            DonationAmounts = await IocContainer.GetKernel().Get<IStorageService>().LoadPurchasesAsync();
            return LoadResult.Success;
        }

        public async Task BuyDonationAsync(PurchaseItem purchaseItem)
        {
            var success = await WindowsStoreManager.BuyFeatureAsync(purchaseItem.Id);
            if (success)
            {
                purchaseItem.IsPurchased = true;
                await Messaging.ShowMessage(string.Format("{0} received!\n Thank you very much for your contribution.",  purchaseItem), "Thank you!");
            }

            // store purchase items
            await IocContainer.GetKernel().Get<IStorageService>().StorePurchasesAsync(_donationAmounts);
        }

        public async Task HandleRatingAsync()
        {
#if DEBUG
            var appId = CurrentAppSimulator.AppId;
#else
            var appId = CurrentApp.AppId;
#endif
            var reviewUri = new Uri(string.Format("ms-windows-store:reviewapp?appid={0}", appId));
            await Launcher.LaunchUriAsync(reviewUri);
        }

        public Task HandleFeedbackAsync()
        {
            return SendEmailAsync("Feedback", Constants.FeedbackEmailBody);
        }

        public void HandleSharing()
        {
            DataTransferManager.ShowShareUI();
        }

        public Task HandleBugReportAsync()
        {
            return SendEmailAsync("Bug Report", Constants.BugReportEmailBody);
        }

        private static async Task SendEmailAsync(string subject, string body)
        {
            var mail = new EmailMessage
            {
                Subject = string.Format("[{0}] {1}", Constants.AppNameShort, subject),
                Body = body
            };
            mail.To.Add(Constants.FeedbackEmailRecipient);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        public IList<PurchaseItem> DonationAmounts
        {
            get { return _donationAmounts; }
            private set
            {
                if (_donationAmounts != value)
                {
                    _donationAmounts = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IList<ReleaseNotes> ReleaseNotes
        {
            get { return WPDevToolkit.ReleaseNotes.GetReleaseNotes(); }
        }
    }
}
