using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace WPDevToolkit
{
    public static class Messaging
    {
        public static Task ShowMessageDialogErrorAsync(string content, [CallerMemberName]string caller = "")
        {
            return ShowMessage(content, caller);
        }

        public static Task ShowMessageDialogErrorAsync(Exception exception, [CallerMemberName]string caller = "")
        {
            return ShowMessage(exception.Message, caller);
        }

        public static Task ShowInternetUnavailableAsync(string msg = "Your phone cannot connect to the internet right now.")
        {
            return ShowMessage(msg, "Internet unavailable");
        }

        public static async Task ShowMessage(string msg, string title)
        {
            // show message on UI thread
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var md = new MessageDialog(msg, title);
                await md.ShowAsync();
            });
        }
    }
}
