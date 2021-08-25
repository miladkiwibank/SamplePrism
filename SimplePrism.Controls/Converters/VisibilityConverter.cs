using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimplePrism.Presentation.Controls.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = parameter == null || bool.Parse(parameter as string);
            bool flag2 = (bool)value;
            return (flag2 == flag) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return visibility == Visibility.Visible;
        }
    }
}