using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DreamHouse.Models
{
    [Serializable]
    public class CategoryModel:INotifyPropertyChanged
    {
        public int Id { get; set; }
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
        private string _Background;
        public string Background
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
        public CategoryModel(String NameCategory, byte[] PathCategoryPhoto, string BackgroundColor, ObservableCollection<ShopModel> ShopModelColletion)
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
