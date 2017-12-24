using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    public class PavilionModel:INotifyPropertyChanged
    {
        public int Id { get; set; }
        private String _numberPavilion;
        public String NumberPavilion
        {
            get
            {
                return _numberPavilion;
            }
            set
            {
                _numberPavilion = value;
                OnPropertyChange("NumberPavilion");
            }
        }
        private ShopModel _shop;
        public ShopModel Shop
        {
            get
            {
                return _shop;
            }
            set
            {
                _shop = value;
                OnPropertyChange("Shop");
            }
        }
        public PavilionModel(String NumberPavilion, ShopModel Shop)
        {
            this.NumberPavilion = NumberPavilion;
            this.Shop = Shop;
        }
        public PavilionModel()
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
