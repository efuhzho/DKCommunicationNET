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
        IPacketsBuilderOfACS GetPacketsOfACS ( );
        IPacketBuilderOfACM GetPacketsOfACM ( );
        IPacketBuilderOfDCS GetPacketsOfDCS ( );
        IPacketBuilderOfDCM GetPacketsOfDCM ( );
        IPacketBuilderOfIO GetPacketsOfIO ( );
        IPacketBuilderOfPQ GetPacketsOfPQ ( );
        ICRCChecker GetCRCChecker ( );
        IProtocolFunctionsState GetProtocolFunctionsState ( );
    }
}
