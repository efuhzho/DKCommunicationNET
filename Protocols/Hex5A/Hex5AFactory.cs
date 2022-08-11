using DKCommunicationNET. Module;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A;

[Model ( Models. Hex5A )]
internal class Hex5AFactory : IProtocolFactory
{
    public IPacketBuilderOfACM GetPacketsOfACM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketsBuilderOfACS GetPacketsOfACS ( )
    {
        return new Hex5APacketBuilderOfACS ( );
    }

    public IPacketBuilderOfDCM GetPacketsOfDCM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketBuilderOfDCS GetPacketsOfDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketBuilderOfIO GetPacketsOfIO ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketBuilderOfPQ GetPacketsOfPQ ( )
    {
        throw new NotImplementedException ( );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }

    public IProtocolFunctionsState GetProtocolFunctionsState ( )
    {
        return new Hex5AFunctionsState ( );
    }
}
