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
    public IPacketOfACM GetPacketsOfACM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketsOfACS GetPacketsOfACS ( )
    {
        return new Hex5APacketOfACS ( );
    }

    public IPacketOfDCM GetPacketsOfDCM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketOfDCS GetPacketsOfDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketOfIO GetPacketsOfIO ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketOfPQ GetPacketsOfPQ ( )
    {
        throw new NotImplementedException ( );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex5ACRCChecker ( );
    }
}
