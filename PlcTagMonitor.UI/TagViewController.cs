using Logix;
using System;
using System.Linq;
using System.ComponentModel;
using System.Net;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PlcTagMonitor.UI
{
    class TagViewController
    {
        private const int TIMEOUT = 300;
        public int SCAN_INTERVAL = 200;
        private Controller plc;
        private TagGroup tagGroup;

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
                CmdStartMonitoring = new DelegateCommand(() => tagGroup.ScanStart(SCAN_INTERVAL)),
                CmdStopMonitoring = new DelegateCommand(() => tagGroup.ScanStop()),
            };
        }

        public VmTagView State { get; private set; }

        private void LoadTags()
        {
            try
            {
                plc = new Controller(Controller.CPU.LOGIX, IPAddress.Parse(State.PlcIP).ToString())
                {
                    Timeout = TIMEOUT
                };
                if (plc.Connect() != ResultCode.E_SUCCESS || plc.UploadTags() != ResultCode.E_SUCCESS)
                {
                    throw new Exception(plc.ErrorString);
                }
                this.tagGroup = new TagGroup(plc);
                State.Tags = LoadAllScalar();
                State.Connected = true;
            }
            catch (Exception e)
            {
                State.Notification = e.Message;
            }
        }

        public List<TagTemplate> LoadAllScalar()
        {
            var grpName = "SimulationTests";
            var program = plc.ProgramList.Where((p) => p.Name == grpName).FirstOrDefault();
            if (program != null)
            {
                return program.TagItems().ToList();
            }
            State.Notification = $"Cannot find group {grpName}";
            return plc.ProgramList.SelectMany((p) => p.TagItems()).Where((t) => t.IsAtomic).ToList();
        }

        public void AddMonitorTag(string tagName)
        {
            try
            {
                var tag = new Tag(tagName);
                var vmTag = new VmMonitoredTag(tag);
                tag.Changed += (sender, args) => OnTagChanged(args, vmTag);
                this.tagGroup.AddTag(tag);
                State.MonitoredTags.Add(vmTag);
            }
            catch (Exception e)
            {
                State.Notification = e.Message;
            }
        }

        private void OnTagChanged(EventArgs eargs, VmMonitoredTag vmTag)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                var args = (DataChangeEventArgs)eargs;
                try
                {
                    if (ResultCode.QUAL_GOOD == args.QualityCode)
                    {
                        vmTag.AddValue(double.Parse(args.Value.ToString()));
                        //vmTag.Value = float.Parse(args.Value.ToString());
                        //vmTag.TimeStamp = args.TimeStamp;
                    }
                    vmTag.Quality = args.QualityString;
                }
                catch (Exception e)
                {
                    State.Notification = e.Message;
                }
            });
        }

        public void RemoveMonitorTag(string tagName)
        {
            var vmTag = State.MonitoredTags.FirstOrDefault((t) => t.Name == tagName);
            if (vmTag != null)
            {
                State.MonitoredTags.Remove(vmTag);
                this.tagGroup.RemoveTag(vmTag.TheTag);
                vmTag.TheTag.Dispose();
            }
        }
    }
}
