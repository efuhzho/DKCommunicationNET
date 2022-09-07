using DKCommunicationNET. Core;
using DKCommunicationNET. Module;


namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    public OperateResult<IEncoder_ACM> GetPacketBuilderOfACM ( ushort id )
    {
        return new OperateResult<IEncoder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_ACS> GetPacketBuilderOfACS ( ushort id , IByteTransform byteTransform )
    {
        // return new OperateResult<IEncoder_ACS> ( StringResources.Language.NotSupportedModule);

        // TODO

        return OperateResult. CreateSuccessResult ( new Hex81Encoder_ACS ( id , byteTransform ) as IEncoder_ACS );
    }

    public OperateResult<IEncoder> GetPacketBuilderOfDCM ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_DCM ( id ) as IEncoder );
    }

    public OperateResult<IEncoder_DCS> GetPacketBuilderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_DCS ( id , byteTransform ) as IEncoder_DCS );
    }

    public OperateResult<IEncoder_IO> GetPacketBuilderOfIO ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_EPQ> GetPacketBuilderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_EPQ ( id , byteTransform ) as IEncoder_EPQ );
    }

    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex81CRCChecker ( );
    }

    public IProtocolFunctions GetProtocolFunctions ( )
    {
        return new Hex81Functions ( );
    }

    public IDecoders GetDecoder ( IByteTransform byteTransform )
    {
        return new Hex81Decoder ( byteTransform );
    }

   
    public OperateResult<IEncoder_Calibrate> GetPacketBuilderOfCalibrate ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_Calibrate ( id , byteTransform ) as IEncoder_Calibrate );
    }

   
    public IDecoder_ACS GetDecoder_ACS ( IByteTransform byteTransform )
    {
        return new Hex81Decoder_ACS ( byteTransform );
    }

   
    public IDecoder_DCS GetDecoder_DCS ( IByteTransform byteTransform )
    {
        return new Hex81Decoder_DCS ( byteTransform );
    }
}
