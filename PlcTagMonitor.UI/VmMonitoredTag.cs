using Logix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagMonitor.UI
{
    public class VmMonitoredTag: INotifyPropertyChanged 
    {
        private float _value;
        private string _name;
        private DateTime _tstamp;
        internal Tag TheTag { get; }

        public VmMonitoredTag(Tag t)
        {
            TheTag = t;
            Name = t.Name;
        }

        public DateTime TimeStamp
        {
            get
            {
                return _tstamp;
            }
            set
            {
                if (value != this._tstamp)
                {
                    this._tstamp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != this._value)
                {
                    this._value = value;
                    NotifyPropertyChanged();
                }
            }
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
