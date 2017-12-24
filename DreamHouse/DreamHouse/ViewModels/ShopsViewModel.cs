using DreamHouse.DataReader;
using DreamHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.ViewModels
{
    class ShopsViewModel: INotifyPropertyChanged
    {
        private string _Category;
        public string Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                OnPropertyChange("Category");
            }
        }

        public ObservableCollection<ShopModel> allshopslist { get; set; }
        public ObservableCollection<ShopModel> getallshopslist()
        {
            ShopsCreater shopsCreater = new ShopsCreater(); 
            foreach (var shop in shopsCreater.GetShopCollectionByCategory(Category))
            {
                allshopslist.Add(shop);
            }
            return allshopslist;
        }

        public ShopsViewModel()
        {
            allshopslist = new ObservableCollection<ShopModel>();
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
