using DKCommunicationNET. Core;
using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public OperateResult<IPacketBuilder_ACM> GetPacketBuilderOfACM ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilder_ACM(id) as IPacketBuilder_ACM );
    }

    public OperateResult<IPacketsBuilder_ACS> GetPacketBuilderOfACS ( ushort id ,IByteTransform byteTransform)
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilder_ACS (  id ,byteTransform) as IPacketsBuilder_ACS );
    }

    public OperateResult<IPacketBuilder_DCM> GetPacketBuilderOfDCM ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilder_DCM ( id  ) as IPacketBuilder_DCM );
    }

    public OperateResult<IPacketBuilder_DCS> GetPacketBuilderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilder_DCS ( id ,byteTransform) as IPacketBuilder_DCS );
    }

    public OperateResult<IPacketBuilder_IO> GetPacketBuilderOfIO ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IPacketBuilder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IPacketBuilder_EPQ> GetPacketBuilderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81PacketBuilder_EPQ ( id,byteTransform )  as IPacketBuilder_EPQ );
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
