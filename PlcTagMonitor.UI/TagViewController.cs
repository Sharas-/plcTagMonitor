using PlcTagMonitor.Repository;
using System;
using System.Net;
using System.Windows.Input;

namespace PlcTagMonitor.UI
{
    class TagViewController
    {
        public class DelegateCommand : ICommand
        {
            private readonly Action _action;
            public DelegateCommand(Action action)
            {
                _action = action;
            }

            public void Execute(object parameter)
            {
                _action();
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }


        public TagViewController()
        {
            this.State = new VmTagView()
            {
                PlcIP = "192.168.10.1",
                Notification = "Ready",
                CmdLoadTags = new DelegateCommand(() => LoadTags()),
            };

        }

        private void LoadTags()
        {
            try
            {
                State.Tags = new Tags(IPAddress.Parse(State.PlcIP)).GetAllAtomic();
            }
            catch (Exception e)
            {
                State.Notification = e.Message;
            }
        }

        public void AddMonitorTag(string tag)
        {
            try
            {
                var a = 10; 
            }
            catch (Exception e)
            {
                State.Notification = e.Message;
            }
        }
        
        public void RemoveMonitorTag(string tag)
        {
            try
            {
                var b = 1;
            }
            catch (Exception e)
            {
                State.Notification = e.Message;
            }
        }
        public VmTagView State { get; private set; }

    }
}
