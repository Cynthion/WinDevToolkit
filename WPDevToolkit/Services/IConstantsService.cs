using Windows.ApplicationModel.Email;

namespace WPDevToolkit.Services
{
    public interface IConstantsService
    {
        string AppName { get; }
        string AppNameShort { get; }
        string TwitterHashtag { get; }
        string FeedbackEmailBody { get; }
        EmailRecipient FeedbackEmailRecipient { get; }
        string BugReportEmailBody { get; }
        EmailRecipient BugReportEmailRecipient { get; }
    }
}