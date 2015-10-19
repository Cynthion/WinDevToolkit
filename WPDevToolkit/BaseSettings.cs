using Windows.Foundation.Collections;
using Windows.Storage;

namespace WPDevToolkit
{
    public abstract class BaseSettings
    {
        // settings
        private const string IsFirstRunKey = "isfirstrun";

        private static void Set<T>(string settingKey, T value)
        {
            var settings = GetLocalSettings();
            if (settings.ContainsKey(settingKey) == false)
            {
                settings.Add(settingKey, value);
            }
            else
            {
                settings[settingKey] = value;
            }
        }

        private static T Get<T>(string settingKey)
        {
            var settings = GetLocalSettings();
            if (settings.ContainsKey(settingKey))
            {
                return (T)settings[settingKey];
            }
            return default(T);
        }

        private static IPropertySet GetLocalSettings()
        {
            return ApplicationData.Current.LocalSettings.Values;
        }

        public static bool IsFirstRun
        {
            get { return Get<bool>(IsFirstRunKey); }
            set { Set(IsFirstRunKey, value); }
        }
    }
}
