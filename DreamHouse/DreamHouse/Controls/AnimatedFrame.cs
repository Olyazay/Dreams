using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace DreamHouse.Controls
{
    public class AnimatedFrame : Frame
    {
        public AnimatedFrame()
        {
            Navigating += OnNavigating;
        }

        private bool _allowDirectNavigation;
        private ContentPresenter _contentPresenter;
        private NavigatingCancelEventArgs _navArgs;

        #region In animations

        public static readonly DependencyProperty NewInStoryboardProperty = DependencyProperty.Register(
            nameof(NewInStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard NewInStoryboard
        {
            get => (Storyboard)GetValue(NewInStoryboardProperty);
            set => SetValue(NewInStoryboardProperty, value);
        }

        public static readonly DependencyProperty BackInStoryboardProperty = DependencyProperty.Register(
            nameof(BackInStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard BackInStoryboard
        {
            get => (Storyboard)GetValue(BackInStoryboardProperty);
            set => SetValue(BackInStoryboardProperty, value);
        }

        public static readonly DependencyProperty ForwardInStoryboardProperty = DependencyProperty.Register(
            nameof(ForwardInStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard ForwardInStoryboard
        {
            get => (Storyboard)GetValue(ForwardInStoryboardProperty);
            set => SetValue(ForwardInStoryboardProperty, value);
        }

        public static readonly DependencyProperty RefreshInStoryboardProperty = DependencyProperty.Register(
            nameof(RefreshInStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard RefreshInStoryboard
        {
            get => (Storyboard)GetValue(RefreshInStoryboardProperty);
            set => SetValue(RefreshInStoryboardProperty, value);
        }

        #endregion

        #region Out animations

        public static readonly DependencyProperty NewOutStoryboardProperty = DependencyProperty.Register(
            nameof(NewOutStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard NewOutStoryboard
        {
            get => (Storyboard)GetValue(NewOutStoryboardProperty);
            set => SetValue(NewOutStoryboardProperty, value);
        }

        public static readonly DependencyProperty BackOutStoryboardProperty = DependencyProperty.Register(
            nameof(BackOutStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard BackOutStoryboard
        {
            get => (Storyboard)GetValue(BackOutStoryboardProperty);
            set => SetValue(BackOutStoryboardProperty, value);
        }

        public static readonly DependencyProperty ForwardOutStoryboardProperty = DependencyProperty.Register(
            nameof(ForwardOutStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard ForwardOutStoryboard
        {
            get => (Storyboard)GetValue(ForwardOutStoryboardProperty);
            set => SetValue(ForwardOutStoryboardProperty, value);
        }

        public static readonly DependencyProperty RefreshOutStoryboardProperty = DependencyProperty.Register(
            nameof(RefreshOutStoryboard), typeof(Storyboard), typeof(AnimatedFrame),
            new PropertyMetadata(default(Storyboard), OnStoryboardChanged));

        public Storyboard RefreshOutStoryboard
        {
            get => (Storyboard)GetValue(RefreshOutStoryboardProperty);
            set => SetValue(RefreshOutStoryboardProperty, value);
        }

        #endregion

        public static readonly DependencyProperty ContentRenderTransformProperty = DependencyProperty.Register(
            nameof(ContentRenderTransform), typeof(Transform), typeof(AnimatedFrame),
            new PropertyMetadata(default(Transform)));

        public Transform ContentRenderTransform
        {
            get => (Transform)GetValue(ContentRenderTransformProperty);
            set => SetValue(ContentRenderTransformProperty, value);
        }

        public static readonly DependencyProperty ContentRenderTransformOriginProperty = DependencyProperty.Register(
            nameof(ContentRenderTransformOrigin), typeof(Point), typeof(AnimatedFrame), new PropertyMetadata(new Point(0d, 0d)));

        public Point ContentRenderTransformOrigin
        {
            get => (Point)GetValue(ContentRenderTransformOriginProperty);
            set => SetValue(ContentRenderTransformOriginProperty, value);
        }

        private static void OnStoryboardChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is AnimatedFrame frame && e.NewValue is Storyboard storyboard)
                foreach (Timeline timeline in storyboard.Children)
                    Storyboard.SetTarget(timeline, frame._contentPresenter);
        }

        public override void OnApplyTemplate()
        {
            _contentPresenter = GetTemplateChild("PART_FrameCP") as ContentPresenter;
            if (_contentPresenter != null)
            {
                _contentPresenter.SetBinding(RenderTransformProperty,
                    new Binding("ContentRenderTransform") { Source = this });
                _contentPresenter.SetBinding(RenderTransformOriginProperty,
                    new Binding("ContentRenderTransformOrigin") { Source = this });
            }
            base.OnApplyTemplate();
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation && _contentPresenter != null)
            {
                Storyboard outStoryboard;
                switch (e.NavigationMode)
                {
                    case NavigationMode.New:
                        outStoryboard = NewOutStoryboard;
                        break;
                    case NavigationMode.Back:
                        outStoryboard = BackOutStoryboard;
                        break;
                    case NavigationMode.Forward:
                        outStoryboard = ForwardOutStoryboard;
                        break;
                    case NavigationMode.Refresh:
                        outStoryboard = RefreshOutStoryboard;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (outStoryboard != null)
                {
                    e.Cancel = true;
                    _navArgs = e;
                    _contentPresenter.IsHitTestVisible = false;
                    outStoryboard.Completed += OutCompleted;
                    _contentPresenter.BeginStoryboard(outStoryboard);
                }
            }
            _allowDirectNavigation = false;
        }

        private void OutCompleted(object sender, EventArgs e)
        {
            if (sender is Clock animationClock)
                animationClock.Completed -= OutCompleted;

            if (_contentPresenter == null) return;

            _contentPresenter.IsHitTestVisible = true;
            _allowDirectNavigation = true;
            Storyboard inStoryboard;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    inStoryboard = NewInStoryboard;
                    if (_navArgs.Uri == null)
                        NavigationService.Navigate(_navArgs.Content);
                    else
                        NavigationService.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    inStoryboard = BackInStoryboard;
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                    break;
                case NavigationMode.Forward:
                    inStoryboard = ForwardInStoryboard;
                    if (NavigationService.CanGoForward)
                        NavigationService.GoForward();
                    break;
                case NavigationMode.Refresh:
                    inStoryboard = RefreshInStoryboard;
                    NavigationService.Refresh();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                new ThreadStart(() => _contentPresenter.BeginStoryboard(inStoryboard)));
        }
    }
}