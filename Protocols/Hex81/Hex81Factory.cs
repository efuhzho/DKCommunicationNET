using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols. Hex81. Decoders;
using DKCommunicationNET. Protocols. Hex81. Encoders;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议工厂类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81Factory : IProtocolFactory
{
    #region 《编码器

    public OperateResult<IEncoder_ACS> GetEncoderOfACS ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_ACS ( id , byteTransform ) as IEncoder_ACS );
    }

    public OperateResult<IEncoder_DCM> GetEncoderOfDCM ( ushort id )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_DCM ( id ) as IEncoder_DCM );
    }

    public OperateResult<IEncoder_DCS> GetEncoderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_DCS ( id , byteTransform ) as IEncoder_DCS );
    }

    public OperateResult<IEncoder_EPQ> GetEncoderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_EPQ ( id , byteTransform ) as IEncoder_EPQ );
    }

    public OperateResult<IEncoder_ACM> GetEncoderOfACM ( ushort id )
    {
        //不具备此功能模块
        return new OperateResult<IEncoder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IEncoder_IO> GetEncoderOfIO ( ushort id , IByteTransform byteTransform )
    {
        //不具备此功能模块
        return new OperateResult<IEncoder_IO> ( StringResources. Language. NotSupportedModule );
    }

    public IEncoder_Settings GetEncoder_Settings ( ushort id , IByteTransform byteTransform )
    {
        return new Hex81Encoder_Settings ( id );
    }

    public OperateResult<IEncoder_Calibrate> GetEncoderOfCalibrate ( ushort id , IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Encoder_Calibrate ( id , byteTransform ) as IEncoder_Calibrate );
    }

    public OperateResult<IEncoder_PPS> GetEncoder_PPS ( ushort id )
    {
        //不具备此功能模块
        return new OperateResult<IEncoder_PPS> ( StringResources. Language. NotSupportedModule );
    }
    #endregion 编码器》

    #region 《解码器

    public OperateResult<IDecoder_ACS> GetDecoder_ACS ( IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Decoder_ACS ( byteTransform ) as IDecoder_ACS );
    }


    public OperateResult<IDecoder_DCS> GetDecoder_DCS ( IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Decoder_DCS ( byteTransform ) as IDecoder_DCS );

    }

    public OperateResult<IDecoder_DCM> GetDecoder_DCM ( IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Decoder_DCM ( byteTransform ) as IDecoder_DCM );
    }

    public OperateResult<IDecoder_EPQ> GetDecoder_EPQ ( IByteTransform byteTransform )
    {
        return OperateResult. CreateSuccessResult ( new Hex81Decoder_EPQ ( byteTransform ) as IDecoder_EPQ );
    }

    public IDecoder_Settings GetDecoder_Settings ( IByteTransform byteTransform )
    {
        return new Hex81Decoder_Settings ( byteTransform );
    }

    public OperateResult<IDecoder_ACM> GetDecoder_ACM ( IByteTransform byteTransform )
    {
        //不具备此功能模块
        return new OperateResult<IDecoder_ACM> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IDecoder_PPS> GetDecoder_PPS ( )
    {
        //不具备此功能模块
        return new OperateResult<IDecoder_PPS> ( StringResources. Language. NotSupportedModule );
    }

    public OperateResult<IDecoder_IO> GetDecoder_IO ( IByteTransform byteTransform )
    {
        //不具备此功能模块
        return new OperateResult<IDecoder_IO> ( StringResources. Language. NotSupportedModule );
    }
    #endregion 解码器》

    #region 《校验器
    public ICRCChecker GetCRCChecker ( )
    {
        return new Hex81CRCChecker ( );
    }

    #endregion 校验器》

}
