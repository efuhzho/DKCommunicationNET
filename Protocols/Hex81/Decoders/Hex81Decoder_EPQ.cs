using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Decoders;

/// <summary>
/// 电能模块解码器
/// </summary>
internal class Hex81Decoder_EPQ : IDecoder_EPQ
{
    IByteTransform _byteTransform;
    internal Hex81Decoder_EPQ ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }
    /// <inheritdoc/>
    public uint Rounds_Current { get; private set; }
    /// <inheritdoc/>

    public uint Counts_Current { get; private set; }
    /// <inheritdoc/>

    public float EValue_P { get; private set; }
    /// <inheritdoc/>

    public float EValue_Q { get; private set; }

    OperateResult IDecoder_EPQ.DecodeReadData_EPQ ( byte[ ] response )
    {
        try
        {
            DecodeEPQ_Flag decodeEPQ_Flag = ( DecodeEPQ_Flag ) response[6];

            switch ( decodeEPQ_Flag )
            {
                case DecodeEPQ_Flag. Invalid:
                    Rounds_Current = _byteTransform. TransUInt32 ( response , 11 );
                    break;
                case DecodeEPQ_Flag. EValue_P:
                    EValue_P = _byteTransform. TransSingle ( response , 7 );
                    Rounds_Current = _byteTransform. TransUInt32 ( response , 11 );
                    break;
                case DecodeEPQ_Flag. EValue_Q:
                    EValue_Q = _byteTransform. TransSingle ( response , 7 );
                    Rounds_Current = _byteTransform. TransUInt32 ( response , 11 );
                    break;
                default:
                    return new OperateResult ( "（Hex81）DecodeReadData_EPQ:直流源回复数据解码失败：没能找到匹配的DecodeEPQ_Flag枚举项" );
            }
            //返回解析成功结果
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( ex. Message );
        }
    }
}
