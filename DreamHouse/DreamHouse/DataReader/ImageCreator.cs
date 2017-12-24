using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.IO;
using DreamHouse.Models;

namespace DreamHouse.DataReader
{
    public class ImageCreator
    {
        public PavilionCreator pavcre { get; set; }
        public List<ImageModel> ImagesForPavilionsCollection { get; set; }

        public List<ImageModel> GetImageForPavilions()
        {
            pavcre = new PavilionCreator(); 
            ImagesForPavilionsCollection = new List<ImageModel>();
            foreach (var pav in pavcre.zerofloor)
            {
                ImagesForPavilionsCollection.Add(new ImageModel() { ImageName = pav.Shop.NumberPavilion, ImageForPavilion = ArrayConvertToImage(pav.Shop.PhotoPathShop2) }); 
            }
            return ImagesForPavilionsCollection; 
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
