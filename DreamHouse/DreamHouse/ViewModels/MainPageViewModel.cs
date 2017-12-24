using DreamHouse.DataReader;
using DreamHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DreamHouse.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public AnnonsCreator ac = new AnnonsCreator(); 
      private ObservableCollection<CategoryModel> AllCategories = new ObservableCollection<CategoryModel>();

        public ObservableCollection<CategoryModel> ShownCategories { get; set; }

        public ObservableCollection<AnnouncementsModel> Announcements { get; set; }

        public ObservableCollection<CarouselItemModel> CarouselItems { get; set; }

       public ObservableCollection<ShopModel> shops { get; set; }
     public ShopsCreater shopsCreater { get; set; }
        public CarouselItemCreator cc = new CarouselItemCreator(); 
        public MainPageViewModel()
        {
     //       ac = new AnnonsCreator();
            Announcements = new ObservableCollection<AnnouncementsModel>();
            shopsCreater = new ShopsCreater(); 
          AllCategories = shopsCreater.GetShopList();
          GetShownCategories();
            Announcements = ac.GetAnnounsmentList();
            //ObservableCollection<ShopModel> allShops = new ObservableCollection<ShopModel>();
            //foreach (var x in shopsCreater.GetShopList())
            //{
            //    foreach (var y in x.ShopModelCollection)
            //    {
            //        allShops.Add(y);
            //    }
        //}
        CarouselItems = cc.GetCarouselItemCollection(); 
        }
        private byte[] _PhotoPath;
        public byte[] PhotoPath
        {
            get { return _PhotoPath; }
            set
            {
                _PhotoPath = value;
                DoPropertyChanged("PhotoPath ");
            }
        }

        private byte[] _PreviewPhotoPath;
        public byte[] PreviewPhotoPath
        {
            get { return _PreviewPhotoPath; }
            set
            {
                _PreviewPhotoPath = value;
                DoPropertyChanged("PreviewPhotoPath ");
            }
        }
        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ?? (_SearchCommand = new Command.Command(delegate
        {
            //if (!(PageContent.Content is SearchPage))
            //    PageContent.Content = new SearchPage();
        }));
        private void GetShownCategories()
        {
            ShownCategories = new ObservableCollection<CategoryModel>();
            ShownCategories = shopsCreater.GetShopList();
            //Random r = new Random();
            //for (int i = 0; i < 4; i++)
            //{
            //    int index = r.Next(0, AllCategories.Count - 1);
            //    while (ShownCategories.Contains(AllCategories[index]))
            //    {
            //        index = r.Next(0, AllCategories.Count - 1);
            //    }
            //    ShownCategories.Add(AllCategories[index]);
            //}
      //      ShownCategories.Add(new CategoryModel("Карта", null, "#9492DD", null));
        }
        ~MainPageViewModel()
        {

        }
    }
}
