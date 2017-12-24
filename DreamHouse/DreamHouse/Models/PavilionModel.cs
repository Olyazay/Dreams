using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Xml.Serialization;

namespace DreamHouse.Models
{
    [Serializable]
    public class PavilionModel: INotifyPropertyChanged
    {
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
        
        public int Id { get; set; }
        public ShopModel Shop { get; set; }
        public int? FloorModelId { get; set; }
        public FloorModel floor { get; set; }
        public PavilionModel(String NumberPavilion, ShopModel Shop)
        {
            this.NumberPavilion = NumberPavilion;
            this.Shop = Shop; 
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public PavilionModel()
        {

        }
    }
}
