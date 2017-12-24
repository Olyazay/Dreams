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
using DreamHouse.Pages;
using System.Windows.Media.Animation;
using DreamHouse.DataReader;
using System.ComponentModel;
using DreamHouse.Models;
using System.Diagnostics;

namespace DreamHouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            inactionUser = new InactionUser(ReturnMainPage);
            inactionUser.Start();
            //PageContent.Source = new Uri("Pages/MainPage.xaml",UriKind.Relative);

            //this.Cursor = Cursors.None;
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM explorer.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process?.WaitForExit();
            Closing += (e, a) => Process.Start(System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));

        }

        private void ReturnMainPage()
        {
            if(!(PageContent.Content is MainPage))
                GoHomeCommand.Execute(null);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainWindow_Loaded;
            MainPageInstance = new MainPage();
            MapPageInstance = new MapPage();
            SearchPageInstance = new SearchPage();
            PageContent.Content = MainPageInstance;
        }

        private InactionUser inactionUser;
        private MainPage _MainPageInstance;
        private SearchPage _SearchInstance;

        public SearchPage SearchPageInstance
        {
            get { return _SearchInstance; }
            set
            {
                _SearchInstance = value;
                DoPropertyChanged("SearchPageInstance");
            }
        }
        public MainPage MainPageInstance
        {
            get { return _MainPageInstance; }
            set
            {
                _MainPageInstance = value;
                DoPropertyChanged("MainPageInstance");
            }
        }

        private MapPage _MapPageInstance;
        public MapPage MapPageInstance
        {
            get { return _MapPageInstance; }
            set
            {
                _MapPageInstance = value;
                DoPropertyChanged("MapPageInstance");
            }
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ?? (_SearchCommand = new Command.Command(delegate
        {
            if (!(PageContent.Content is SearchPage))
                PageContent.Content = new SearchPage();
        }));

        private void PageContent_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            PageContent.Navigating += PageContent_Navigating;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private ICommand _GoHomeCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(String PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public ICommand GoHomeCommand => _GoHomeCommand ?? (_GoHomeCommand = new Command.Command(delegate
        {
            if (PageContent.Content.GetType() != typeof(MainPage))
            {
                PageContent.Content = null;
                PageContent.Content = MainPageInstance;
                //    car.Dispose();
                // PageContent.NavigationService.Content = NavigationCommands.FirstPage;
            }
        }));
    }
}
