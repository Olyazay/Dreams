using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.FileProcessing
{
    public class CaruselLoader
    {
        public ObservableCollection<CarouselItemModels> CarouselItemCollection { get; set; }
        //public void GetFromBd()
        //{
        //    CarouselItemCollection = new ObservableCollection<CarouselItemModels>();
        //    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
        //    {
        //        var carusels = bd.CarouselItemModels.ToList();
        //        foreach (var car in carusels)
        //        {
        //            CarouselItemCollection.Add(car);
        //        }
        //    }
        //}
        public ObservableCollection<CarouselItemModels> getcar()
        {
            return CarouselItemCollection; 
        }
        public async Task GetFromBdAsync()
        {
            CarouselItemCollection = new ObservableCollection<CarouselItemModels>();
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var carusels = await bd.CarouselItemModels.ToListAsync();
                foreach (var car in carusels)
                {
                    CarouselItemCollection.Add(car);
                }
            }
        }
        //public async Task<ObservableCollection<CarouselItemModels>> GetAnnounsmentList()
        //{
        //    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
        //    {
        //           var carusels = await bd.CarouselItemModels.ToListAsync();
        //           return new ObservableCollection<CarouselItemModels>(carusels); 
        //    }
        //}
        public CaruselLoader()
        {
            //GetFromBd(); 
        }
    }
}
