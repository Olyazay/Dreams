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
using DreamHouse.Command;
using DreamHouse.Models;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using DreamHouse.ViewModels;

namespace DreamHouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        ~MainPage()
        { }
        public MainPage()
        {
            InitializeComponent();
            //InitCarousel();
            this.Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= MainPage_Unloaded;
            //_timer.Stop();
            //_swipeTimer.Stop();
        }

        private ICommand _CategoriesCommand;
        public ICommand CategoriesCommand => _CategoriesCommand ?? (_CategoriesCommand = new Command.Command(delegate
        {
            (App.Current.MainWindow as MainWindow).PageContent.Content = new AllCategoriesPage();
        }));

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MainGrid.Margin = new Thickness(0, 0, 0, 0);
            //MainGrid.Opacity = 1;
            //ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
            //anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            //anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-4320 * 0.41, 0, 0, 0), KeyTime.FromPercent(.11)));
            //anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-4320 * 0.7, 0, 0, 0), KeyTime.FromPercent(.31)));
            //anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-4320 * 0.89, 0, 0, 0), KeyTime.FromPercent(.56)));
            //anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-4320 * 0.97, 0, 0, 0), KeyTime.FromPercent(.75)));
            //anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-4320, 0, 0, 0), KeyTime.FromPercent(1)));
            //DoubleAnimation opacityAnimation = new DoubleAnimation();
            //opacityAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            //opacityAnimation.To = 0;
            //anim.Completed += (s, ea) =>
            //{
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedIndex != -1)
            {
                var category = e.AddedItems[0] as CategoryModel;
                if (category.NameCategory.ToLower() == "карта")
                {
                    //NavigationService?.Navigate(new MapPage());
                    (App.Current.MainWindow as MainWindow).PageContent.Content = new MapPage();
                }
                else
                {
                    //NavigationService?.Navigate(new AllCompaniesPage(category.ShopModelCollection, category.NameCategory));
                    (App.Current.MainWindow as MainWindow).PageContent.Content = new AllCompaniesPage(category.ShopModelCollection, category.NameCategory);
                }
            }
            listBox.SelectedIndex = -1;
            //};
            //MainGrid.BeginAnimation(MarginProperty, anim);
            //MainGrid.BeginAnimation(OpacityProperty, opacityAnimation);

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedIndex != -1)
            {
                AnnouncementsModel model = e.AddedItems[0] as AnnouncementsModel;
                //NavigationService?.Navigate(new AnnouncementPage(model));
                (App.Current.MainWindow as MainWindow).PageContent.Content = new AnnouncementPage(model);
            }
            listBox.SelectedIndex = -1;
        }

        //#region Carousel

        //private List<Button> _buttons;
        //private double SPRINESS = 0.1;
        //private double _target = 0;
        //private double _current = 0;
        //private double _spring = 0;
        //private DispatcherTimer _timer = new DispatcherTimer();
        //private DispatcherTimer _swipeTimer = new DispatcherTimer();

        //private void InitCarousel()
        //{
        //    var viewModel = DataContext as MainPageViewModel;
        //    _buttons = new List<Button>();
        //    AddButtons(viewModel.CarouselItems);
        //    StartCarousel();
        //}

        //private void AddButtons(ObservableCollection<CarouselItemModel> items)
        //{
        //    foreach (var item in items)
        //    {
        //        Image img = new Image() { Width = 1750, VerticalAlignment = VerticalAlignment.Top, Stretch = Stretch.Uniform, };
        //        img.Source = new BitmapImage(new Uri(item.PhotoPath, UriKind.Relative));
        //        TextBlock heading = new TextBlock()
        //        {
        //            Text = item.Heading,
        //            FontSize = 60,
        //            Foreground = new SolidColorBrush(Color.FromRgb(48, 48, 48)),
        //            Style = (Style)Resources["BoldTextBlock"],
        //            Margin = new Thickness(8, 60, 0, 0),
        //        };
        //        TextBlock description = new TextBlock()
        //        {
        //            Text = item.Description,
        //            FontSize = 50,
        //            Foreground = new SolidColorBrush(Color.FromRgb(48, 48, 48)),
        //            Style = (Style)Resources["LightTextBlock"],
        //            Margin = new Thickness(8, 16, 0, 0),
        //        };
        //        StackPanel stackPanel = new StackPanel();
        //        stackPanel.Children.Add(img);
        //        stackPanel.Children.Add(heading);
        //        stackPanel.Children.Add(description);
        //        Button btn = new Button()
        //        {
        //            MaxHeight = 1400,
        //            Width = 1750,
        //            Style = (System.Windows.Style)Resources["BlankButton"],
        //            Tag = item.Object,
        //        };
        //        btn.Click += Btn_Click;
        //        btn.Content = stackPanel;
        //        _buttons.Add(btn);
        //    }
        //    for (int i = 0; i < _buttons.Count(); i++)
        //    {
        //        CarouselCanvas.Children.Add(_buttons[i]);
        //        PosButtons(_buttons[i], i);
        //    }
        //}

        public void OpenCarouselItem(object tag)
        {
            if (tag == null)
                return;
            if (tag.GetType() == typeof(ShopModel))
            {
                var shop = (ShopModel)tag;
                //NavigationService?.Navigate(new ShopPage(shop.NameShop, null));
                (App.Current.MainWindow as MainWindow).PageContent.Content = new ShopPage(shop.NameShop,null);

            }
            if (tag.GetType() == typeof(AnnouncementsModel))
            {
                var announcement = (AnnouncementsModel)tag;
                //NavigationService?.Navigate(new AnnouncementPage(announcement));
                (App.Current.MainWindow as MainWindow).PageContent.Content = new AnnouncementPage(announcement);

            }
        }

        //private void PosButtons(Button btn, int index)
        //{
        //    double diffFactor = index - _current;

        //    double left = 1750 * diffFactor;
        //    double top = 0;
        //    btn.SetValue(Canvas.LeftProperty, left);
        //    btn.SetValue(Canvas.TopProperty, top);
        //}

        //private void MoveIndex(int value)
        //{
        //    _target += value;
        //    if(_target>_buttons.Count()-1)
        //    {
        //        _target = 0;
        //    }
        //    if(_target<0)
        //    {
        //        _target = _buttons.Count() - 1;
        //    }
        //}

        //private void StartCarousel()
        //{
        //    _timer = new DispatcherTimer();
        //    _timer.Interval = new TimeSpan(0, 0, 0, 0, 40);
        //    _timer.Tick += _timer_Tick;
        //    _timer.Start();
        //    _swipeTimer = new DispatcherTimer();
        //    _swipeTimer.Interval = new TimeSpan(0, 0, 10);
        //    _swipeTimer.Tick += _swipeTimer_Tick;
        //    _swipeTimer.Start();
        //}

        //private void _swipeTimer_Tick(object sender, EventArgs e)
        //{
        //    MoveIndex(1);
        //}

        //private void _timer_Tick(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < _buttons.Count; i++)
        //    {
        //        Button btn = _buttons[i];
        //        PosButtons(btn, i);
        //    }

        //    if(_target==_buttons.Count)
        //    {
        //        _target = 0;
        //    }
        //    _spring = (_target - _current) * SPRINESS;
        //    _current += _spring;
        //}

        //private void ScrollLeft_Click(object sender, RoutedEventArgs e)
        //{
        //    _swipeTimer.Stop();
        //    MoveIndex(1);
        //    _swipeTimer.Start();
        //}

        //private void ScrollRight_Click(object sender, RoutedEventArgs e)
        //{
        //    _swipeTimer.Stop();
        //    MoveIndex(-1);
        //    _swipeTimer.Start();
        //}

        //#endregion
        private ICommand _SearchCommand;
        public ICommand SearchCommand => _SearchCommand ?? (_SearchCommand = new Command.Command(delegate
        {
            (App.Current.MainWindow as MainWindow).PageContent.Content = (App.Current.MainWindow as MainWindow).SearchPageInstance;
        }));
        
    }
}
