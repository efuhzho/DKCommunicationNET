using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public IPacketBuilderOfACM GetPacketsOfACM ( )
    {
        throw new NotImplementedException ( );
    }

    public IPacketsBuilderOfACS GetPacketsOfACS ( )
    {
        return new Hex81PacketBuilderOfACS ( );        
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
        return new Hex81CRCChecker ( ) ;
    }

    public IProtocolFunctionsState GetProtocolFunctionsState ( )
    {
        return new Hex81FunctionsState ( );
    }
}
