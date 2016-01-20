using System;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinDevToolkit.Converter
{
    public enum ScreenParameter
    {
        Width,
        Height,
        HalfWidth,
    }

    public class ScreenSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string)
            {
                var param = (ScreenParameter)Enum.Parse(typeof(ScreenParameter), (string) parameter);
                var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                double size = 0;

                switch (param)
                {
                    case ScreenParameter.Width:
                        size = Window.Current.Bounds.Width * scaleFactor;
                        break;
                    case ScreenParameter.Height:
                        size = Window.Current.Bounds.Height * scaleFactor;
                        break;
                    case ScreenParameter.HalfWidth:
                        size = (Window.Current.Bounds.Width * scaleFactor) / 2;
                        break;
                }
                return size;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
