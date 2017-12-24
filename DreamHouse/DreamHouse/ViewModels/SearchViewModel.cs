using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DreamHouse.Models;
using DreamHouse.DataReader; 

namespace DreamHouse.ViewModels
{
    public class SearchViewModel:INotifyPropertyChanged
    {
        private String _searchShop;
        public String SearchShop
        {
            get
            {
                return _searchShop;
            }
            set
            {
                _searchShop = value;
                OnPropertyChange("SearchShop");
                OnPropertyChange("ShopListSearched"); 
            }
        }
    
        public ObservableCollection<ShopModel> allshopslist { get; set; }
        public ObservableCollection<ShopModel> getallshopslist()
        {
            ShopsCreater shopsCreater = new ShopsCreater(); 
            //foreach (var x in shopsCreater.GetShopList())
            //{
            //    foreach (var y in x.ShopModelCollection)
            //    {
            //        allshopslist.Add(y);

            //    }




            //}
            allshopslist = shopsCreater.GetShopAllCollection(); 
            return allshopslist;
        }

        public SearchViewModel()
        {
          //  allshopslist = new ObservableCollection<ShopModel>();
        //    getallshopslist();
        }

        public IEnumerable<ShopModel> ShopListSearched
        {
            get
            {
                getallshopslist(); 
                if (string.IsNullOrWhiteSpace(SearchShop))
                {
                    return allshopslist;
                }
                else
                {
                    List<ShopModel> t = allshopslist.Where(x => x.NameShop != null && (x.NameShop).ToLower().Contains(SearchShop.ToLower())).ToList();
                    return t;
                }

            }
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
