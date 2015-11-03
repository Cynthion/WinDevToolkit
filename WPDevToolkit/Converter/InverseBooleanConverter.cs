using System;
using Windows.UI.Xaml.Data;

namespace WPDevToolkit.Converter
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof (bool))
            {
                throw new InvalidOperationException("The target must be of type boolean!");
            }

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException("The target must be of type boolean!");
            }

            return !(bool)value;
        }
    }
}
