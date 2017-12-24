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
using DreamHouse.Models;
using System.Windows.Media.Effects;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace DreamHouse.Pages.FloorsPages
{
    /// <summary>
    /// Логика взаимодействия для Floor2.xaml
    /// </summary>
    public partial class Floor2 : Page
    {
        public Floor2()
        {
            InitializeComponent();
            Floors = new PavilionCreator().secondfloor; 
            FindEllipses();
            ImageToShop();
            DesirilisationMatrix();
            DesirilisationweightMatrix();
            DesirilisationpathMatrix();
            //Serilisation();
            //Serilisation1();
            //Serilisation2();
            this.Unloaded += Floor2_Unloaded;
        }
        public void DesirilisationMatrix()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("matrix3.dat", FileMode.OpenOrCreate))
            {
                matrix = (Double[,])formatter.Deserialize(fs);
            }
        }
        public void DesirilisationweightMatrix()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("weightMatrix3.dat", FileMode.OpenOrCreate))
            {
                weightMatrix = (Double[,])formatter.Deserialize(fs);
            }
        }
        public void DesirilisationpathMatrix()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("pathMatrix3;.dat", FileMode.OpenOrCreate))
            {
                pathMatrix = (int[,])formatter.Deserialize(fs);
            }
        }
        public void Serilisation()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Matrix3.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, matrix);
            }
        }
        public void Serilisation1()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("weightMatrix3.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, weightMatrix);
            }
        }
        public void Serilisation2()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("pathMatrix3;.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, pathMatrix);
            }
        }
        private void Floor2_Unloaded(object sender, RoutedEventArgs e)
        {
            images = null;
            ellipseCollection = null;
            Floors = null;
        }
        public List<Grid> images;
        public BitmapImage ArrayConvertToImage(byte[] array)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(array, 0, array.Length))
            {
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = ms;
                bmImage.EndInit();
                bmImage.DecodePixelHeight = 1;
                bmImage.DecodePixelWidth = 1;

                return bmImage;
            }
        }
        public void ImageToShop()
        {
            images = new List<Grid>();
            List<Ellipse> ellipses = canvasmap.Children.OfType<Ellipse>().Where(x => (!String.IsNullOrWhiteSpace(x.Name) && x.Name != "start")).ToList();
            foreach (var ellipse in ellipses)
            {
                PavilionModel p = Floors.FirstOrDefault(x => (x.Shop != null) && (x.NumberPavilion == ellipse.Name));
                    Grid grid = new Grid();
                    if (p.Shop.PhotoPathShop2 != null)
                    {
                        ImageBrush gridBack = new ImageBrush(ArrayConvertToImage(p.Shop.PhotoPathShop2));
                        gridBack.Stretch = Stretch.Uniform;
                        grid.Background = gridBack;
                        // img.Source = new BitmapImage((ArrayConvertToImage(p.Shop.PhotoPathShop2));
                        //img.Width = 15;
                        //img.Height = 15;
                        grid.Width = 20;
                        grid.Height = 20;
                        grid.RenderTransformOrigin = new Point(0.5, 0.5);
                        grid.RenderTransform = new RotateTransform(180);
                        grid.Tag = ellipse.Name;
                        grid.MouseEnter += Path_MouseEnter;
                        grid.MouseLeave += Path_MouseLeave;
                        Point start = new Point(Double.Parse(ellipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(ellipse.GetValue(Canvas.TopProperty).ToString()));
                        grid.SetValue(Canvas.TopProperty, start.Y - 2);
                        grid.SetValue(Canvas.LeftProperty, start.X - 5);
                        canvasmap.Children.Add(grid);

                    }
                
            }

        }
        public Floor2(ShopModel shopik)
        {
            InitializeComponent();
            DesirilisationMatrix();
            DesirilisationweightMatrix();
            DesirilisationpathMatrix();
            Floors = new PavilionCreator().secondfloor; 
            FindEllipses();
            try
            {
                DrawRoute("start", shopik.NumberPavilion);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            ChoosenShop = shopik;
            shopname.Content = shopik.NameShop;
            ImageToShop();
            this.Unloaded += Floor2_Unloaded;
        }

        IEnumerable<Ellipse> ellipseCollection;
        Polyline l;
        const int INF = int.MaxValue;
        int countCoordinates = 1;
        Double[,] matrix;
        Double[,] weightMatrix;
        int[,] pathMatrix;
        int length;
        Point TempStartPoint;
        Point TempEndPoint;
        #region HooliganMethods
        private Ellipse FindTag(int firstTag)
        {
            Ellipse outEllipse = null;
            foreach (var item in ellipseCollection)
            {
                if (item.Tag != null && item.Tag.ToString().Split(',')[0] == firstTag.ToString())
                {
                    outEllipse = item;
                }
            }
            if (outEllipse == null)
            {
                throw new NullReferenceException("Не найдено элемента с заданным Tag");
            }
            return outEllipse;
        }

        private int GetTagFirstElementFromEllipse(String ellipseName)
        {
            Ellipse ellipse;
            try
            {
                ellipse = this.FindName(ellipseName) as Ellipse;
            }
            catch (Exception ex)
            {
                ellipse = null;
                //MessageBox.Show(ex.Message);
                throw new NullReferenceException("не надено точки");
            }
            String[] ellipseTag = ellipse.Tag.ToString().Split(',');
            int TagInt = 0;
            bool TagFlag = int.TryParse(ellipseTag[0], out TagInt);
            if (!TagFlag) throw new NullReferenceException("не удалось преобразовать элемент в int");
            return TagInt;
        }

        private void FindEllipses()
        {

            ellipseCollection = canvasmap.Children.OfType<Ellipse>();
            //matrix = new Double[ellipseCollection.Count(), ellipseCollection.Count()];
            //length = matrix.GetLength(0);
            //pathMatrix = new int[length, length];
            //for (int i = 0; i < length; i++)
            //{
            //    for (int j = 0; j < length; j++)
            //    {
            //        if (i == j) matrix[i, j] = 0;
            //        else matrix[i, j] = INF;
            //        pathMatrix[i, j] = j;
            //    }
            //}
            //foreach (var item in ellipseCollection)
            //{
            //    List<String> tags = item.Tag.ToString().Split(',').ToList();
            //    int first = 0;
            //    bool flag = int.TryParse(tags[0], out first);
            //    if (!flag) throw new NullReferenceException("не удалось преобразовать первый элемент в int");
            //    //if (first == 6)
            //    //{

            //    //}
            //    tags.RemoveAt(0);
            //    foreach (var tag in tags)
            //    {
            //        int TagInt = 0;
            //        bool TagFlag = int.TryParse(tag, out TagInt);
            //        if (!TagFlag) throw new NullReferenceException("не удалось преобразовать элемент в int");
            //        matrix[first - 1, TagInt - 1] = GetLegthBetweenEllipses(FindTag(first), FindTag(TagInt));
            //    }
            //}
            ////for (int i = 0; i < length; i++)
            ////{
            ////    for (int j = 0; j < length; j++)
            ////    {
            ////        Debug.Write(matrix[i, j] + "        ");
            ////    }
            ////    Debug.WriteLine("");
            ////}
            //weightMatrix = matrix;
            ////создание матрицы растояний между всеми элементами и матрицы предков
            //for (int k = 0; k < length; k++)
            //{
            //    for (int i = 0; i < length; i++)
            //    {
            //        for (int j = 0; j < length; j++)
            //        {
            //            double min = weightMatrix[i, k] + weightMatrix[k, j];
            //            if (weightMatrix[i, j] > min)
            //            {
            //                weightMatrix[i, j] = min;
            //                pathMatrix[i, j] = pathMatrix[i, k];
            //            }
            //        }
            //    }
            //}
        }

        private Double GetLegthBetweenPoints(Point start, Point end)
        {
            Double X = Math.Abs(end.X - start.X);
            Double Y = Math.Abs(end.Y - start.Y);
            Double legth = Math.Sqrt(X * X + Y * Y);
            return legth;
        }

        private Double GetLegthBetweenEllipses(Ellipse startEllipse, Ellipse endEllipse)
        {
            Point startPoint = new Point(Double.Parse(startEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startEllipse.GetValue(Canvas.TopProperty).ToString()));
            Point endPoint = new Point(Double.Parse(endEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endEllipse.GetValue(Canvas.TopProperty).ToString()));
            return GetLegthBetweenPoints(startPoint, endPoint);
        }

        //метод получения отдельного восстановленного пути
        private List<int> GetClosestRoute(int startIndex, int endIndex)
        {
            List<int> pathList = new List<int>();
            int k = startIndex - 1;
            while (k != endIndex - 1)
            {
                pathList.Add(k);
                k = pathMatrix[k, endIndex - 1];
            }
            pathList.Add(k);
            return pathList;
        }

        public void DrawRoute(String StartElement, String EndElement)
        {
            Ellipse startElement;
            Ellipse endElement;
            try
            {
                startElement = (Ellipse)this.FindName(StartElement);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("не найдено начальной точки");
            }

            try
            {
                endElement = (Ellipse)this.FindName(EndElement);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("не найдено конечной точки");
            }

            Point startPoint = new Point(Double.Parse(startElement.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startElement.GetValue(Canvas.TopProperty).ToString()));
            Point endPoint = new Point(Double.Parse(endElement.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endElement.GetValue(Canvas.TopProperty).ToString()));
            List<int> route = GetClosestRoute(GetTagFirstElementFromEllipse(StartElement), GetTagFirstElementFromEllipse(EndElement));
            if (l == null)
            {
                l = new Polyline();
                canvasmap.Children.Add(l);
            }
            l.Stroke = new SolidColorBrush(Color.FromRgb(100, 120, 255));
            l.StrokeThickness = 2.0;
            l.StrokeEndLineCap = PenLineCap.Round;
            l.StrokeStartLineCap = PenLineCap.Round;
            l.StrokeLineJoin = PenLineJoin.Round;
            l.Points = new PointCollection();
            l.Name = "NewLine";
            DropShadowEffect effect = new DropShadowEffect();
            effect.Opacity = 0.5;
            effect.BlurRadius = 3;
            effect.ShadowDepth = 0;
            l.Effect = effect;
            Ellipse startEllipse = FindTag(route[0] + 1);
            Point StartPoint = new Point(Double.Parse(startEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startEllipse.GetValue(Canvas.TopProperty).ToString()));
            l.Points.Add(new Point(StartPoint.X + startEllipse.ActualWidth / 2, StartPoint.Y + startEllipse.ActualHeight / 2));
            Canvas.SetZIndex(l, 1);
            if (route.Count < 3 && route[1] != 204)
            {
                //throw new Exception("Не удается построить маршрут напрямую");
                //MessageBox.Show("Не удается построить маршрут к данной комнате\nВозможно, она находится в другом корпусе");
            }
            else
            {
                for (int i = 0; i < route.Count - 2; i++)
                {
                    DrawOneLine(route[i + 1]);
                }
            }
        }

        private void DrawOneLine(int EndElement)
        {
            Ellipse endEllipse = FindTag(EndElement + 1);
            TempEndPoint = new Point(Double.Parse(endEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endEllipse.GetValue(Canvas.TopProperty).ToString()));
            l.Points.Add(new Point(TempEndPoint.X + endEllipse.ActualWidth / 2, TempEndPoint.Y + endEllipse.ActualHeight / 2));
        }
        #endregion
        public ShopModel ChoosenShop { get; set; }
        public ObservableCollection<PavilionModel> Floors { get; set; }
        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            String tag = String.Empty;
            if (sender as System.Windows.Shapes.Path != null)
            {
                System.Windows.Shapes.Path p = sender as System.Windows.Shapes.Path;
                if (p.Tag != null)
                {
                    p.Fill = Brushes.LightBlue;
                }
                try
                {
                    tag = ((System.Windows.Shapes.Path)sender).Tag.ToString();
                }
                catch
                {
                    return;
                }
            }
            else if (sender as Grid != null)
            {
                try
                {
                    tag = ((Grid)sender).Tag.ToString();
                    System.Windows.Shapes.Path p = FindPathByTag(tag);
                    p.Fill = Brushes.LightBlue;
                }
                catch
                {
                    return;
                }
            }
            else return;
            try
            {
                if (tag != null)
                {
                    PavilionModel pav = Floors.FirstOrDefault(x => x.Shop.NumberPavilion == tag);
                    if ((pav.Shop.NameShop != null) && (pav.Shop.NameShop != ""))
                    {
                        DrawRoute("start", tag);

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            if (tag != null)
            {
                //if (l != null)
                //{
                //    l.Points = new PointCollection();
                //}
                foreach (var x in Floors)
                {
                    if ((x.Shop != null))
                    {
                        if (x.Shop.NumberPavilion == tag.ToString())
                        {
                            ChoosenShop = x.Shop;
                            shopname.Content = x.Shop.NameShop;
                        }
                    }

                }
            }
        }

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            String tag = String.Empty;
            if (sender as System.Windows.Shapes.Path != null)
            {
                System.Windows.Shapes.Path p = sender as System.Windows.Shapes.Path;
                if (p.Tag != null)
                {
                    p.Fill = new SolidColorBrush(Color.FromRgb(204, 204, 203));
                }
                try
                {
                    tag = ((System.Windows.Shapes.Path)sender).Tag.ToString();
                }
                catch
                {
                    return;
                }
            }
            else if (sender as Grid != null)
            {
                try
                {
                    tag = ((Grid)sender).Tag.ToString();
                    System.Windows.Shapes.Path p = FindPathByTag(tag);
                    p.Fill = new SolidColorBrush(Color.FromRgb(204, 204, 203));
                }
                catch
                {
                    return;
                }
            }
            else return;
        }

        private System.Windows.Shapes.Path FindPathByTag(string firstTag)
        {
            System.Windows.Shapes.Path outPath = null;
            foreach (var item in GridPath.Children.OfType<System.Windows.Shapes.Path>())
            {
                if (item.Tag != null && item.Tag.ToString() == firstTag)
                {
                    outPath = item;
                }
            }
            if (outPath == null)
            {
                throw new NullReferenceException("Не найдено элемента с заданным Tag");
            }
            return outPath;
        }

        private void shopname_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosenShop != null)
            {
                var par = GetDependencyObject(this, typeof(MapPage)) as MapPage;
                par.NavigateToShop(ChoosenShop.NameShop);
            }
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
    }
}
