using DreamHouse.DataReader;
using DreamHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DreamHouse.ViewModels
{
    class OneShopViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ShopModel _Shop;
        public ShopModel Shop
        {
            get { return _Shop; }
            set
            {
                
                _Shop = value;
                //if (_Shop.NumberPavilion!= null)
                //{
                //    string temp = Regex.Replace(_Shop.NumberPavilion, @"[^\d]", "");
                //    _Shop.FloorNumber = int.Parse(temp.Substring(0, 1));
                //    if (temp.Count() >= 3)
                //    {
                //        _Shop.SpotNumber = temp.Substring(1, 2);
                //    }
                //    else
                //    {
                //        _Shop.SpotNumber = string.Empty;
                //    }
                //}
                if (_Shop.NumberShop != null && string.IsNullOrEmpty(_Shop.NumberShop) == false)
                    PhoneNumberVisibility = Visibility.Visible;
                else
                    PhoneNumberVisibility = Visibility.Collapsed;
                if (_Shop.Place!=null&&string.IsNullOrEmpty(_Shop.Place)==false)
                {
                    PlaceShopVisibility = Visibility.Visible; 
                }
                else
                {
                    PlaceShopVisibility = Visibility.Collapsed; 
                }
                if (_Shop.SpotNumber != null && string.IsNullOrEmpty(_Shop.SpotNumber) == false)
                    SpotNumberVisibility = Visibility.Visible;
                else
                    SpotNumberVisibility = Visibility.Collapsed;
                OnPropertyChanged("Shop");
            }
        }

        private Visibility _PhoneNumberVisibility;
        public Visibility PhoneNumberVisibility
        {
            get { return _PhoneNumberVisibility; }
            set
            {
                _PhoneNumberVisibility = value;
                OnPropertyChanged("PhoneNumberVisibility");
            }
        }
        private Visibility _placeShopVisibility;
        public Visibility PlaceShopVisibility
        {
            get { return _placeShopVisibility; }
            set
            {
                _placeShopVisibility = value;
                OnPropertyChanged("PlaceShopVisibility");
            }
        }
        private Visibility _SpotNumberVisibility;
        public Visibility SpotNumberVisibility
        {
            get { return _SpotNumberVisibility; }
            set
            {
                _SpotNumberVisibility = value;
                OnPropertyChanged("SpotNumberVisibility");
            }
        }
        public ObservableCollection<ShopModel> allShops { get; set; }
        public void InitShop(string name, string category)
        {
            PhoneNumberVisibility = Visibility.Visible;
            SpotNumberVisibility = Visibility.Visible;
            if (category==null)
            {
                ShopsCreater shopslist = new ShopsCreater(); 
                foreach (var item in shopslist.GetShopAllCollection())
                {
                    if ((item.NameShop == name) )
                    {
                        //Shop = new ShopModel(null, null, null, null, null, null, null,null,null,null);
                        Shop = item;
                        break;
                    }
                }
            }
            if (category!=null)
            {
                ShopsCreater shops = new ShopsCreater(); 
            allShops = new ObservableCollection<ShopModel>();
            allShops = shops.GetShopCollectionByCategory(category);
            foreach (var item in allShops)
            {
                if((item.NameShop==name))
                {
                    //Shop = new ShopModel(null,null, null, null, null, null, null,null,null,null);
                    Shop = item;
                        break;
                }
            }
            }

        }
        ~OneShopViewModel()
        {
            allShops = null;
        }
    }
}
