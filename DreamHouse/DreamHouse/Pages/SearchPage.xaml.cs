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
using System.Windows.Media.Animation;
using DreamHouse.ViewModels;

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            this.Loaded += SearchPage_Loaded;
        }

        private void SearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= SearchPage_Loaded;
            //var viewmodel = (SearchViewModel)this.DataContext;
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command.Command(delegate
        {
            //NavigationService?.GoBack();
            if (NavigationCommands.BrowseBack.CanExecute(null, null))
            {
                NavigationCommands.BrowseBack.Execute(null, null);
            }
        }));

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (alllist.SelectedIndex != -1)
            {
                var ChoosenShop = alllist.SelectedItem as ShopModel;
                if (ChoosenShop != null)
                {
                    NavigationService?.Navigate(new ShopPage(ChoosenShop.NameShop, null));
                }
            }
            alllist.SelectedIndex = -1;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("HigherOpacity") as Storyboard;
            Storyboard.SetTarget(sb, keyboardControl);
            sb.Begin();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("LowerOpacity") as Storyboard;
            Storyboard.SetTarget(sb, keyboardControl);
            sb.Begin();
        }
    }
}
