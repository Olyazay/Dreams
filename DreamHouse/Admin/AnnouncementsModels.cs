namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel; 

    public partial class AnnouncementsModels:INotifyPropertyChanged
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
                OnPropertyChange("PhotoPath ");
            }
        }

        private byte[] _PreviewPhotoPath;
        public byte[] PreviewPhotoPath
        {
            get { return _PreviewPhotoPath; }
            set
            {
                _PreviewPhotoPath = value;
                OnPropertyChange("PreviewPhotoPath ");
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
        //public string Heading { get; set; }

        //public string Body { get; set; }

        //public byte[] PhotoPath { get; set; }

        //public byte[] PreviewPhotoPath { get; set; }
    }
}
