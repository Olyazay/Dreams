namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel; 

    public partial class PavilionModels:INotifyPropertyChanged
    {
        //public int Id { get; set; }
        private int _id;
        public int Id
        {
            get
            {
                return _id; 
            }
            set
            {
                _id= value;
                OnPropertyChange("Id");
            }
        }

        public string NumberPavilion { get; set; }


        public int? FloorModelId { get; set; }

        public int? Shop_Id { get; set; }

        public virtual FloorModels FloorModels { get; set; }

        //public virtual ShopModels ShopModels { get; set; }
        private ShopModels _shopModels;
        public ShopModels ShopModels
        {
            get
            {
                return _shopModels;
            }
            set
            {
                _shopModels = value;
                OnPropertyChange("ShopModels");
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
