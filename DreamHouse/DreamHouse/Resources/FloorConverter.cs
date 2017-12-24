using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DreamHouse.Resources
{
    class FloorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString()==parameter.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(System.Convert.ToBoolean(value))
            {
                return int.Parse(parameter.ToString());
            }
            return null;
        }
    }
}
