using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET
{
    internal interface IPacketFactory
    {
        IPacketsOfACS GetPacketsOfACS ( );
        IModuleDCM GetPacketsOfDCM ( );
        IModuleDCS GetPacketsOfDCS ( );
        IModuleIO GetPacketsOfIO ( );
        IModulePQ GetPacketsOfPQ ( );
    }
}
