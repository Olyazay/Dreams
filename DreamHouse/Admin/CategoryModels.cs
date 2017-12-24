namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Collections.ObjectModel;

    public partial class CategoryModels:INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryModels()
        {
            ShopModels = new ObservableCollection<ShopModels>();
        }

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

        //public string Background { get; set; }


        //public byte[] PathCategoryPhoto { get; set; }

        //public string NameCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<ShopModels> ShopModels { get; set; }
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
