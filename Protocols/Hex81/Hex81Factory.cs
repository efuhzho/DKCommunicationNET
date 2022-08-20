using DKCommunicationNET. Core;
using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public OperateResult<IPacketBuilderOfACM> GetPacketsOfACM ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketsBuilderOfACS> GetPacketsOfACS ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilderOfACS (  id ) as IPacketsBuilderOfACS );

    }

    public OperateResult<IPacketBuilderOfDCM> GetPacketsOfDCM ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfDCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfDCS> GetPacketsOfDCS ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfDCS> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfIO> GetPacketsOfIO ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfIO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilderOfPQ> GetPacketsOfPQ ( ushort id )
    {
        return new OperateResult<IPacketBuilderOfPQ> ( StringResources. Language. NotSupportedModule );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex81CRCChecker ( );
    }

    public IProtocolFunctions GetProtocolFunctions ( )
    {
        return new Hex81Functions ( );
    }

    public IDecoder GetDecoder (IByteTransform byteTransform )
    {
        return new Hex81Decoder ( byteTransform );
    }
}
