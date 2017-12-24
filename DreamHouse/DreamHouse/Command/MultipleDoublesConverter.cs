using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace VremenaGoda.Converters
{
    public class MultipleDoublesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double left = System.Convert.ToDouble(value);
                double right = System.Convert.ToDouble(parameter);
                return left * right;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double left = System.Convert.ToDouble(value);
                double right = System.Convert.ToDouble(parameter);
                return left / right;
            }
            catch
            {
                return null;
            }
        }
    }
}