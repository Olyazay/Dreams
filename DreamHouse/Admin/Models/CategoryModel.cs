using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Admin.Models
{
    [Serializable]
    public class CategoryModel : INotifyPropertyChanged
    {
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
        private Color _Background;
        public Color Background
        {
            get
            {
                return _Background;
            }
            set
            {
                _Background = value;
                OnPropertyChange("NameCategory");
            }
        }
        private String _pathCategoryPhoto;
        public String PathCategoryPhoto
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
        private ObservableCollection<ShopModel> _shopModelColletion;
        public ObservableCollection<ShopModel> ShopModelCollection
        {
            get
            {
                return _shopModelColletion;
            }
            set
            {
                _shopModelColletion = value;
                OnPropertyChange("ShopModelColletion");
            }
        }
        public CategoryModel(String NameCategory, String PathCategoryPhoto, Color BackgroundColor, ObservableCollection<ShopModel> ShopModelColletion)
        {
            this.NameCategory = NameCategory;
            this.PathCategoryPhoto = PathCategoryPhoto;
            this.ShopModelCollection = ShopModelColletion;
            this.Background = BackgroundColor;
        }
        public CategoryModel()
        {

        }
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
