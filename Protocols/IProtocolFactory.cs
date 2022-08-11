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
        OperateResult<IPacketsBuilderOfACS> GetPacketsOfACS ( );
        OperateResult<IPacketBuilderOfACM> GetPacketsOfACM ( );
        OperateResult<IPacketBuilderOfDCS> GetPacketsOfDCS ( );
        OperateResult<IPacketBuilderOfDCM> GetPacketsOfDCM ( );
        OperateResult<IPacketBuilderOfIO> GetPacketsOfIO ( );
        OperateResult<IPacketBuilderOfPQ> GetPacketsOfPQ ( );
        ICRCChecker GetCRCChecker ( );
        IProtocolFunctionsState GetProtocolFunctionsState ( );
    }
}
