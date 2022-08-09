using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

[Model ( Models. Hex81 )]
internal class Hex81PacketFactory : IPacketFactory
{
    public IPacketsOfACS GetPacketsOfACS ( )
    {
       return new Hex81PacketOfACS ( );
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
