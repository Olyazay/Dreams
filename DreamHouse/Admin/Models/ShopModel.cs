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

namespace Admin.Models
{
    [Serializable]
    public class ShopModel : INotifyPropertyChanged
    {
        [Key]
        [ForeignKey("Pavilion")]
        public int Id { get; set; }
        public PavilionModel Pavilion { get; set; }
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
                //    //string temp = Regex.Replace(_numberPavilion, @"[^\d]", "");
                //    //FloorNumber = int.Parse(temp.Substring(0, 1));
                //    //SpotNumber = int.Parse(temp.Substring(1,2));
                //}
                OnPropertyChange("NumberPavilion");
            }
        }
        private String _photoPathShop;
        public String PhotoPathShop
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
                _numberShop = value.Substring(1, 3) + " " + value.Substring(4, 3) + " " + value.Substring(7, 2) + " " + value.Substring(9, 2);
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
        private int _SpotNumber;
        public int SpotNumber
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

        public ShopModel(String NumberPavilion, String PhotoPathShop, String IdShop, String NameShop, string ShopDescription, String NumberShop, String SiteShop)
        {
            this.NumberPavilion = NumberPavilion;
            this.PhotoPathShop = PhotoPathShop;
            this.IdShop = IdShop;
            this.NameShop = NameShop;
            this.ShopDescription = ShopDescription;
            if (NumberShop != null)
            {
                this.NumberShop = Regex.Replace(NumberShop, @"[^\d]", "");
            }
            this.SiteShop = SiteShop;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }

        }
        public ShopModel()
        {

        }
    }
}
