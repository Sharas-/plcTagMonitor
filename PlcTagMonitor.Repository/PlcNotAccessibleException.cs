using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagMonitor.Repository
{
    public class PlcNotAccessibleException:Exception
    {
        public PlcNotAccessibleException(): base("Cannot connect to controller") { }
      
    }
}
