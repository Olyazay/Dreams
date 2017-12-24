namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel; 

    public partial class CarouselItemModels:INotifyPropertyChanged 
    {
        public int id { get; set; }

        //public string Heading { get; set; }

        //public string Description { get; set; }

        //public byte[] PhotoPath { get; set; }

        //public string NameObject { get; set; }
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

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChange("Description");
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
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        private string _nameObject;
        public string NameObject
        {
            get { return _nameObject; }
            set
            {
                _nameObject = value;
                OnPropertyChange("NameObject");
            }
        }
    }
}
