using Logix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace PlcTagMonitor.Repository
{
    public class Tags
    {
        private const int TIMEOUT = 300;
        private Controller plc;

        public Tags(IPAddress PlcIP)
        {
            if (PlcIP == null)
            {
                throw new ArgumentNullException("PLC IP cannot be null");
            }
            var plc = new Controller()
            {
                CPUType = Controller.CPU.LOGIX,
                IPAddress = PlcIP.ToString(),
                Timeout = TIMEOUT,
            };
            if(plc.Connect() != ResultCode.E_SUCCESS)
            {
                throw new PlcNotAccessibleException();
            }
            this.plc = plc;
        }

        public List<TagTemplate> GetAll()
        {
            if (plc.UploadTags() != ResultCode.E_SUCCESS)
            {
                throw new Exception(plc.ErrorString);
            }
            return plc.ProgramList.SelectMany((p) => p.TagItems()).ToList();
        }
        
        public List<TagTemplate> GetAllAtomic()
        {
            return GetAll().Where((t) => t.IsAtomic).ToList();
        }
    }
}