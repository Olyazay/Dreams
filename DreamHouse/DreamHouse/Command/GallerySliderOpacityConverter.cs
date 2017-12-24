using System;
using System.Globalization;
using System.Windows.Data;

namespace VremenaGoda.Converters
{
    public class GallerySliderOpacityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double aWidth && values[1] is double xTranslate)
            {
                double halfAWidth = aWidth * .9;
                double opacity = Math.Abs(xTranslate) / halfAWidth;
                if(!double.IsNaN(opacity))
                    return 1 - opacity;
            }
            return 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}