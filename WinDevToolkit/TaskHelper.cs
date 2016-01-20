using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace WinDevToolkit
{
    public static class TaskHelper
    {
        public static async Task HandleTaskObservedAsync(Task task, [CallerMemberName] string caller = "")
        {
            ExceptionDispatchInfo exception = null;

            try
            {
                await task;
            }
            catch (Exception e)
            {
                exception = ExceptionDispatchInfo.Capture(e);
            }

            if (exception != null)
            {
                var report = "IsCanceled: " + task.IsCanceled + "\nIsFaulted: " + task.IsFaulted + "\nException: " +
                             task.Exception + "\nInnerException: " + task.Exception.InnerException;
                await Messaging.ShowMessageDialogErrorAsync(report, caller);

                //exception.Throw();
            }
        }

        public static async Task<T> HandleTaskObservedAsync<T>(Task<T> task, [CallerMemberName] string caller = "")
        {
            ExceptionDispatchInfo exception = null;

            var res = default(T);
            try
            {
                res = await task;
            }
            catch (Exception e)
            {
                exception = ExceptionDispatchInfo.Capture(e);
            }

            if (exception != null)
            {
                var report = "IsCanceled: " + task.IsCanceled + "\nIsFaulted: " + task.IsFaulted + "\nException: " +
                             task.Exception + "\nInnerException: " + task.Exception.InnerException;
                await Messaging.ShowMessageDialogErrorAsync(report, caller);

                //exception.Throw();
            }
            return res;
        }
    }
}
