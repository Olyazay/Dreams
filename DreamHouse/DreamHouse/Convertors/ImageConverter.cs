using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Globalization;
using System.Drawing;
using System.Windows.Data;

namespace DreamHouse.Convertors
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] array = value as byte[];
                BitmapImage bmImage = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(array, 0, array.Length))
                {
                    bmImage.BeginInit();
                    bmImage.CacheOption = BitmapCacheOption.OnLoad;
                    bmImage.StreamSource = ms;
                    bmImage.EndInit();
                }
                array = null;
                
                return bmImage;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                BitmapImage bmImage = value as BitmapImage;
                Bitmap image = new Bitmap(bmImage.BaseUri.ToString());
                ImageFormat format = image.RawFormat;
                //using (WrappingStream wrapper = new WrappingStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    return ms.ToArray();
                }
            }
            return null;
        }
        public byte[] ImageConverterToArray(string path)
        {

            Bitmap image = new Bitmap(path);
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }

        }
        public BitmapImage ArrayConvertToImage(byte[] array)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(array, 0, array.Length))
            {
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = ms;
                bmImage.EndInit();

                return bmImage;
            }
        }
    }
}


