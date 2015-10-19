using Windows.UI.Xaml;

namespace WPDevToolkit.Converter
{
    public class BooleanToVisibilityConverter : VisibilityConverter
    {
        protected override Visibility GetVisibility(object value, VisibilityParameter param)
        {
            if (value is bool)
            {
                switch (param)
                {
                    case VisibilityParameter.Normal:
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                    case VisibilityParameter.Inverse:
                        return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }
    }
}
