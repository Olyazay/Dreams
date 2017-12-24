using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DreamHouse.Convertors
{
    public class MyImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;

            try
            { 

                //RELATIVE
                return new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + path));
            }
            catch (Exception)
            {
                return new BitmapImage();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
