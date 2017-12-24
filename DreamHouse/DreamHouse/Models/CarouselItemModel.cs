using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; 

namespace DreamHouse.Models
{
    [Serializable]
    public class CarouselItemModel
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

        private object _Object;
        public object Object
        {
            get { return _Object; }
            set
            {
                _Object = value;
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
        //private String _PathPhoto;
        //public String PathPhoto
        //{
        //    get { return _PathPhoto; }
        //    set
        //    {
        //        _PathPhoto = value;
        //        OnPropertyChange("PathPhoto");
        //    }
        //}
        public CarouselItemModel(string Heading, string Description, byte[] PhotoPath, string NameObject)
        {
            this.Heading = Heading;
            this.Description = Description;
            this.PhotoPath = PhotoPath;
            this.NameObject = NameObject ;
        }
        public CarouselItemModel()
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
