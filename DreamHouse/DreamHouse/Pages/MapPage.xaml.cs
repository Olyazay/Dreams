using DreamHouse.Models;
using DreamHouse.Pages.FloorsPages;
using DreamHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DreamHouse.DataReader;

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        public MapPage()
        {
            InitializeComponent();
            MapFrame.Content = new Floor0();
             R1.IsChecked = true; 

        }
    //    public PavilionCreator pc { get; set; }
        public MapPage(ShopModel shop)
        {
            InitializeComponent();
            //MapFrame.Content = new Floor0(shop);
            R1.IsChecked = true;
            this.Unloaded += MapPage_Unloaded;
   //         R1.IsChecked = true;
        //    MapFrame.Content = new Floor0();
            //pc = new PavilionCreator(); 
            //foreach (var x in pc.GetFloors())
            //{
            //    foreach (var y in x.PavilionList)
            //    {
            //        if (y.Shop != null)
            //        {
            //            if (y.Shop.NameShop == shop.NameShop)
            //            {

            //                shop = y.Shop;
            //            }
            //        }
            //    }
            //}
            
            switch (shop.FloorNumber)
            {
                case 0:
                    {
                        MapFrame.Content = new Floor0(shop);
                        R1.IsChecked = true;
                        break;
                    }
                case 1:
                    {
                        MapFrame.Content = new Floor1(shop);
                        R2.IsChecked = true;

                        break;
                    }
                case 2:
                    {
                        MapFrame.Content = new Floor2(shop);
                        R3.IsChecked = true;

                        break;
                    }
                case 3:
                    {
                        MapFrame.Content = new Floor3(shop);
                        R4.IsChecked = true;

                        break;
                    }
                default:
                    break;
            }
        }

        private void MapPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= MapPage_Unloaded;
            this.DataContext = null;
            this.Content = null;
        //    pc = null; 
        }

        private ICommand _BackCommand;
        public ICommand BackCommand => _BackCommand ?? (_BackCommand = new Command.Command(delegate
        {
            //NavigationService?.Navigate(new MainPage());
            (App.Current.MainWindow as MainWindow).PageContent.Content = (App.Current.MainWindow as MainWindow).MainPageInstance;
        }));

        public void NavigateToShop(string shopname)
        {
            if (shopname.ToLower() != "туалет")
                (App.Current.MainWindow as MainWindow).PageContent.Content = new ShopPage(shopname, null);
        }

        ~MapPage()
        {
            //pc = null;
        }

        private void R1_Click(object sender, RoutedEventArgs e)
        {
            MapFrame.Content = new Floor0(); 
        }

        private void R2_Click(object sender, RoutedEventArgs e)
        {
            MapFrame.Content = new Floor1();
        }

        private void R3_Click(object sender, RoutedEventArgs e)
        {
            MapFrame.Content = new Floor2(); 
        }

        private void R4_Click(object sender, RoutedEventArgs e)
        {
            MapFrame.Content = new Floor3(); 
        }
    }
}
