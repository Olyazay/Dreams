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
using DreamHouse.Models;
using DreamHouse.Pages.FloorsPages;
using DreamHouse.DataReader; 

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public ShopModel shop { get; set; }
        private OneShopViewModel Viewmodel { get; set; }
        public ShopPage(string shopName, string categoryName)
        {
            InitializeComponent();
            Viewmodel = DataContext as OneShopViewModel;
            Viewmodel.InitShop(shopName, categoryName);
            this.Unloaded += ShopPage_Unloaded;
        }

        private void ShopPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Viewmodel = null;
            shop = null; 
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command.Command(delegate
        {
            (App.Current.MainWindow as MainWindow).PageContent.Content = (App.Current.MainWindow as MainWindow).MainPageInstance;
        }));

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OneShopViewModel viewModel = DataContext as OneShopViewModel;
        //    NavigationService?.Navigate(new Floor0(viewModel.Shop));      
        //}

        //private ICommand _OpenNavigationCommand;
        //public ICommand OpenNavigationCommand => _OpenNavigationCommand ?? (_OpenNavigationCommand = new Command.Command(delegate
        //{
        //    (App.Current.MainWindow as MainWindow).PageContent.Content = new MapPage(NavigationButton.Tag as ShopModel);
        //}));
    }
}
