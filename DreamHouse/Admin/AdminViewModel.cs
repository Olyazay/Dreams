using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Admin.FileProcessing;
using System.Collections.ObjectModel;
using Admin.Models;
using Admin.Commands;
using Admin.Converters;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data.Entity;
using System.Net;
using System.Windows;

namespace Admin
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private Command _update;
        public Command Update => _update ?? (_update = new Command(obj =>
        {
            Counters upcount = new Counters();
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                upcount = bd.Counters.Find(2);
                upcount.CounterForUpdate++;
                bd.Entry(upcount).State = EntityState.Modified;
                bd.SaveChanges();
            }
        }
        ));
        private void CheckConnection()
        {

            Action<string> s = (sd) => { };
            Checker.CheckInternet(s);
        }
        DataLoader dl = DataLoader.GetLoadShopsCreater();
        ShopsCreater cr = ShopsCreater.GetLoadShopsCreater();
        public ObservableCollection<CategoryModels> CategoryCollection { get; set; }
        public CaruselLoader cl = new CaruselLoader();

        public ObservableCollection<FloorModels> PavilionCollection { get; set; }
        public ObservableCollection<CarouselItemModels> CaruselsCollections { get; set; }
        public int count = 0;
        public ObservableCollection<AnnouncementsModels> AnnonsmentCollection { get; set; }
        public async void GetDbAsync()
        {

            AnnonsLoader = new AnnonsLoader();

            await dl.GetFromBdAsync();
        }
        public async void GetDbAboutCategoryAsycn()
        {
            await cr.GetFromBdAsync();


        }
        public async void GetCaruselAsync()
        {
            await cl.GetFromBdAsync();
        }
        public async void GetDbAnnonsAsync()
        {
            await AnnonsLoader.GetFromBdAsync();

        }

        public AdminViewModel()
        {
            Action<string> s = (sd) => {
                Session.CurrentSession.IsUpdating = true;

                GetDbAsync();
                PavilionCollection = dl.GetFloors();
                //   AnnonsLoader = new AnnonsLoader();
                GetDbAnnonsAsync();
                AnnonsmentCollection = AnnonsLoader.GetAnnons();
                //  cl = new CaruselLoader();
                GetCaruselAsync();
                GetDbAboutCategoryAsycn();
                CategoryCollection = cr.GetShopList();
                CaruselsCollections = new ObservableCollection<CarouselItemModels>();
                CaruselsCollections = cl.getcar();
                FreeAre();


            };
            Checker.CheckInternet(s);
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
        #region Shop
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
        private String _placeShop;
        public String PlaceShop
        {
            get
            {
                return _placeShop;
            }
            set
            {
                _placeShop = value;
                OnPropertyChange("PlaceShop");
            }
        }
        private CategoryModels _selectedCategoryForShop;
        public CategoryModels SelectedCategoryForShop
        {
            get
            {
                return _selectedCategoryForShop;
            }
            set
            {
                _selectedCategoryForShop = value;
                OnPropertyChange("SelectedCategoryForShop");
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
                Image1 = null;
                Image2 = null;
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
            if (string.IsNullOrWhiteSpace(path) == false)
            {
                Bitmap image = new Bitmap(path);
                ImageFormat format = image.RawFormat;
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    return ms.ToArray();
                }
            }
            return null;

        }
        public void GetDataAboutShop()
        {
            if (SelectedShop != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        //ShopModels huo = new ShopModels()
                        //{
                        //    NameShop = "Ludovig",
                        //    NumberPavilion = "n256"
                        //};
                        //FloorModels ds = bd.FloorModels.Find(3);
                        //bd.ShopModels.Add(huo);
                        //PavilionModels model = new PavilionModels()
                        //{
                        //    Id = huo.Id,
                        //    NumberPavilion = "n256",
                        //    FloorModelId = 2,
                        //    FloorModels = ds,
                        //    ShopModels = huo

                        //};
                        //bd.PavilionModels.Add(model);
                        //bd.SaveChanges();
                        //List<PavilionModels> listdelete = bd.PavilionModels.Where(ty => ty.Id != ty.ShopModels.Id).ToList();
                        //foreach (var r in listdelete)
                        //{
                        //    bd.PavilionModels.Remove(r);
                        //    bd.SaveChanges(); 
                        //}
                        NameShop = SelectedShop.ShopModels.NameShop;
                        ShopDescription = SelectedShop.ShopModels.ShopDescription;
                        SiteShop = SelectedShop.ShopModels.SiteShop;
                        NumberShop = SelectedShop.ShopModels.NumberShop;
                        PlaceShop = SelectedShop.ShopModels.Place;
                        SelectedCategoryForShop = SelectedShop.ShopModels.CategoryModels;

                        if (SelectedCategoryForShop != null)
                        {
                            if (SelectedCategoryForShop.NameCategory == SelectedShop.ShopModels.NameCategory)
                            {
                                CategoryModels a = new CategoryModels();
                                a = CategoryCollection.FirstOrDefault(x => x.NameCategory == SelectedShop.ShopModels.NameCategory);
                                SelectedCategoryForShop = a;
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте подключение к интернету!");
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
            if (path != null && path != "" && SelectedShop != null)
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
            if (path != null && path != "" && SelectedShop != null)
            {
                SelectedShop.ShopModels.PhotoPathShop2 = ImageConverterToArray(path);
                Image2 = ImageConverterToArray(path);
            }

        }
        ));
        #endregion
        #region annons
        public AnnonsLoader AnnonsLoader { get; set; }
        private Command _deleteToBd;
        public Command DeleteShop => _deleteToBd ?? (_deleteToBd = new Command(obj =>
         {

             using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
             {
                 PavilionModels pavilion = bd.PavilionModels.Find(SelectedShop.Id);
                 ShopModels shopModels = bd.ShopModels.Find(pavilion.Id);
                 shopModels.NameShop = null;
                 shopModels.NumberShop = null;
                 shopModels.ShopDescription = null; 
                 shopModels.SiteShop = null;
                 shopModels.Place = null;
                 //   shopModels.NumberPavilion = pavilion.NumberPavilion;
                 //     shopModels.FloorNumber = pavilion.FloorModelId.Value;
                 shopModels.PhotoPathShop = null;
                 shopModels.PhotoPathShop2 = null; ;
                 shopModels.NameCategory = null; 
                 pavilion.ShopModels = new ShopModels();

                 pavilion.ShopModels = shopModels;
                 try
                 {
                     bd.Entry(pavilion).State = EntityState.Modified;
                     bd.SaveChanges();
                 }
                 catch (Exception)
                 {

                     MessageBox.Show("Проверьте соединение с интернетом!");
                 }
             }
         }
        ));

        private Command _saveToBd;
        public Command SaveToBd => _saveToBd ?? (_saveToBd = new Command(obj =>
        {

        using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
        {
                    PavilionModels pavilion = bd.PavilionModels.Find(SelectedShop.Id);
                    ShopModels shopModels = bd.ShopModels.Find(SelectedShop.ShopModels.Id);
                    shopModels.NameShop = NameShop;
                    shopModels.NumberShop = NumberShop;
                    shopModels.ShopDescription = ShopDescription;
                    shopModels.SiteShop = SiteShop;
                    shopModels.Place = PlaceShop;
             //  shopModels.NumberPavilion = pavilion.NumberPavilion;
              shopModels.FloorNumber = pavilion.FloorModelId.Value;
                    shopModels.PhotoPathShop = Image1 != null ? Image1 : SelectedShop.ShopModels.PhotoPathShop;
                    shopModels.PhotoPathShop2 = Image2 != null ? Image2 : SelectedShop.ShopModels.PhotoPathShop2;
                    try
                    {
                        shopModels.NameCategory = SelectedCategoryForShop.NameCategory;

                }
                    catch (Exception)
                    {
                        MessageBox.Show("Введите название категории!"); 
                    }

 
                pavilion.ShopModels = shopModels;

                    bd.Entry(pavilion).State = EntityState.Modified;
                    bd.SaveChanges();

            }
            //    ShopModels newshop = obj as ShopModels;
            //    ShopModels shopforcategry = new ShopModels();
            //    if (SelectedShop != null)
            //    {
            //        using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            //        {
            //            newshop = bd.ShopModels.Find(SelectedShop.Id);
            //            shopforcategry = bd.ShopModels.FirstOrDefault(shop => ((shop.CategoryModelId != null) && (newshop.NumberPavilion == shop.NumberPavilion)));
            //            newshop.NameShop = NameShop;
            //            newshop.ShopDescription = ShopDescription;
            //            newshop.SiteShop = SiteShop;
            //            newshop.NumberShop = NumberShop;
            //            newshop.PhotoPathShop = Image1 != null ? Image1 : SelectedShop.ShopModels.PhotoPathShop;
            //            newshop.PhotoPathShop2 = Image2 != null ? Image2 : SelectedShop.ShopModels.PhotoPathShop2;
            //            GetShopAdd();
            //            if (shopforcategry != null)
            //            {
            //                shopforcategry.NameShop = NameShop;
            //                shopforcategry.ShopDescription = ShopDescription;
            //                shopforcategry.SiteShop = SiteShop;
            //                shopforcategry.NumberShop = NumberShop;
            //                shopforcategry.PhotoPathShop = Image1 != null ? Image1 : shopforcategry.PhotoPathShop;
            //                shopforcategry.PhotoPathShop2 = Image2 != null ? Image2 : shopforcategry.PhotoPathShop2;
            //                try
            //                {
            //                    shopforcategry.NameCategory = SelectedCategoryForShop.NameCategory;
            //                }
            //                catch (Exception)
            //                {

            //                    MessageBox.Show("Поставьте категорию");
            //                }

            //            }
            //            bd.Entry(newshop).State = EntityState.Modified;
            //            bd.SaveChanges();
            //            count++;
            //        }
            //    }
            //}

            //private Command _saveToBd;
            //public Command SaveToBd => _saveToBd ?? (_saveToBd = new Command(obj =>
            //{
            //    ShopModels newshop = obj as ShopModels;
            //    ShopModels shopforcategry = new ShopModels();
            //    ShopModels doubleshop = new ShopModels(); 
            //    if (SelectedShop != null)
            //    {
            //        using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            //        {
            //            newshop = bd.ShopModels.Find(SelectedShop.Id);
            //            shopforcategry = bd.ShopModels.FirstOrDefault(shop => ((shop.CategoryModelId != null) && (newshop.NumberPavilion == shop.NumberPavilion)));
            //            newshop.NameShop = NameShop;
            //            newshop.ShopDescription = ShopDescription;
            //            newshop.SiteShop = SiteShop;
            //            newshop.NumberShop = NumberShop;
            //            newshop.PhotoPathShop = Image1 != null ? Image1 : SelectedShop.ShopModels.PhotoPathShop;
            //            newshop.PhotoPathShop2 = Image2 != null ? Image2 : SelectedShop.ShopModels.PhotoPathShop2;
            //            GetShopAdd(); 
            //            if (shopforcategry != null)
            //            {
            //                shopforcategry.NameShop = NameShop;
            //                shopforcategry.ShopDescription = ShopDescription;
            //                shopforcategry.SiteShop = SiteShop;
            //                shopforcategry.NumberShop = NumberShop;
            //                shopforcategry.PhotoPathShop = Image1 != null ? Image1 : shopforcategry.PhotoPathShop;
            //                shopforcategry.PhotoPathShop2 = Image2 != null ? Image2 : shopforcategry.PhotoPathShop2;
            //                shopforcategry.NameCategory = SelectedCategoryForShop.NameCategory;
            //            }
            //            doubleshop = bd.ShopModels.FirstOrDefault(ds => ds.NameShop == NameShop); 
            //                bd.Entry(newshop).State = EntityState.Modified;
            //                bd.SaveChanges();

            //        }
            //    }
        }
        ));
        public void GetShopModifire()
        {
        }
        #endregion
        #region category 
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
        private string _BackgroundCategory;
        public string BackgroundCategory
        {
            get
            {
                return _BackgroundCategory;
            }
            set
            {
                _BackgroundCategory = value;
                OnPropertyChange("BackgroundCategory");
            }
        }
        private byte[] _pathCategoryPhoto;
        public byte[] PathCategoryPhoto
        {
            get
            {
                return _pathCategoryPhoto;
            }
            set
            {
                _pathCategoryPhoto = value;
                OnPropertyChange("PathCategoryPhoto");
            }
        }
        private CategoryModels _selectedCategory;
        public CategoryModels SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChange("SelectedCategory");
                GetDataAboutCategory();
            }
        }
        public void GetDataAboutCategory()
        {
            if (SelectedCategory != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        CategoryModels category = bd.CategoryModels.Find(SelectedCategory.Id);
                        NameCategory = SelectedCategory.NameCategory;
                        PathCategoryPhoto = SelectedCategory.PathCategoryPhoto;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Проверьте подключение к интернету"); 
                }

            }
        }
        private Command _saveOpenedCategoryImage;
        public Command SaveOpenedCategoryImage => _saveOpenedCategoryImage ?? (_saveOpenedCategoryImage = new Command(obj =>
        {
            CategoryModels newpavilion = obj as CategoryModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (path != null && path != "" && SelectedCategory != null)
                SelectedCategory.PathCategoryPhoto = ImageConverterToArray(path);
            PathCategoryPhoto = ImageConverterToArray(path);
        }
        ));
        private Command _saveToBdAboutCategory;
        public Command SaveToBdAboutCategory => _saveToBdAboutCategory ?? (_saveToBdAboutCategory = new Command(obj =>
        {
            CategoryModels newcategory = obj as CategoryModels;
            if (SelectedCategory != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        newcategory = bd.CategoryModels.Find(SelectedCategory.Id);
                        //newcategory.NameCategory = NameCategory;
                        newcategory.PathCategoryPhoto = PathCategoryPhoto != null ? PathCategoryPhoto : newcategory.PathCategoryPhoto;
                        bd.Entry(newcategory).State = EntityState.Modified;
                        bd.SaveChanges();
                        MessageBox.Show("Фотография обновлена!");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Данные не сохранились, проверьте подключение к интернету!"); 
                }

            }
        }
        ));
        #endregion
        #region annons
        private string _HeadingAnnons;
        public string HeadingAnnons
        {
            get { return _HeadingAnnons; }
            set
            {
                _HeadingAnnons = value;
                OnPropertyChange("HeadingAnnons");
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

        private byte[] _PhotoPathAnnons;
        public byte[] PhotoPathAnnons
        {
            get { return _PhotoPathAnnons; }
            set
            {
                _PhotoPathAnnons = value;
                OnPropertyChange("PhotoPathAnnons");
            }
        }

        private byte[] _PreviewPhotoPathAnnons;
        public byte[] PreviewPhotoPathAnnons
        {
            get { return _PreviewPhotoPathAnnons; }
            set
            {
                _PreviewPhotoPathAnnons = value;
                OnPropertyChange("PreviewPhotoPathAnnons");
            }
        }
        private AnnouncementsModels _selectedAnnons;
        public AnnouncementsModels SelectedAnnons
        {
            get
            {
                return _selectedAnnons;
            }
            set
            {
                _selectedAnnons = value;
                OnPropertyChange("SelectedAnnons");
                PhotoPathAnnons = null;
                PreviewPhotoPathAnnons = null;
                GetDataAboutAnnons();
                
            }
        }
        public void GetDataAboutAnnons()
        {
            if (SelectedAnnons != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        AnnouncementsModels category = bd.AnnouncementsModels.Find(SelectedAnnons.id);
                        HeadingAnnons = SelectedAnnons.Heading;
                        Body = SelectedAnnons.Body;
                        PreviewPhotoPathAnnons = SelectedAnnons.PreviewPhotoPath;
                        PhotoPathAnnons = SelectedAnnons.PhotoPath;
                        // PathCategoryPhoto = SelectedCategory.PathCategoryPhoto; 
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось получить доступ к серверу!");
                }

            }
        }
        private Command _saveOpenedAnnonsMain;
        public Command SaveOpenedAnnonsMain => _saveOpenedAnnonsMain ?? (_saveOpenedAnnonsMain = new Command(obj =>
        {
            AnnouncementsModels newannons = obj as AnnouncementsModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (path != null && path != "" && SelectedAnnons != null)
            {//нуждается в обработке 
                SelectedAnnons.PhotoPath = ImageConverterToArray(path);
                PhotoPathAnnons = ImageConverterToArray(path);
            }
        }
        ));

        private Command _saveOpenedAnnonsMainPre;
        public Command SaveOpenedAnnonsMainPre => _saveOpenedAnnonsMainPre ?? (_saveOpenedAnnonsMainPre = new Command(obj =>
        {
            AnnouncementsModels newannons = obj as AnnouncementsModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (path != null && path != "" && SelectedAnnons != null)//нуждается в обработке 
                SelectedAnnons.PreviewPhotoPath = ImageConverterToArray(path);
            PreviewPhotoPathAnnons = ImageConverterToArray(path);
        }
        ));
        private Command _saveAnnonsToBd;
        public Command SaveAnnonsToBd => _saveAnnonsToBd ?? (_saveAnnonsToBd = new Command(obj =>
        {
            AnnouncementsModels newannons = obj as AnnouncementsModels;
            if (SelectedAnnons != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        newannons = bd.AnnouncementsModels.Find(SelectedAnnons.id);
                        newannons.Heading = HeadingAnnons;
                        newannons.Body = Body;
                        newannons.PhotoPath = PhotoPathAnnons != null ? PhotoPathAnnons : newannons.PhotoPath;
                        newannons.PreviewPhotoPath = PreviewPhotoPathAnnons != null ? PreviewPhotoPathAnnons : newannons.PreviewPhotoPath;
                        bd.Entry(newannons).State = EntityState.Modified;
                        bd.SaveChanges();
                        MessageBox.Show("Данные сохранены!"); 
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Данные не сохранены, проверьте соединение с сервером!"); 
                }

            }
        }
        ));
        private Command _saveDeleteAnnonsMain;
        public Command SaveDeleteAnnonsMain => _saveDeleteAnnonsMain ?? (_saveDeleteAnnonsMain = new Command(obj =>
        {
            if (SelectedAnnons!=null)
            {
                try
                {
                    AnnouncementsModels anonns = obj as AnnouncementsModels;
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        anonns = bd.AnnouncementsModels.Find(SelectedAnnons.id);
                        if (anonns != null)
                        {
                            bd.AnnouncementsModels.Remove(anonns);
                            AnnonsmentCollection.Remove(anonns);
                            bd.SaveChanges();
                            MessageBox.Show("Удалено!"); 
                        }

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Проверьте соединение с сервером!"); 

                }

            }
        }
        ));
        private Command _saveAddAnnonsMain;
        public Command SaveAddAnnonsMain => _saveAddAnnonsMain ?? (_saveAddAnnonsMain = new Command(obj =>
        {
            if (SelectedAnnons == null)
            {
                try
                {
                    AnnouncementsModels anonns = new AnnouncementsModels();
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        anonns.Body = Body;
                        anonns.Heading = HeadingAnnons;
                        anonns.PhotoPath = PhotoPathAnnons;
                        anonns.PreviewPhotoPath = PreviewPhotoPathAnnons;
                        bd.AnnouncementsModels.Add(anonns);
                        AnnonsmentCollection.Add(anonns);
                        bd.SaveChanges();
                        MessageBox.Show("Данные сохранены!");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Проверьте соединение с сервером"); 
                    }

            }
        }
        ));

        #endregion
        #region carosel
        private string _HeadingCar;
        public string HeadingCar
        {
            get { return _HeadingCar; }
            set
            {
                _HeadingCar = value;
                OnPropertyChange("HeadingCar");
            }
        }

        private string _DescriptionCar;
        public string DescriptionCar
        {
            get { return _DescriptionCar; }
            set
            {
                _DescriptionCar = value;
                OnPropertyChange("DescriptionCar");
            }
        }

        private byte[] _PhotoPathCar;
        public byte[] PhotoPathCar
        {
            get { return _PhotoPathCar; }
            set
            {
                _PhotoPathCar = value;
                OnPropertyChange("PhotoPathCar");
            }
        }
        private string _nameObjectCar;
        public string NameObjectCar
        {
            get { return _nameObjectCar; }
            set
            {
                _nameObjectCar = value;
                OnPropertyChange("NameObjectCar");
            }
        }
        private CarouselItemModels _selectedCar;
        public CarouselItemModels SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChange("SelectedCar");
                PhotoPathCar = null; 
                GetDataAboutCarusel();
            }
        }
        public void GetDataAboutCarusel()
        {
            if (SelectedCar != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        CarouselItemModels category = bd.CarouselItemModels.Find(SelectedCar.id);
                        HeadingCar = SelectedCar.Heading;
                        DescriptionCar = SelectedCar.Description;
                        NameObjectCar = SelectedCar.NameObject;
                        PhotoPathCar = SelectedCar.PhotoPath;                       
                        // PathCategoryPhoto = SelectedCategory.PathCategoryPhoto; 
                        
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Проверьте подключение к интернету"); 
                }

            }
        }
        private Command _saveOpenedImageCarusel;
        public Command SaveOpenedImageCarusel => _saveOpenedImageCarusel ?? (_saveOpenedImageCarusel = new Command(obj =>
        {
            CarouselItemModels newannons = obj as CarouselItemModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
            if (path != null && path != "" && SelectedCar != null)//нуждается в обработке 
                SelectedCar.PhotoPath = ImageConverterToArray(path);
            PhotoPathCar = ImageConverterToArray(path);
        }
        ));
        private Command _saveToBdCarusel;
        public Command SaveToBdCarusel => _saveToBdCarusel ?? (_saveToBdCarusel = new Command(obj =>
        {
            CarouselItemModels newcar = obj as CarouselItemModels;
            if (SelectedCar != null)
            {
                try
                {
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        newcar = bd.CarouselItemModels.Find(SelectedCar.id);
                        newcar.Heading = HeadingCar;
                        newcar.Description = DescriptionCar;
                        newcar.NameObject = NameObjectCar;
                        newcar.PhotoPath = PhotoPathCar;
                        bd.Entry(newcar).State = EntityState.Modified;
                        bd.SaveChanges();
                        MessageBox.Show("Данные сохранены"); 
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Данные не сохранены!"); 
                }

            }
        }
        ));
        private Command _saveDeleteCarusel;
        public Command SaveDeleteCarusel => _saveDeleteCarusel ?? (_saveDeleteCarusel = new Command(obj =>
        {
            if (SelectedCar != null)
            {
                try
                {
                    CarouselItemModels car = new CarouselItemModels();
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        car = bd.CarouselItemModels.Find(SelectedCar.id);
                        bd.CarouselItemModels.Remove(car);
                        CaruselsCollections.Remove(car);
                        bd.SaveChanges();
                        MessageBox.Show("Данные удалены!"); 
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Проверьте соединение с сервером!"); 
                }

            }
        }
        ));
        private Command _saveAddCarusel;
        public Command SaveAddCarusel => _saveAddCarusel ?? (_saveAddCarusel = new Command(obj =>
        {
            if (SelectedCar== null)
            {
                try
                {
                    CarouselItemModels car = new CarouselItemModels();
                    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                    {
                        car.Heading = HeadingCar;
                        car.NameObject = NameObjectCar;
                        car.PhotoPath = PhotoPathCar;
                        car.Description = DescriptionCar;
                        bd.CarouselItemModels.Add(car);
                        CaruselsCollections.Add(car);
                        bd.SaveChanges();
                        MessageBox.Show("Данные добавлены!"); 
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Проверьте соединение с сервером!"); 
                }

            }
        }
        ));
        #endregion
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
