using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET
{
    internal interface IProtocolFactory
    {
        IPacketsOfACS GetPacketsOfACS ( );
        IPacketOfACM GetPacketsOfACM ( );
        IPacketOfDCS GetPacketsOfDCS ( );
        IPacketOfDCM GetPacketsOfDCM ( );
        IPacketOfIO GetPacketsOfIO ( );
        IPacketOfPQ GetPacketsOfPQ ( );
        ICRCChecker GetCRCChecker ( );
    }
}
