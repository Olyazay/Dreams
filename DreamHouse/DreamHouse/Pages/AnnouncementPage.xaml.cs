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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для AnnouncementPage.xaml
    /// </summary>
    public partial class AnnouncementPage : Page, INotifyPropertyChanged
    {
        public AnnouncementPage(AnnouncementsModel ann)
        {
            InitializeComponent();
            //var p = GetDependencyObject(this, typeof(Page));
            //annonscr.Source = ArrayConvertToImage(ann.PreviewPhotoPath); 
            source = ann.PhotoPath;
            NameAnnons = ann.Heading;
            BodyAnnons = ann.Body;
        }
        public BitmapImage ArrayConvertToImage(byte[] array)
        {
            BitmapImage bmImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(array, 0, array.Length))
            {
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = ms;
                bmImage.EndInit();

                return bmImage;
            }
        }

        private byte[] _source;
        public byte[] source
        {
            get { return _source; }
            set
            {
                _source = value;
                DoPropertyChanged("source");
            }
        }
        private string _nameAnnons;
        public string NameAnnons
        {
            get { return _nameAnnons; }
            set
            {
                _nameAnnons = value;
                DoPropertyChanged("NameAnnons");
            }
        }
        private string _bodyAnnons;
        public string BodyAnnons
        {
            get { return _bodyAnnons; }
            set
            {
                _bodyAnnons = value;
                DoPropertyChanged("BodyAnnons");
            }
        }
        private ICommand _GoBackCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(String PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command.Command(delegate
        {
            //NavigationService?.GoBack();
            if (NavigationCommands.BrowseBack.CanExecute(null, null))
            {
                NavigationCommands.BrowseBack.Execute(null, null);
            }
        }));

        //private DependencyObject
        //    GetDependencyObject(DependencyObject startObject, Type type)
        //{
        //    DependencyObject parent = startObject;
        //    while (parent != null)
        //    {
        //        if (type.IsInstanceOfType(parent))
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            parent = VisualTreeHelper.GetParent(parent);
        //        }
        //    }
        //    return parent;
    }
}

