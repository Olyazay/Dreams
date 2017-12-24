namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel; 

    public partial class ShopModels:INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShopModels()
        {
            PavilionModels = new HashSet<PavilionModels>();

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
        public int Id { get; set; }

        public int? CategoryModelId { get; set; }

        //public string NameCategory { get; set; }

        //public string NumberPavilion { get; set; }
        private CategoryModels _categoryModel;
        public CategoryModels CategoryModel
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
                OnPropertyChange("NumberPavilion");
            }
        }

        //public string Place { get; set; }

        //public byte[] PhotoPathShop { get; set; }

        //public byte[] PhotoPathShop2 { get; set; }

        //public string IdShop { get; set; }

        //public string NameShop { get; set; }

        //public string ShopDescription { get; set; }

        //public string NumberShop { get; set; }

        //public string SiteShop { get; set; }

        //public int FloorNumber { get; set; }

        //public string SpotNumber { get; set; }

        //public string LogoPath { get; set; }

        //public string BackButtonColor { get; set; }

        public virtual CategoryModels CategoryModels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PavilionModels> PavilionModels { get; set; }
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
