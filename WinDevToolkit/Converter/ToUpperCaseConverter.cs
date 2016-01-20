using System;
using Windows.UI.Xaml.Data;

namespace WinDevToolkit.Converter
{
    public class ToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                return value.ToString().ToUpper();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
