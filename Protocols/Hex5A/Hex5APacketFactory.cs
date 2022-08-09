using DKCommunicationNET. Module;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A;

[Model ( Models. Hex5A )]
internal class Hex5APacketFactory : IPacketFactory
{
    public IPacketsOfACS GetPacketsOfACS ( )
    {
        return new Hex5APacketOfACS ( );
    }

    public IModuleDCM GetPacketsOfDCM ( )
    {
        throw new NotImplementedException ( );
    }

    public IModuleDCS GetPacketsOfDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public IModuleIO GetPacketsOfIO ( )
    {
        throw new NotImplementedException ( );
    }

    public IModulePQ GetPacketsOfPQ ( )
    {
        throw new NotImplementedException ( );
    }
}
