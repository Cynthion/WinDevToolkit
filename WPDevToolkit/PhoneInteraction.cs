using Windows.Networking.Connectivity;

namespace WPDevToolkit
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
    }
}
