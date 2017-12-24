using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Admin.FileProcessing;
using System.Collections.ObjectModel;
using Admin.Models;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Admin.Commands;
using System.Data.Entity;

namespace Admin.ViewModel
{
    public class ShopViewModel:INotifyPropertyChanged
    {
        DataLoader dl = DataLoader.GetLoadShopsCreater();
        ShopsCreater cr = ShopsCreater.GetLoadShopsCreater();
        public ObservableCollection<CategoryModels> CategoryCollection { get; set; }
        public CaruselLoader cl { get; set; }

        public ObservableCollection<FloorModels> PavilionCollection { get; set; }
        public ObservableCollection<CarouselItemModels> CaruselsCollections { get; set; }

        public ObservableCollection<AnnouncementsModels> AnnonsmentCollection { get; set; }
        public ShopViewModel()
        {
                PavilionCollection = dl.GetFloors();
                cl = new CaruselLoader();
                CategoryCollection = cr.GetShopList();
                CaruselsCollections = new ObservableCollection<CarouselItemModels>();
                CaruselsCollections = cl.CarouselItemCollection;
                FreeAre();
            
        }
        public void FreeAre()
        {
            foreach (var floors in dl.GetFloors())
            {
                foreach (var pav in floors.PavilionModels)
                {
                    if (pav.ShopModels == null)
                    {
                        pav.ShopModels.NameShop = "Свободно";
                    }
                }
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
        private PavilionModels _selectedShop;
        public PavilionModels SelectedShop
        {
            get
            {
                return _selectedShop;
            }
            set
            {
                _selectedShop = value;
                OnPropertyChange("SelectedShop");
                GetDataAboutShop();
            }
        }
        private byte[] _image1;
        public byte[] Image1
        {
            get
            {
                return _image1;
            }
            set
            {
                _image1 = value;
                OnPropertyChange("Image1");
            }
        }
        private byte[] _image2;
        public byte[] Image2
        {
            get
            {
                return _image2;
            }
            set
            {
                _image2 = value;
                OnPropertyChange("Image2");
            }
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
        public void GetDataAboutShop()
        {
            if (SelectedShop != null)
            {
                using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                {
                    PavilionModels pav = bd.PavilionModels.Find(SelectedShop.Id);
                    NameShop = SelectedShop.ShopModels.NameShop;
                    ShopDescription = SelectedShop.ShopModels.ShopDescription;
                    SiteShop = SelectedShop.ShopModels.SiteShop;
                    NumberShop = SelectedShop.ShopModels.NumberShop;
                }
            }
        }
        private Command _saveOpenedImage;
        public Command SaveOpenedImage => _saveOpenedImage ?? (_saveOpenedImage = new Command(obj =>
        {
            PavilionModels newpavilion = obj as PavilionModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (((string.IsNullOrWhiteSpace(path)) == false) && SelectedShop != null)
            {
                SelectedShop.ShopModels.PhotoPathShop = ImageConverterToArray(path);
                Image1 = ImageConverterToArray(path);
            }

        }
        ));
        private Command _saveOpenedImage2;
        public Command SaveOpenedImage2 => _saveOpenedImage2 ?? (_saveOpenedImage2 = new Command(obj =>
        {
            PavilionModels newpavilion = obj as PavilionModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (path != null && path != "" && SelectedShop != null)//нуждается в обработке 
                SelectedShop.ShopModels.PhotoPathShop2 = ImageConverterToArray(path);
            Image2 = ImageConverterToArray(path);
        }
        ));
        public AnnonsLoader AnnonsLoader { get; set; }
        private Command _saveToBd;
        public Command SaveToBd => _saveToBd ?? (_saveToBd = new Command(obj =>
        {
            ShopModels newshop = obj as ShopModels;
            ShopModels shopforcategry = new ShopModels();
            CategoryModels categories = new CategoryModels();
            if (SelectedShop != null)
            {
                using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                {
                    newshop = bd.ShopModels.Find(SelectedShop.Id);
                    shopforcategry = bd.ShopModels.FirstOrDefault(shop => ((shop.CategoryModelId != null) && (newshop.NumberPavilion == shop.NumberPavilion)));
                    newshop.NameShop = NameShop;
                    newshop.ShopDescription = ShopDescription;
                    newshop.SiteShop = SiteShop;
                    newshop.NumberShop = NumberShop;
                    newshop.PhotoPathShop = Image1;
                    newshop.PhotoPathShop2 = Image2;
                    if (shopforcategry != null)
                    {
                        shopforcategry.NameShop = NameShop;
                        shopforcategry.ShopDescription = ShopDescription;
                        shopforcategry.SiteShop = SiteShop;
                        shopforcategry.NumberShop = NumberShop;
                        shopforcategry.PhotoPathShop = Image1;
                        shopforcategry.PhotoPathShop2 = Image2;
                    }

                    bd.Entry(newshop).State = EntityState.Modified;
                    bd.SaveChanges();
                }
            }
        }
        ));
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
