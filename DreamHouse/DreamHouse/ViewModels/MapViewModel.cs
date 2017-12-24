using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.ViewModels
{
    class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    //    private int _SelectedFloor;
    //    public int SelectedFloor
    //    {
    //        get { return _SelectedFloor; }
    //        set
    //        {
    //            _SelectedFloor = value;
    //            FrameSource = floorPath[value];
    //            OnPropertyChanged("SelectedFloor");
    //        }
    //    }

    //    private string[] floorPath = new string[] { "../Pages/FloorsPages/Floor0.xaml", "../Pages/FloorsPages/Floor1.xaml",
    //                                        "../Pages/FloorsPages/Floor2.xaml", "../Pages/FloorsPages/Floor3.xaml"};

    //private string _FrameSource;
    //public string FrameSource
    //{
    //    get { return _FrameSource; }
    //    set
    //    {
    //        _FrameSource = value;
    //        OnPropertyChanged("FrameSource");
    //    }
    //}

    public MapViewModel()
        {
          //  SelectedFloor = 0;
        }
    }
}
