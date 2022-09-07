using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Decoders;

/// <summary>
/// 直流源解码器
/// </summary>
public class Hex81Decoder_DCS : IDecoder_DCS
{
    private readonly IByteTransform _byteTransform;
    internal Hex81Decoder_DCS ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    OperateResult IDecoder_DCS.DecodeReadData_DCS ( byte[ ] response )
    {
        OutputType_DCS outputType_DCS = ( OutputType_DCS ) response[11];

        switch ( outputType_DCS )
        {
            case OutputType_DCS. DCS_Type_U:
                RangeIndex_DCU = response[6];
                DCU = _byteTransform. TransSingle ( response , 7 );
                IsOpen_DCU = _byteTransform. TransBool ( response , 12 );
                break;
            case OutputType_DCS. DCS_Type_I:
                RangeIndex_DCI = response[6];
                DCI = _byteTransform. TransSingle ( response , 7 );
                IsOpen_DCI = _byteTransform. TransBool ( response , 12 );
                break;
            case OutputType_DCS. DCS_Type_R:
                RangeIndex_DCR = response[6];
                DCR = _byteTransform. TransSingle ( response , 7 );
                IsOpen_DCR = _byteTransform. TransBool ( response , 12 );
                break;
            default:
                return new OperateResult ( StringResources. GetLineNum ( ) , StringResources. Language. DecodeError + StringResources. GetCurSourceFileName ( ) );
        }

        return OperateResult. CreateSuccessResult ( );
    }

    OperateResult IDecoder_DCS.DecodeGetRanges_DCS ( byte[ ] response )
    {

        RangesCount_DCU = response[6];
        RangesCount_DCI = response[7];
        Ranges_DCU = _byteTransform. TransSingle ( response , 8 , RangesCount_DCU );
        Ranges_DCI = _byteTransform. TransSingle ( response , 8 + RangesCount_DCU * 4 , RangesCount_DCI );

        return OperateResult. CreateSuccessResult ( );
    }

    #region 《档位数量

    /// <summary>
    /// 直流源电压档位个数
    /// </summary>
    public byte RangesCount_DCU { get; private set; }

    /// <summary>
    /// 直流源电流档位个数
    /// </summary>
    public byte RangesCount_DCI { get; private set; }

    /// <summary>
    /// 直流源电阻档位个数
    /// </summary>
    public byte RangesCount_DCR { get; private set; }

    #endregion 档位数量》

    #region 《幅值
    /// <summary>
    /// 当前直流源电压幅值
    /// </summary>
    public float DCU { get; private set; }

    /// <summary>
    /// 当前直流源电流幅值
    /// </summary>
    public float DCI { get; private set; }

    /// <summary>
    /// 当前直流电阻幅值
    /// </summary>
    public float DCR { get; private set; }

    #endregion 幅值》

    #region 《档位索引

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    public byte RangeIndex_DCU { get; private set; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    public byte RangeIndex_DCI { get; private set; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    public byte RangeIndex_DCR { get; private set; }

    #endregion 档位索引》

    #region 《档位列表

    /// <summary>
    /// 直流源电压档位列表
    /// </summary>
    public float[ ]? Ranges_DCU { get; set; }

    /// <summary>
    /// 直流源电流档位列表
    /// </summary>
    public float[ ]? Ranges_DCI { get; set; }

    /// <summary>
    /// 直流源电阻档位列表
    /// </summary>
    public float[ ]? Ranges_DCR { get; set; }

    #endregion 档位列表》

    #region 《输出状态

    /// <summary>
    /// 当前直流电压输出状态：true=源打开；false=源关闭
    /// </summary>
    public bool IsOpen_DCU { get; private set; }

    /// <summary>
    /// 当前直流电流输出状态：true=源打开；false=源关闭
    /// </summary>
    public bool IsOpen_DCI { get; private set; }

    /// <summary>
    /// 当前直流电阻输出状态：true=源打开；false=源关闭
    /// </summary>
    public bool IsOpen_DCR { get; private set; }

    #endregion 输出状态》
}
