using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;

namespace DreamHouse.Models
{
    [Serializable]
    public class AnnouncementsModel : INotifyPropertyChanged
    {

        public int id { get; set; }
        private string _Heading;
        public string Heading
        {
            get { return _Heading; }
            set
            {
                _Heading = value;
                OnPropertyChange("Heading");
            }
        }

        private string _Body;
        public string Body
        {
            get { return _Body; }
            set
            {
                _Body = value;
                OnPropertyChange("Body");
            }
        }

        private byte[] _PhotoPath;
        public byte[] PhotoPath
        {
            get { return _PhotoPath; }
            set
            {
                _PhotoPath = value;
                OnPropertyChange("PhotoPath");
            }
        }

        private byte[] _PreviewPhotoPath;
        public byte[] PreviewPhotoPath
        {
            get { return _PreviewPhotoPath; }
            set
            {
                _PreviewPhotoPath = value;
                OnPropertyChange("PreviewPhotoPath");
            }
        }

        public AnnouncementsModel(string heading, byte[] preview, string body, byte[] photo)
        {
            this.Heading = heading;
            this.PreviewPhotoPath = preview;
            this.Body = body;
            this.PhotoPath = photo;
        }
        public AnnouncementsModel()
        {

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
