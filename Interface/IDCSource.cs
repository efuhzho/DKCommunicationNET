using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Interface
{
    public interface IDCSource
    {
        public bool IsDCSConnected { get;  }
        void StarDCVoltage();
        void StopDCVoltage();
    }
}
