using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Xml.Serialization; 


namespace DreamHouse.Models
{
    [Serializable]
    public class ShopModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int? CategoryModelId { get; set; }
        [XmlIgnore]
        private CategoryModel _categoryModel;
        public CategoryModel CategoryModel
        {
            get
            {
                return _categoryModel;
            }
            set
            {
                _categoryModel = value;
                OnPropertyChange("CategoryModel");
            }
        }
        private String _nameCategory;
        public String NameCategory
        {
            get
            {
                return _nameCategory;
            }
            set
            {
                _nameCategory = value;
                OnPropertyChange("NameCategory");
            }
        }
        private String _numberPavilion;
        public String NumberPavilion
        {
            get
            {
                return _numberPavilion;
            }
            set
            {
                _numberPavilion = value;
                //if (_numberPavilion != null)
                //{
                //    string temp = Regex.Replace(_numberPavilion, @"[^\d]", "");
                //    FloorNumber = int.Parse(temp.Substring(0, 1));
                //    if (temp.Count() >= 3)
                //    {
                //        SpotNumber = temp.Substring(1, 2);
                //    }
                //    else
                //    {
                //        SpotNumber = string.Empty;
                //    }
                //}
                OnPropertyChange("NumberPavilion");
            }
        }
        private String _Place;
        public String Place
        {
            get
            {
                return _Place;
            }
            set
            {
                _Place = value;
                OnPropertyChange("Place");
            }
        }
        private byte[] _photoPathShop;
        public byte[] PhotoPathShop
        {
            get
            {
                return _photoPathShop;
            }
            set
            {
                _photoPathShop = value;
                OnPropertyChange("PhotoPathShop");
            }
        }
        private byte[] _photoPathShop2;
        public byte[] PhotoPathShop2
        {
            get
            {
                return _photoPathShop2;
            }
            set
            {
                _photoPathShop2 = value;
                OnPropertyChange("PhotoPathShop2");
            }
        }
        private String _idShop;
        public String IdShop
        {
            get
            {
                return _idShop;
            }
            set
            {
                _idShop = value;
                OnPropertyChange("IdShop");
            }
        }
        private String _nameShop;
        public String NameShop
        {
            get
            {
                return _nameShop;
            }
            set
            {
                _nameShop = value;
                OnPropertyChange("NameShop");
            }
        }
        private string _shopDescription;
        public string ShopDescription
        {
            get
            {
                return _shopDescription;
            }
            set
            {
                _shopDescription = value;
                OnPropertyChange("ShopDescription");
            }
        }
        private String _numberShop;
        public String NumberShop
        {
            get
            {
                return _numberShop;
            }
            set
            {
                _numberShop = value; 
                //_numberShop = value.Substring(1, 3) + " " + value.Substring(4, 3) + " " + value.Substring(7, 2) + " " + value.Substring(9, 2);
                OnPropertyChange("NumberShop");
            }
        }
        private String _siteShop;
        public String SiteShop
        {
            get
            {
                return _siteShop;
            }
            set
            {
                _siteShop = value;
                OnPropertyChange("SiteShop");
            }
        }
        private int _FloorNumber;
        public int FloorNumber
        {
            get { return _FloorNumber; }
            set
            {
                _FloorNumber = value;
            }
        }
        private string _SpotNumber;
        public string SpotNumber
        {
            get { return _SpotNumber; }
            set
            {
                _SpotNumber = value;
            }
        }
        private string _LogoPath;
        public string LogoPath
        {
            get { return _LogoPath; }
            set
            {
                _LogoPath = value;
            }
        }
        private string _BackButtonColor;
        public string BackButtonColor
        {
            get { return _BackButtonColor; }
            set
            {
                _BackButtonColor = value;
            }
        }
        public ShopModel(byte[] PhotoPathShop,byte[] PhotoPathShop2,CategoryModel CategoryModel, String NameCategory, String NumberPavilion, String IdShop, String NameShop, string ShopDescription, String NumberShop, String SiteShop)
        {
            this.PhotoPathShop2 = PhotoPathShop2; 
            this.CategoryModel = CategoryModel;
            this.NameCategory = NameCategory;
            this.NumberPavilion = NumberPavilion;
            this.PhotoPathShop = PhotoPathShop;
            this.IdShop = IdShop;
            this.NameShop = NameShop;
            this.ShopDescription = ShopDescription;
            if(NumberShop!=null)
            {
                this.NumberShop = Regex.Replace(NumberShop, @"[^\d]", "");
            }
            this.SiteShop = SiteShop;
        }

        public ShopModel()
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
