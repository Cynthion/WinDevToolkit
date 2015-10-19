using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WPDevToolkit.Converter
{
    public enum VisibilityParameter
    {
        Normal,
        Inverse
    }
    
    public abstract class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be Visibility!");

            var param = VisibilityParameter.Normal;
            if (parameter is string)
            {
                param = (VisibilityParameter)Enum.Parse(typeof(VisibilityParameter), (string) parameter);
            }
            
            return GetVisibility(value, param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        protected abstract Visibility GetVisibility(object value, VisibilityParameter param);
    }
}
