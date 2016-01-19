using Windows.Foundation.Collections;
using Windows.Storage;

namespace WPDevToolkit.Services
{
    public abstract class SettingsBase
    {
        // settings
        public const string IsFirstRunKey = "isfirstrun";
        public const string AppVersionKey = "appVersion";
        
        public static void Store<T>(string settingKey, T value)
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

        public static T Load<T>(string settingKey)
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

        public bool IsFirstRun
        {
            // take inverse, due to bool default being false
            get { return !Load<bool>(IsFirstRunKey); }
            set { Store(IsFirstRunKey, !value); }
        }

        public string AppVersion
        {
            get { return Load<string>(AppVersionKey); }
            set { Store(AppVersionKey, value); }
        }
    }
}
