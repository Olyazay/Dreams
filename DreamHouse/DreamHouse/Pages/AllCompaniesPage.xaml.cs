using DreamHouse.Models;
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
using System.Collections.ObjectModel;

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllCompaniesPage.xaml
    /// </summary>
    public partial class AllCompaniesPage : Page
    {
        public AllCompaniesPage(ObservableCollection<ShopModel> category, string categoryName)
        {
            InitializeComponent();
            allshopslist = category;
            CategoryName.Text = categoryName;
            this.Unloaded += AllCompaniesPage_Unloaded;
        }

        private void AllCompaniesPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= AllCompaniesPage_Unloaded;
        }

        public ObservableCollection<ShopModel> allshopslist { get; set; }
        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command.Command(delegate
        {
            NavigationService?.GoBack();
        }));

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox s = sender as ListBox;
            if (s.SelectedIndex != -1)
            {
                var shop = e.AddedItems[0] as ShopModel;
                NavigationService?.Navigate(new ShopPage(shop.NameShop, CategoryName.Text));
            }
            s.SelectedIndex = -1;
        }
    }
}
