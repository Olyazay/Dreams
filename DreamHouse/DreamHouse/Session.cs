using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse
{
    public class Session : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(String PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }


        private bool _IsUpdating = false;
        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set
            {
                _IsUpdating = value;
                DoPropertyChanged("IsUpdating");
            }
        }

        private static Session _currentSession;
        public static Session CurrentSession => _currentSession ?? (_currentSession = new Session());
    }
}
