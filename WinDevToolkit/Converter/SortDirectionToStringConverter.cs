using System;
using Windows.UI.Xaml.Data;

namespace WinDevToolkit.Converter
{
    public class SortDirectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(string))
            {
                throw new InvalidOperationException("The target must be of type string!");
            }

            return (bool)value ? "Ascending" : "Descending";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
