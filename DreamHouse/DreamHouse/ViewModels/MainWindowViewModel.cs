using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input; 
using DreamHouse.Command;
using DreamHouse.Models;
using System.Collections.ObjectModel;
using DreamHouse.DataReader; 

namespace DreamHouse.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Dictionary<int, string> monthsByNumber = new Dictionary<int, string>()
        {
            { 1,"января"}, { 2,"февраля"}, { 3,"марта"}, { 4,"апреля"},
            { 5,"мая"}, { 6,"июня"}, { 7,"июля"}, { 8,"августа"},
            { 9,"сентября"}, { 10,"октября"}, { 11,"ноября"}, { 12,"декабря"},
        };
        private DispatcherTimer clockTimer = new DispatcherTimer();
        private string _DateString;
        public string DateString
        {
            get { return _DateString; }
            set
            {
                _DateString = value;
                DoPropertyChanged("DateString");
            }
        }
        private string _TimeString;
        public string TimeString
        {
            get { return _TimeString; }
            set
            {
                _TimeString = value;
                DoPropertyChanged("TimeString");
            }
        }

        public MainWindowViewModel()
        {
            GetDateTime();
            clockTimer.Interval = new TimeSpan(0, 0, 60 - DateTime.Now.Second);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            clockTimer.Interval = new TimeSpan(0, 1, 0);
            GetDateTime();
        }

        private void GetDateTime()
        {
            var time = DateTime.Now;
            DateString = $"{time.Day} {monthsByNumber[time.Month]}";
            TimeString = $"{time.Hour:d2}:{time.Minute:d2}";
        }
    }
}
