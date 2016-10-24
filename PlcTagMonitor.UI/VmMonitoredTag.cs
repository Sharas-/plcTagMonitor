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
        private ulong X;
        private ulong _xoffset;

        //private DateTime _tstamp;
        internal Tag TheTag { get; }

        public VmMonitoredTag(Tag t)
        {
            TheTag = t;
            Name = t.Name;
            _values = new ObservableCollection<Point>();
        }

        public ulong XOffset
        {
            get
            {
                return _xoffset;
            }
            set
            {
                if (value != this._xoffset)
                {
                    this._xoffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
            int MaxX = 100;
            if (Values.Count >= MaxX)
            {
                XOffset = this.X - (ulong)MaxX;
                Values.RemoveAt(0);
            }
            Values.Add(new Point(this.X++, value));
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
