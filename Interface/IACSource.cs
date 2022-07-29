using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKCommunicationNET.Interface
{
    public interface IACSource
    {
        public bool IsACSConnected { get; }
        void StarACVoltage(float amplitude);
        void StopACVoltage();
    }
}
