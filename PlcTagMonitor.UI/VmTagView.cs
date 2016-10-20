using Logix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PlcTagMonitor.UI
{
    public class VmTagView : INotifyPropertyChanged
    {
        private string _plcIP;
        private List<TagTemplate> _tags;
        private string _notification;

        public ICommand CmdLoadTags { get; internal set; }

        public string PlcIP
        {
            get
            {
                return _plcIP;
            }
            set
            {
                if (value != this._plcIP)
                {
                    this._plcIP = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<TagTemplate> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                if (value != this._tags)
                {
                    this._tags = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Notification
        {
            get
            {
                return _notification;
            }
            set
            {
                if (value != this._notification)
                {
                    this._notification = value;
                    NotifyPropertyChanged();
                }
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
