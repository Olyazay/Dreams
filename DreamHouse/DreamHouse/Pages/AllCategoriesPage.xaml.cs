using DreamHouse.Models;
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
    /// Логика взаимодействия для AllCategoriesPage.xaml
    /// </summary>
    public partial class AllCategoriesPage : Page
    {
        public AllCategoriesPage()
        {
            InitializeComponent();
            this.Unloaded += AllCategoriesPage_Unloaded;
        }

        private void AllCategoriesPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= AllCategoriesPage_Unloaded;
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command.Command(delegate
        {
            NavigationService?.GoBack();
        }));

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = e.AddedItems[0] as CategoryModel;
            NavigationService?.Navigate(new AllCompaniesPage(category.ShopModelCollection,category.NameCategory));
        }
    }
}
