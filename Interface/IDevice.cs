using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Interface
{
    internal interface IDevice
    {
        public  ProtocolTypes ProtocolType { get; }
        public bool IsACSModuleSupported { get; }
        public bool IsDCSModuleSupported { get;  }
        public bool IsDCMModuleSupported { get;  }
        public bool IsIOModuleSupported { get; }
        public bool IsPQModuleSupported { get; }

    }
}
