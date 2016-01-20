using Windows.ApplicationModel;
using Windows.Networking.Connectivity;

namespace WinDevToolkit
{
    public static class PhoneInteraction
    {
        private static readonly ConnectionProfile InternetProfile = NetworkInformation.GetInternetConnectionProfile();

        public static bool IsInternetAvailable()
        {
            // TODO check correctness
            return InternetProfile != null &&
                   InternetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }

        public static string GetAppVersion()
        {
            // according to http://stackoverflow.com/questions/3098167/why-is-system-version-in-net-defined-as-major-minor-build-revision
            var version = Package.Current.Id.Version;
            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
