using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace DreamHouse.Controls
{
    /// <summary>
    /// Логика взаимодействия для LogotipeOnMap.xaml
    /// </summary>
    public partial class LogotipeOnMap : UserControl
    {
        public LogotipeOnMap()
        {
            InitializeComponent();
        }


        public BitmapImage LogoImage
        {
            get { return (BitmapImage)GetValue(LogoImageProperty); }
            set { SetValue(LogoImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LogoImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogoImageProperty =
            DependencyProperty.Register("LogoImage", typeof(BitmapImage), typeof(LogotipeOnMap), new PropertyMetadata(new object()));


    }
}
