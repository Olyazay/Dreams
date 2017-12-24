using DreamHouse.DataReader;
using DreamHouse.Models;
using DreamHouse.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace VremenaGoda.Controls
{
    /// <summary>
    /// Логика взаимодействия для GalerySlider.xaml
    /// </summary>
    public partial class GallerySlider : UserControl
    {
        public GallerySlider()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private Point? _mousePoint;
        private bool _moveNext;
        private DispatcherTimer _timer;

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_timer == null)
                _timer = new DispatcherTimer(
                    TimeSpan.FromSeconds(15),
                    DispatcherPriority.Loaded,
                    Timer_OnTick,
                    Dispatcher);
            _timer.Start();
        }
        
        private void Timer_OnTick(object sender, EventArgs eventArgs)
        {
            MoveNext();
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            nameof(Source), typeof(IEnumerable<CarouselItemModel>), typeof(GallerySlider),
            new PropertyMetadata(default(IEnumerable<CarouselItemModel>)));

        public IEnumerable<CarouselItemModel> Source
        {
            get => (IEnumerable<CarouselItemModel>) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private Storyboard MoveToPositionStoryboard => (Storyboard) Resources["MoveToPositionStoryboard"];
        private Storyboard CancelMoveStoryboard => (Storyboard) Resources["CancelMoveStoryboard"];
        private Storyboard OutMoveToNextStoryboard => (Storyboard) Resources["OutMoveToNextStoryboard"];
        private Storyboard OutMoveToPrevStoryboard => (Storyboard) Resources["OutMoveToPrevStoryboard"];
        private Storyboard InMoveToNextStoryboard => (Storyboard) Resources["InMoveToNextStoryboard"];
        private Storyboard InMoveToPrevStoryboard => (Storyboard) Resources["InMoveToPrevStoryboard"];
        private CollectionViewSource CollectionSource => (CollectionViewSource) Resources["CollectionSource"];

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed) return;
            Point point = e.GetPosition(RootGrid);
            if (_mousePoint.HasValue)
            {
                ((DoubleAnimation)MoveToPositionStoryboard.Children[0]).To = point.X - _mousePoint.Value.X;
                BeginStoryboard(MoveToPositionStoryboard);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            CheckState();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            CheckState();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            _mousePoint = e.GetPosition(RootGrid);
        }
        
        private void CheckState()
        {
            if(Source?.Count() > 1)
                if (Math.Abs(TranslateTransform.X) > ActualWidth * .2)
                {
                    if (TranslateTransform.X < 0)
                        MoveNext();
                    else
                        MovePreview();
                    return;
                }
            BeginStoryboard(CancelMoveStoryboard);
        }

        private void MoveNext()
        {
            if (Source == null || Source.Count() < 2) return;
            ((DoubleAnimation) OutMoveToNextStoryboard.Children[0]).To = -ActualWidth;
            BeginStoryboard(OutMoveToNextStoryboard);
        }

        private void MovePreview()
        {
            if (Source == null || Source.Count() < 2) return;
            ((DoubleAnimation) OutMoveToPrevStoryboard.Children[0]).To = ActualWidth;
            BeginStoryboard(OutMoveToPrevStoryboard);
        }

        private void OutMoveToNextStoryboard_OnCompleted(object sender, EventArgs e)
        {
            CollectionSource.View.CurrentChanged += ViewOnCurrentChanged;
            _moveNext = true;
            if (!CollectionSource.View.MoveCurrentToNext())
                CollectionSource.View.MoveCurrentToFirst();
        }

        private void OutMoveToPrevStoryboard_OnCompleted(object sender, EventArgs e)
        {
            CollectionSource.View.CurrentChanged += ViewOnCurrentChanged;
            _moveNext = false;
            if (!CollectionSource.View.MoveCurrentToPrevious())
                CollectionSource.View.MoveCurrentToLast();
        }

        private void ViewOnCurrentChanged(object sender, EventArgs e)
        {
            CollectionSource.View.CurrentChanged -= ViewOnCurrentChanged;
            if (_moveNext)
            {
                ((DoubleAnimation)InMoveToNextStoryboard.Children[0]).From = ActualWidth;
                BeginStoryboard(InMoveToNextStoryboard);
            }
            else
            {
                ((DoubleAnimation)InMoveToPrevStoryboard.Children[0]).From = -ActualWidth;
                BeginStoryboard(InMoveToPrevStoryboard);
            }
        }

        private void Move_OnCompleted(object sender, EventArgs e)
        {
            TranslateTransform.X = 0;
        }

        private void PrevButton_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            MovePreview();
            _timer.Start();
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            MoveNext();
            _timer.Start();
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(GallerySlider), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter), typeof(object), typeof(GallerySlider), new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        

        private DependencyObject
            GetDependencyObject(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                {
                    break;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MainPage page = GetDependencyObject(this, typeof(Page)) as MainPage;
            if(btn!=null && btn.Tag!=null)
            {
                page.OpenCarouselItem(btn.Tag);
            }
        }
    }
}