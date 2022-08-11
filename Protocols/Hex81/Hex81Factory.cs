using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public IPacketOfACM GetPacketsOfACM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketsOfACS GetPacketsOfACS ( )
    {
        return new Hex81PacketOfACS ( );        
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
        return new Hex81CRCChecker ( ) ;
    }
}
