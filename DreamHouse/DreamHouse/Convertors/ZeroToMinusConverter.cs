using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DreamHouse.Convertors
{
    public class ZeroToMinusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null)
            //{
            //    return null;
            //}
            string s = value.ToString();
            if (s == "0")
            {
                return "-1";
            }
            else { return s; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
