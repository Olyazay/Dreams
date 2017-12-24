using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace DreamHouse.Models
{
    public class ImageModel:INotifyPropertyChanged
    {
        private BitmapImage _imageForPavilion;
        public BitmapImage ImageForPavilion
        {
            get
            {
                return _imageForPavilion;
            }
            set
            {
                _imageForPavilion = value;
                OnPropertyChange("ImageForPavilion");
            }
        }
        private string _imageName;
        public string ImageName
        {
            get
            {
                return _imageName;
            }
            set
            {
                _imageName = value;
                OnPropertyChange("ImageName");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }

        }
    }
}
