using DreamHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DreamHouse
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            Session.CurrentSession.IsUpdating = false;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0,2,0);
            dispatcherTimer.Start();
          //  GetUpdate();
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public DispatcherTimer dispatcherTimer { get; set; }
        public int AppCount = 0;
        private void Timer_Tick(object sender, EventArgs e)
       {
            Action<string> s = (sd) =>
            {
               
                GetUpdate();
            };
            Checker.CheckInternet(s);
            //try{
            //    GetUpdate();
            //}
            //catch
            //{
            //    MessageBox.Show("Ольга лесбуха");
            //}

        }
        //public static async Task<bool> CheckConnection()
        //{
        //    bool isExist = false;
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://yandex.ru");
        //        request.Timeout = 5000;
        //        request.Credentials = CredentialCache.DefaultNetworkCredentials;
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //        if (response.StatusCode == HttpStatusCode.OK)
        //            isExist = true;
        //        else
        //            isExist = false;
        //    }
        //    catch
        //    {
        //        isExist = false;
        //    }
        //    return isExist;
        //}
        public async void GetUpdate()
        {
            //FloorsList = null;
            //zerofloor = null;
            //firstfloor = null;
            //secondfloor = null;
            //thirdfloor = null; 

            try
            {
                using (DreamHousseBd bd = new DreamHousseBd())
                {
                    try
                    {
                        Session.CurrentSession.IsUpdating = false;
                        var count = await bd.Counts.FindAsync(2);
                        if (AppCount != count.CounterForUpdate)
                        {
                            Session.CurrentSession.IsUpdating = true;
                            AppCount = count.CounterForUpdate;
                            await GetAnnonsFromBdAsync();
                            await GetCaruselFromBdAsync();
                            await GetPavilinFromBdAsync();
                            await GetFromBdAsync();
                            (App.Current.MainWindow as MainWindow).MapPageInstance = new Pages.MapPage();
                            (App.Current.MainWindow as MainWindow).MainPageInstance = new Pages.MainPage();
                            (App.Current.MainWindow as MainWindow).SearchPageInstance = new Pages.SearchPage();
                            (App.Current.MainWindow as MainWindow).PageContent.Content = (App.Current.MainWindow as MainWindow).MainPageInstance;
                            Session.CurrentSession.IsUpdating = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Session.CurrentSession.IsUpdating = false;
                        Debug.WriteLine(ex.Message);
                    }

                }
            }
            catch
            {
                Session.CurrentSession.IsUpdating = false;
            }
        }
        public async Task GetAnnonsFromBdAsync()
        {
            ObservableCollection<AnnouncementsModel> AnnouncementsList = new ObservableCollection<AnnouncementsModel>();
            try
            {
                using (DreamHousseBd bd = new DreamHousseBd())
                {
                    var annons = await bd.Announs.ToListAsync();
                    foreach (var anon in annons)
                    {
                        AnnouncementsList.Add(anon);
                    }
                }
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("AnnonBin.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, AnnouncementsList);
                }
            }
            catch
            {
                Session.CurrentSession.IsUpdating = false;
            }

        }
        public async Task GetCaruselFromBdAsync()
        {
            ObservableCollection<CarouselItemModel> CarouselItemCollection = new ObservableCollection<CarouselItemModel>();
            try
            {
            using (DreamHousseBd bd = new DreamHousseBd())
            {
                var carusels = await bd.CarouselItems.ToListAsync();
                foreach (var car in carusels)
                {
                    CarouselItemCollection.Add(car);
                }
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("CaruselBin.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, CarouselItemCollection);
            }
            }
            catch 
            {
                Session.CurrentSession.IsUpdating = false;
            }

        }
        //public ObservableCollection<PavilionModel> zerofloor = new ObservableCollection<PavilionModel>();
        //public ObservableCollection<PavilionModel> firstfloor = new ObservableCollection<PavilionModel>();
        //public ObservableCollection<PavilionModel> secondfloor = new ObservableCollection<PavilionModel>();
        //public ObservableCollection<PavilionModel> thirdfloor = new ObservableCollection<PavilionModel>();
        //private ObservableCollection<FloorModel> FloorsList = new ObservableCollection<FloorModel>();
        public async Task GetPavilinFromBdAsync()
        {
           ObservableCollection<PavilionModel> zerofloor = new ObservableCollection<PavilionModel>();
        ObservableCollection<PavilionModel> firstfloor = new ObservableCollection<PavilionModel>();
        ObservableCollection<PavilionModel> secondfloor = new ObservableCollection<PavilionModel>();
         ObservableCollection<PavilionModel> thirdfloor = new ObservableCollection<PavilionModel>();
        ObservableCollection<FloorModel> FloorsList = new ObservableCollection<FloorModel>();
            try
            {
                using (DreamHousseBd bd = new DreamHousseBd())
                {
                                        var Floors = await bd.Floors.ToListAsync();
                    var floor0 = await bd.Pavilions.Where(pav => pav.FloorModelId == 1).ToListAsync();
                    var floor1 = await bd.Pavilions.Where(pav => pav.FloorModelId == 2).ToListAsync();
                    var floor2 = await bd.Pavilions.Where(pav => pav.FloorModelId == 3).ToListAsync();
                    var floor3 = await bd.Pavilions.Where(pav => pav.FloorModelId == 4).ToListAsync();
                    var shops0 = await bd.Shops.Where(shop => (shop.FloorNumber == 0)).ToListAsync();
                    var shops1 = await bd.Shops.Where(shop =>  (shop.FloorNumber == 1)).ToListAsync();
                    var shops2 = await bd.Shops.Where(shop =>  (shop.FloorNumber == 2)).ToListAsync();
                    var shops3 = await bd.Shops.Where(shop => (shop.FloorNumber == 3)).ToListAsync();

                   foreach (var pav in floor0)
                    {
                        zerofloor.Add(pav);
                        //foreach (var shop in shops0)
                        //{
                        //    if (shop.NumberPavilion == pav.NumberPavilion)
                        //    {
                        //        pav.Shop = shop;
                        //    }
                        //}
                    }
                    foreach (var pav in floor1)
                    {
                        firstfloor.Add(pav);
                        //foreach (var shop in shops1)
                        //{
                        //    if (shop.NumberPavilion == pav.NumberPavilion)
                        //    {
                        //        pav.Shop = shop;
                        //    }
                        //}
                    }
                    foreach (var pav in floor2)
                    {
                        secondfloor.Add(pav);
                        //foreach (var shop in shops2)
                        //{
                        //    if (shop.NumberPavilion == pav.NumberPavilion)
                        //    {
                        //        pav.Shop = shop;
                        //    }
                        //}
                    }
                    foreach (var pav in floor3)
                    {
                        thirdfloor.Add(pav);
                        //foreach (var shop in shops3)
                        //{
                        //    if (shop.NumberPavilion == pav.NumberPavilion)
                        //    {
                        //        pav.Shop = shop;
                        //    }
                        //}
                    }
                    FloorsList.Add(new FloorModel(0, zerofloor));
                    FloorsList.Add(new FloorModel(1, firstfloor));
                    FloorsList.Add(new FloorModel(2, secondfloor));
                    FloorsList.Add(new FloorModel(3, thirdfloor));
                }
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("FloorwithPavBin.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, FloorsList);

                }
            }
            catch 
            {

                Session.CurrentSession.IsUpdating = false;

            }
           
        }
        private ObservableCollection<ShopModel> AllShopsList = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> moda = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> beauty = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> child = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> home = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> food = new ObservableCollection<ShopModel>();
        public ObservableCollection<ShopModel> service = new ObservableCollection<ShopModel>();
        public async Task GetFromBdAsync()
        {
                    ObservableCollection<CategoryModel> catyg = new ObservableCollection<CategoryModel>();

            try
            {
                using (DreamHousseBd bd = new DreamHousseBd())
                {
                    AllShopsList = new ObservableCollection<ShopModel>();

                    var shops = await bd.Shops.Where(t=>t.NameCategory!=null).ToListAsync();
                    foreach (var shop in shops)
                    {
                        char c;
                        c = shop.NumberPavilion[1];
                        if (shop!=null)
                        {
                            shop.FloorNumber = int.Parse(c.ToString());
                            AllShopsList.Add(shop);
                            if (shop.NameCategory.Contains("МОДА&СТИЛЬ"))
                            {
                                moda.Add(shop);
                            }
                            if (shop.NameCategory == "КРАСОТА&ЗДОРОВЬЕ")
                            {
                                beauty.Add(shop);
                            }
                            if (shop.NameCategory == "ДЕТИ")
                            {
                                child.Add(shop);
                            }
                            if (shop.NameCategory == "ДОМ&ХОББИ")
                            {
                                home.Add(shop);
                            }
                            if (shop.NameCategory == "ЕДА")
                            {
                                food.Add(shop);
                            }
                            if (shop.NameCategory == "УСЛУГИ")
                            {
                                service.Add(shop);
                            }
                        }
                      
                    }

                    var caty = await bd.Categories.ToListAsync();
                    foreach (var car in caty)
                    {
                        catyg.Add(car);
                    }
                }
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("CategoryPhoto.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, catyg);
                }
                using (FileStream fs = new FileStream("ShopsBin.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, AllShopsList);
                }
            }
            catch 
            {

                Session.CurrentSession.IsUpdating = false;
            }
           
        }
    }
}
