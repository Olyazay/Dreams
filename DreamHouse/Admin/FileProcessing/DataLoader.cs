using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Admin.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Admin.FileProcessing
{
    public class DataLoader
    {
        public static DataLoader _instance;
      //  public Counter count { get; set; }
        private ObservableCollection<FloorModels> FloorsList = new ObservableCollection<FloorModels>();
        private ObservableCollection<PavilionModels> PavilionList = new ObservableCollection<PavilionModels>();
        private static object syncLock = new object();
        ShopsCreater creator = ShopsCreater.GetLoadShopsCreater();
        public static DataLoader GetLoadShopsCreater()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataLoader();
                    }
                }
            }
            return _instance;
        }
        public async Task GetFromBdAsync()
        {
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var Floors = await bd.FloorModels.ToListAsync();
                var floor0 = await bd.PavilionModels.Where(pav => pav.FloorModelId == 1).ToListAsync();
                var floor1 = await bd.PavilionModels.Where(pav => pav.FloorModelId == 2).ToListAsync();
                var floor2 = await bd.PavilionModels.Where(pav => pav.FloorModelId == 3).ToListAsync();
                var floor3 = await bd.PavilionModels.Where(pav => pav.FloorModelId == 4).ToListAsync();
                var shops0 = await bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 0)).ToListAsync();
                var shops1 = await  bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 1)).ToListAsync();
                var shops2 = await bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 2)).ToListAsync();
                var shops3 = await bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 3)).ToListAsync();
                foreach (var pav in floor0)
                {
                    zerofloor.Add(pav);
                    foreach (var shop in shops0)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor1)
                {
                    firstfloor.Add(pav);
                    foreach (var shop in shops1)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor2)
                {
                    secondfloor.Add(pav);
                    foreach (var shop in shops2)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor3)
                {
                    thirdfloor.Add(pav);
                    foreach (var shop in shops3)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                FloorsList.Add(new FloorModels(0, zerofloor));
                FloorsList.Add(new FloorModels(1, firstfloor));
                FloorsList.Add(new FloorModels(2, secondfloor));
                FloorsList.Add(new FloorModels(3, thirdfloor));
            }
            Session.CurrentSession.IsUpdating = false;
        }
        public void GetFromBD()
        {
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var Floors = bd.FloorModels.ToList();
                var floor0 = bd.PavilionModels.Where(pav => pav.FloorModelId == 1).ToList();
                var floor1 = bd.PavilionModels.Where(pav => pav.FloorModelId == 2).ToList();
                var floor2 = bd.PavilionModels.Where(pav => pav.FloorModelId == 3).ToList();
                var floor3 = bd.PavilionModels.Where(pav => pav.FloorModelId == 4).ToList();
                var shops0 = bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 0)).ToList();
                var shops1 = bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 1)).ToList();
                var shops2 = bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 2)).ToList();
                var shops3 = bd.ShopModels.Where(shop => (shop.CategoryModelId == null) && (shop.FloorNumber == 3)).ToList();
                foreach (var pav in floor0)
                {
                    zerofloor.Add(pav);
                    foreach (var shop in shops0)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor1)
                {
                    firstfloor.Add(pav);
                    foreach (var shop in shops1)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor2)
                {
                    secondfloor.Add(pav);
                    foreach (var shop in shops2)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                foreach (var pav in floor3)
                {
                    thirdfloor.Add(pav);
                    foreach (var shop in shops3)
                    {
                        if (shop.NumberPavilion == pav.NumberPavilion)
                        {
                            pav.ShopModels = shop;
                        }
                    }
                }
                FloorsList.Add(new FloorModels(0, zerofloor));
                FloorsList.Add(new FloorModels(1, firstfloor));
                FloorsList.Add(new FloorModels(2, secondfloor));
                FloorsList.Add(new FloorModels(3, thirdfloor));
            }
        }
        public ObservableCollection<PavilionModels> zerofloor = new ObservableCollection<PavilionModels>();
        public ObservableCollection<PavilionModels> firstfloor = new ObservableCollection<PavilionModels>();
        public ObservableCollection<PavilionModels> secondfloor = new ObservableCollection<PavilionModels>();
        public ObservableCollection<PavilionModels> thirdfloor = new ObservableCollection<PavilionModels>();
        public ObservableCollection<PavilionModels> allfloor { get; set; }
        public DataLoader()
        {

            //   GetFromBD();
           // GetFromBdAsync(); 
            cr = ShopsCreater.GetLoadShopsCreater();
            foreach (var floors in FloorsList)
            {
                foreach (var x in floors.PavilionModels)
                {
                    x.FloorModels = floors;
                }
            }

        }
       
        public ObservableCollection<PavilionModels> GetPavilionsList()
        {
            foreach (var floor in FloorsList)
            {
                foreach (var pav in floor.PavilionModels)
                {
                    PavilionList.Add(pav);
                }
            }
            return PavilionList;
        }
        public ShopsCreater cr { get; set; }
        public ObservableCollection<FloorModels> GetFloors()
        {
            return FloorsList;
        }
    }
}
