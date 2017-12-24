using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DreamHouse.Models;
using DreamHouse.DataReader;
using System.Collections.ObjectModel;

namespace DreamHouse.ViewModels
{
    public class CategoryViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<CategoryModel> _categoryList;
        public ObservableCollection<CategoryModel> CategoryList
        {
            get
            {
                return _categoryList;
            }
            set
            {
                _categoryList = value;
                ShopsCreater shopsCreater = new ShopsCreater(); 
                _categoryList = shopsCreater.GetShopList(); 
                OnPropertyChange("CategoryList");
            }
        }
        public CategoryViewModel()
        {
            CategoryList = new ObservableCollection<CategoryModel>();
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
