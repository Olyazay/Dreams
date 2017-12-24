using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class FloorModel:INotifyPropertyChanged
    {
        private int _numberFloor;
        public int NumberFloor
        {
            get
            {
                return _numberFloor;
            }
            set
            {
                _numberFloor = value;
                OnPropertyChange("NumberFloor");
            }
        }
        private ObservableCollection<PavilionModel> _pavilionList;
        public ObservableCollection<PavilionModel> PavilionList
        {
            get
            {
                return _pavilionList;
            }
            set
            {
                _pavilionList = value;
                OnPropertyChange("PavilionList");
            }
        }
        public FloorModel(int NumberFloor, ObservableCollection<PavilionModel> PavilionList)
        {
            this.NumberFloor = NumberFloor;
            this.PavilionList = PavilionList;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }

        }
        public FloorModel()
        {

        }
    }
}
