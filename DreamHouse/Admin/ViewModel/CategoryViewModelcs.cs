using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Admin.Commands;
using Admin.Converters;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Admin.FileProcessing;
using System.Collections.ObjectModel;

namespace Admin.ViewModel
{
    public class CategoryViewModelcs:INotifyPropertyChanged
    {
        ShopsCreater cr = ShopsCreater.GetLoadShopsCreater();
        public ObservableCollection<CategoryModels> CategoryCollection { get; set; }
        public CategoryViewModelcs()
        {
            Action<string> sd = (s) =>
            {
                CategoryCollection = cr.GetShopList();
            };
            Checker.CheckInternet(sd);
        }
        private String _nameCategory;
        public String NameCategory
        {
            get
            {
                return _nameCategory;
            }
            set
            {
                _nameCategory = value;
                OnPropertyChange("NameCategory");
            }
        }
        private string _BackgroundCategory;
        public string BackgroundCategory
        {
            get
            {
                return _BackgroundCategory;
            }
            set
            {
                _BackgroundCategory = value;
                OnPropertyChange("BackgroundCategory");
            }
        }
        private byte[] _pathCategoryPhoto;
        public byte[] PathCategoryPhoto
        {
            get
            {
                return _pathCategoryPhoto;
            }
            set
            {
                _pathCategoryPhoto = value;
                OnPropertyChange("PathCategoryPhoto");
            }
        }
        private CategoryModels _selectedCategory;
        public CategoryModels SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChange("SelectedCategory");
                GetDataAboutCategory();
            }
        }
        public void GetDataAboutCategory()
        {
            if (SelectedCategory != null)
            {
                using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                {
                    CategoryModels category = bd.CategoryModels.Find(SelectedCategory.Id);
                    NameCategory = SelectedCategory.NameCategory;
                    PathCategoryPhoto = SelectedCategory.PathCategoryPhoto;
                }
            }
        }
        private Command _saveOpenedCategoryImage;
        public Command SaveOpenedCategoryImage => _saveOpenedCategoryImage ?? (_saveOpenedCategoryImage = new Command(obj =>
        {
            CategoryModels newpavilion = obj as CategoryModels;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG)|*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string path = dlg.FileName;
                if (string.IsNullOrWhiteSpace(path)!=true&&SelectedCategory!=null)
                {
                    SelectedCategory.PathCategoryPhoto = ImageConverterToArray(path);
                    PathCategoryPhoto = ImageConverterToArray(path);
                }

        }
        ));
        private Command _saveToBdAboutCategory;
        public Command SaveToBdAboutCategory => _saveToBdAboutCategory ?? (_saveToBdAboutCategory = new Command(obj =>
        {
            CategoryModels newcategory = obj as CategoryModels;
            if (SelectedCategory != null)
            {
                using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
                {
                    newcategory = bd.CategoryModels.Find(SelectedCategory.Id);
                    newcategory.NameCategory = NameCategory;
                    //newcategory.PathCategoryPhoto = PathCategoryPhoto; 
                    bd.Entry(newcategory).State = EntityState.Modified;
                    bd.SaveChanges();
                }
            }
        }
        ));
        public byte[] ImageConverterToArray(string path)
        {
            Bitmap image = new Bitmap(path);
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
        private Command _AddCategory;
        public Command AddCategory => _AddCategory ?? (_AddCategory = new Command(obj =>
        {
            //CategoryModels newcategory = obj as CategoryModels;
            //    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            //    {
            //    NameCategory = null;
            //    PathCategoryPhoto = null;
            //        newcategory.NameCategory = NameCategory;
            //        newcategory.PathCategoryPhoto = PathCategoryPhoto;
            //        bd.CategoryModels.Add(newcategory);
            //        bd.SaveChanges();
            //    }

        }
        ));
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
