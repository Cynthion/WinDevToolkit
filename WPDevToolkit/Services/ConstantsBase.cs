using Windows.ApplicationModel.Email;

namespace WPDevToolkit.Services
{
    public abstract class ConstantsBase : BaseNotifyPropertyChanged, IConstantsService
    {
        public string AppName { get; private set; }
        public string AppNameShort { get; private set; }
        public string TwitterHashtag { get; private set; }
        public string FeedbackEmailBody { get; private set; }
        public EmailRecipient FeedbackEmailRecipient { get; private set; }
        public string BugReportEmailBody { get; private set; }
        public EmailRecipient BugReportEmailRecipient { get; private set; }

        protected ConstantsBase(
            string appName,
            string appNameShort,
            string twitterHashTag
            )
        {
            AppName = appName;
            AppNameShort = appNameShort;
            TwitterHashtag = twitterHashTag;

            FeedbackEmailBody = string.Format("Hey Chris,\n\nI wanted to provide some feedback about the '{0}' app:\n\n", AppName);
            FeedbackEmailRecipient = new EmailRecipient("chris.windev@outlook.com", "Christian Lüthold");
            BugReportEmailBody = string.Format("Hey Chris,\n\nI wanted to let you know that I found a bug in the '{0}' app:\n\n", AppName);
            BugReportEmailRecipient = FeedbackEmailRecipient;
        }
    }
}
