using Windows.UI.Xaml;

namespace WinDevToolkit.Converter
{
    public class BooleanToVisibilityConverter : VisibilityConverter
    {
        protected override Visibility GetVisibility(object value, VisibilityParameter param)
        {
            if (value is bool)
            {
                switch (param)
                {
                    case VisibilityParameter.Inverse:
                        return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                    case VisibilityParameter.Normal:
                    default:
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }
    }
}
