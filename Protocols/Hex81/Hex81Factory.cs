using DKCommunicationNET. Core;
using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public OperateResult<IPacketBuilder_ACM> GetPacketsOfACM ( ushort id )
    {
        return new OperateResult<IPacketBuilder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketsBuilder_ACS> GetPacketsOfACS ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilderOfACS (  id ) as IPacketsBuilder_ACS );

    }

    public OperateResult<IPacketBuilder_DCM> GetPacketsOfDCM ( ushort id )
    {
        return new OperateResult<IPacketBuilder_DCM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilder_DCS> GetPacketsOfDCS ( ushort id )
    {
        return new OperateResult<IPacketBuilder_DCS> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilder_IO> GetPacketsOfIO ( ushort id )
    {
        return new OperateResult<IPacketBuilder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilder_PQ> GetPacketsOfPQ ( ushort id )
    {
        return new OperateResult<IPacketBuilder_PQ> ( StringResources. Language. NotSupportedModule );
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
