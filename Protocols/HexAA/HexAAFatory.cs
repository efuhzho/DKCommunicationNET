using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols. HexAA. Decoders;
using DKCommunicationNET. Protocols. HexAA. Encoders;

namespace DKCommunicationNET. Protocols. HexAA;

/// <summary>
/// HexAA协议工厂类
/// </summary>
[Model ( Models. HexAA )]
internal class HexAAFatory : IProtocolFactory
{
    #region 《编码器
    public OperateResult<IEncoder_ACM> GetEncoderOfACM ( ushort id )
    {
        throw new NotImplementedException ( );
    }
    public OperateResult<IEncoder_DCM> GetEncoderOfDCM ( ushort id )
    {
        throw new NotImplementedException ( );
    }
    public OperateResult<IEncoder_EPQ> GetEncoderOfEPQ ( ushort id , IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
    public IEncoder_Settings GetEncoder_Settings ( ushort id , IByteTransform byteTransform )
    {
        return new HexAAEncoder_Settings ( );
    }
    #endregion 编码器》

    #region 《解码器
    public OperateResult<IDecoder_ACM> GetDecoder_ACM ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
    public OperateResult<IDecoder_DCM> GetDecoder_DCM ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
    public OperateResult<IDecoder_EPQ> GetDecoder_EPQ ( IByteTransform byteTransform )
    {
        throw new NotImplementedException ( );
    }
    public IDecoder_Settings GetDecoder_Settings ( IByteTransform byteTransform )
    {
        return new HexAADecoder_Settings ( byteTransform );
    }
    #endregion 解码器》

    #region 《校验器
    public ICRCChecker GetCRCChecker ( )
    {
        return new HexAACRCChecker ( );
    }
    #endregion 校验器》

    #region 《不支持的功能模块
    public OperateResult<IDecoder_ACS> GetDecoder_ACS ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_ACS> ( StringResources. Language. NotSupportedModule );
    }
    public OperateResult<IDecoder_DCS> GetDecoder_DCS ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_DCS> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IEncoder_ACS> GetEncoderOfACS ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_ACS> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IEncoder_Calibrate> GetEncoderOfCalibrate ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_Calibrate> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IEncoder_IO> GetEncoderOfIO ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_IO> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IEncoder_PPS> GetEncoder_PPS ( ushort id )
    {
        return new OperateResult<IEncoder_PPS> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IDecoder_IO> GetDecoder_IO ( IByteTransform byteTransform )
    {
        return new OperateResult<IDecoder_IO> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IDecoder_PPS> GetDecoder_PPS ( )
    {
        return new OperateResult<IDecoder_PPS> ( StringResources. Language. NotSupportedModule );

    }
    public OperateResult<IEncoder_DCS> GetEncoderOfDCS ( ushort id , IByteTransform byteTransform )
    {
        return new OperateResult<IEncoder_DCS> ( StringResources. Language. NotSupportedModule );

    }
    #endregion 不支持的功能模块》
}
