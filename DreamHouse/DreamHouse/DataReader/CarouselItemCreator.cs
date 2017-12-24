using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamHouse.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Entity;

namespace DreamHouse.DataReader
{
    public class CarouselItemCreator:IDisposable
    {
        public ObservableCollection<CarouselItemModel> CarouselItemCollection { get; set; } 
        public AnnonsCreator ac { get; set; }
        public ShopsCreater cr { get; set; }
        public CarouselItemCreator()
        {
            ac = new AnnonsCreator();
            cr = new ShopsCreater(); 
            
        }
        public ObservableCollection<CarouselItemModel> GetCarouselItemCollection()
        {

            //  GetFromBd();

            //{
            //    new CarouselItemModel("Коллекция очков в DreamHouse", "Настоящий стиль видно сразу", "../Images/CarouselSamplePhotos/fashion_kids_storry_cover.jpg", "Экран Оптика"),
            //    new CarouselItemModel("Игристое по бразильской системе", "Отличный выбор шампанского", "../Images/CarouselSamplePhotos/fashion_plyazh_storry_cover.jpg", "ВИНОТЕКА 80/100"),
            //    new CarouselItemModel("На морской волне", "Морской стиль", "../Images/CarouselSamplePhotos/grebly_storry_cover.jpg", "Винни"),
            //    new CarouselItemModel("Тренируйтесь на большой воде, не выходя из дома", "Лучший способ развить все тело сразу", "../Images/CarouselSamplePhotos/vino_storry_cover.jpg", "Technogym"),
            //    new CarouselItemModel("Пляжный костюм", "Самый актуальный пляжный look", "../Images/CarouselSamplePhotos/fashion_plyazh_storry_cover.jpg", "Пляжный костюм"),
            //};
            //  

         //   getshop();
         //   getannoun();
            //  SerilisationAnnon(); 
            DesirilisationCategory();

            return CarouselItemCollection;
        }

        public ShopModel caruselshop { get; set; }
        public void getshop()
        {
            caruselshop = new ShopModel(); 
            foreach (var car in CarouselItemCollection)
            {
                foreach (var category in cr.GetShopAllCollection())
                {
                    //caruselshop = category.ShopModelCollection.FirstOrDefault(shop => shop.NameShop == car.NameObject);
                    if (caruselshop!=null)
                    {
                        if (car.NameObject ==category.NameShop )
                        {
                            car.Object = category;
                        }
 
                    }
                }
            }
        }
        public void SerilisationAnnon()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("CaruselBin.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, CarouselItemCollection);
            }
        }
        public void DesirilisationCategory()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("CaruselBin.dat", FileMode.OpenOrCreate))
            {
                CarouselItemCollection = (ObservableCollection<CarouselItemModel>)formatter.Deserialize(fs);
                   getshop();
                   getannoun();
            }
            //foreach (var carusel in CarouselItemCollection)
            //{
            //    using (MemoryStream ms = new MemoryStream(carusel.PhotoPath, 0, carusel.PhotoPath.Length))
            //    {
            //        BinaryReader br = new BinaryReader(ms);
            //        byte[] bytes = br.ReadBytes((Int32)ms.Length);
            //        Image img = Image.FromStream(ms);
            //        try
            //        {
            //            img.Save("../../Images/" + carusel.NameObject + ".jpg", ImageFormat.Jpeg);
            //            //carusel.PathPhoto = "../../Images/" + carusel.NameObject + ".jpg";
            //        }
            //        catch (Exception ex)
            //        {

            //            throw new NotImplementedException(ex.Message);
            //        }
            //    }
            //}
        }
        public AnnouncementsModel caruselannon { get; set; }
        public void getannoun()
        {
            //CarouselItemCollection = new ObservableCollection<CarouselItemModel>();
            caruselannon = new AnnouncementsModel();
            foreach (var car in CarouselItemCollection)
            {
                caruselannon = ac.GetAnnounsmentList().FirstOrDefault(an => an.Heading == car.NameObject);
                if (caruselannon!=null)
                {
                    car.Object = caruselannon; 
                }
            }
        }
        public void GetFromBd()
        {
            CarouselItemCollection = new ObservableCollection<CarouselItemModel>(); 
            using (DreamHousseBd bd = new DreamHousseBd())
            {
                var carusels = bd.CarouselItems.ToList();
                foreach (var car in carusels)
                {
                    CarouselItemCollection.Add(car); 
                }
            }
        }
        public async Task GetFromBdAsync()
        {
            CarouselItemCollection = new ObservableCollection<CarouselItemModel>();
            using (DreamHousseBd bd = new DreamHousseBd())
            {
                var carusels = await bd.CarouselItems.ToListAsync();
                foreach (var car in carusels)
                {
                    CarouselItemCollection.Add(car);
                }
            }
        }
        public void Dispose()
        {
            //if (CarouselItemCollection!=null)
            //{
            //    CarouselItemCollection.Clear();
            //    CarouselItemCollection = null;
            //}

        }
    }
}
