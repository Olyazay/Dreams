using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Models;
using System.Windows.Media;
using System.Data.Entity;

namespace Admin.FileProcessing
{
    public class ShopsCreater
    {
        public DataLoader pavilionCreator { get; set; }
        private static ShopsCreater _instance;
        private ObservableCollection<CategoryModels> ShopList = new ObservableCollection<CategoryModels>();
        private ObservableCollection<ShopModels> AllShopsList = new ObservableCollection<ShopModels>();
        public ObservableCollection<ShopModels> restarauntrelax { get; set; }
        public ObservableCollection<ShopModels> beauty { get; set; }
        public ObservableCollection<ShopModels> home { get; set; }
        public ObservableCollection<ShopModels> food { get; set; }
        public ObservableCollection<ShopModels> clothes { get; set; }
        public ObservableCollection<ShopModels> kids { get; set; }
        public ObservableCollection<ShopModels> medicine { get; set; }
        public ObservableCollection<ShopModels> jewerely { get; set; }
        public ObservableCollection<ShopModels> services { get; set; }
        public ObservableCollection<ShopModels> presents { get; set; }
        public ObservableCollection<ShopModels> shopsbd { get; set; }

        private static object syncLock = new object();
        public async Task GetFromBdAsync()
        {
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var categories = await bd.CategoryModels.ToListAsync();
                foreach (var category in categories)
                {
                    if (category.Id<7)
                    {
                        ShopList.Add(category);

                    }
                }
                AllShopsList = new ObservableCollection<ShopModels>();
                var shops = await bd.ShopModels.Where(shop => shop.CategoryModelId != null).ToListAsync();
                foreach (var shop in shops)
                {
                    char c;
                    c = shop.NumberPavilion[1];
                    shop.FloorNumber = int.Parse(c.ToString());
                    AllShopsList.Add(shop);
                }
            }
        }
        public void GetFromBd()
        {
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var categories = bd.CategoryModels.ToList();
                foreach (var category in categories)
                {
                    if (category.Id<6)
                    {
                        ShopList.Add(category);
                    }

                }
                AllShopsList = new ObservableCollection<ShopModels>();
                var shops = bd.ShopModels.Where(shop => shop.CategoryModelId != null).ToList();
                foreach (var shop in shops)
                {
                    char c;
                    c = shop.NumberPavilion[1];
                    shop.FloorNumber = int.Parse(c.ToString());
                    AllShopsList.Add(shop);
                }
            }
        }
        protected ShopsCreater()
        {
            foreach (var x in ShopList)
            {
                foreach (var y in x.ShopModels)
                {
                    char c;
                    c = y.NumberPavilion[1];
                    y.FloorNumber = int.Parse(c.ToString());
                }
            }
            //        GetFromBd();
          //  GetFromBdAsync(); 
        }
        public static ShopsCreater GetLoadShopsCreater()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ShopsCreater();
                    }
                }
            }
            return _instance;
        }
        public ObservableCollection<CategoryModels> GetShopList()
        {
            return ShopList;
        }
    }
}
