using Logix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlcTagMonitor.UI
{
    public class VmMonitoredTag : INotifyPropertyChanged
    {
        private ObservableCollection<Point> _values;
        private string _name;
        private uint X;
        //private DateTime _tstamp;
        internal Tag TheTag { get; }

        public VmMonitoredTag(Tag t)
        {
            TheTag = t;
            Name = t.Name;
            _values = new ObservableCollection<Point>();
        }

        //public DateTime TimeStamp
        //{
        //    get
        //    {
        //        return _tstamp;
        //    }
        //    set
        //    {
        //        if (value != this._tstamp)
        //        {
        //            this._tstamp = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        public ObservableCollection<Point> Values
        {
            get
            {
                return _values;
            }
            set
            {
                if (value != this._values)
                {
                    this._values = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void AddValue(double value)
        {
            uint MaxX = 50;
            this.X %= MaxX;
            if (Values.Count >= MaxX)
            {
                Values.Clear();
            }
            Values.Add(new Point(this.X += 1, value));
        }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                if (value != this._name)
                {
                    this._name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Quality
        {
            set
            {
                this.Name = $"{TheTag.Name} ({value})";
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
