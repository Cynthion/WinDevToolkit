using System;
using System.Threading.Tasks;

namespace WPDevToolkit.Services
{
    public abstract class InitializerBase
    {
        public Task InitializeAsync()
        {
            return DoInitializeAsync();
        }

        protected abstract Task DoInitializeAsync();

        protected static bool IsNewerAppVersion(SettingsBase settings)
        {
            var currentVersion = PhoneInteraction.GetAppVersion();
            var storedVersion = settings.AppVersion;

            return string.Compare(currentVersion, storedVersion, StringComparison.Ordinal) > 0;
        }
    }
}
