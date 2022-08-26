using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using System. Text;

namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议解码器
/// </summary>
internal class Hex81Decoder : IDecoder
{
    private readonly IByteTransform _byteTransform;
    public Hex81Decoder ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 【属性】

    public int Offset => Hex81Information. DataStartIndex;

    public string? Model { get; set; }

    public string? SN { get; set; }

    public string? Firmware { get; private set; }

    public string? ProtocolVer => string. Empty;

    public bool IsEnabled_ACS { get; private set; }

    public bool IsEnabled_ACM { get; private set; }

    public bool IsEnabled_DCS { get; private set; }

    public bool IsEnabled_IO { get; private set; }

    public bool IsEnabled_EPQ { get; private set; }

    public bool IsEnabled_DCM { get; private set; }

    public bool IsEnabled_DualFreqs { get; private set; }

    public bool IsEnabled_IProtect { get; private set; }

    public bool IsEnabled_PST { get; private set; }

    public bool IsEnabled_YX { get; private set; }

    public bool IsEnabled_HF { get; private set; }

    public bool IsEnabled_PWM { get; private set; }

    public bool IsEnabled_ACM_Cap { get; private set; }

    public bool IsEnabled_DCS_AUX { get; private set; }

    public bool IsEnabled_DCM_RIP { get; private set; }

    public bool IsEnabled_PPS { get; private set; }

    public byte URanges_Count { get; private set; }

    public byte IRanges_Count { get; private set; }

    public byte URange_CurrentIndex { get; set; }

    public float URange_CurrentValue { get; private set; }

    public float IRange_CurrentValue { get; private set; }

    public float IProtectRange_CurrentValue { get; private set; }

    public byte IProtectRanges_Count { get; private set; }

    public byte URangeStartIndex_Asingle { get; private set; }

    public byte IRangeStartIndex_Asingle { get; private set; }

    public byte IProtectStartIndex_Asingle { get; private set; }

    public float[ ]? URanges { get; set; }
    public float[ ]? IRanges { get; set; }
    public float[ ]? IProtectRanges { get; set; }
    public Enum WireMode { get; set; }
    public Enum CloseLoopMode { get; set; }
    public Enum HarmonicMode { get; set; }
    public float Freq { get; set; }
    public float Freq_C { get; set; }
    public byte HarmonicCount { get; set; }
    public Enum HarmonicChannels { get; set; }
    public HarmonicArgs[ ] Harmonics { get; set; }
    public float UA { get; set; }
    public float UB { get; set; }
    public float UC { get; set; }
    public float IA { get; set; }
    public float IB { get; set; }
    public float IC { get; set; }
    public float IPA { get; set; }
    public float IPB { get; set; }
    public float IPC { get; set; }
    public float FAI_UA { get; set; }
    public float FAI_UB { get; set; }
    public float FAI_UC { get; set; }
    public float FAI_IA { get; set; }
    public float FAI_IB { get; set; }
    public float FAI_IC { get; set; }
    public float PA { get; set; }
    public float PB { get; set; }
    public float PC { get; set; }
    public float P { get; set; }
    public float QA { get; set; }
    public float QB { get; set; }
    public float QC { get; set; }
    public float Q { get; set; }

    public float SA { get; private set; }

    public float SB { get; private set; }

    public float SC { get; private set; }

    public float S { get; private set; }

    public float PFA { get; private set; }

    public float PFB { get; private set; }

    public float PFC { get; private set; }

    public float PF { get; private set; }

    public byte Flag_A { get; private set; }

    public byte Flag_B { get; private set; }

    public byte Flag_C { get; private set; }
    public byte IRange_CurrentIndex { get; set; }
    public byte IProtectRange_CurrentIndex { get; set; }
    #endregion 属性

    #region 【Decoders】

    public void DecodeHandShake ( OperateResult<byte[ ]> response )
    {
        if ( !response. IsSuccess || response. Content == null )
        {
            return;
        }

        //下位机回复的原始报文
        byte[ ] buffer = response. Content;

        //将缓存数据转换成List方便查找字符串的结束标志:0x00
        List<byte> bufferList = buffer. ToList ( );    //可忽略null异常

        //获取设备型号结束符的索引值
        int endIndex = bufferList. IndexOf ( 0x00 , Offset );

        //计算model字节长度，包含0x00结束符，5=报文头的字节数6再减去1
        int modelLength = endIndex - Offset + 1;
        //解析的设备型号
        Model = _byteTransform. TransString ( buffer , Offset , modelLength , Encoding. ASCII );

        //解析下位机版本号
        byte verA = buffer[modelLength + Offset];
        byte verB = buffer[modelLength + Offset + 1];
        //下位机版本号
        Firmware = $"V{verA}.{verB}";

        //解析设备编号
        int serialEndIndex = bufferList. IndexOf ( 0x00 , Offset + modelLength + 2 );
        int serialLength = serialEndIndex - 7 - modelLength;
        //设备编号字节长度，包含0x00结束符            
        SN = _byteTransform. TransString ( buffer , Offset + modelLength + 2 , serialLength , Encoding. ASCII );

        //基本功能激活状态
        byte FuncB = buffer[^3];
        bool[ ] funcB = SoftBasic. ByteToBoolArray ( FuncB );
        IsEnabled_ACS = funcB[0];
        IsEnabled_ACM = funcB[1];
        IsEnabled_DCS = funcB[2];
        IsEnabled_DCM = funcB[3];
        IsEnabled_EPQ = funcB[4];

        //特殊功能激活状态
        byte FuncS = buffer[^2];
        bool[ ] funcS = SoftBasic. ByteToBoolArray ( FuncS );
        IsEnabled_DualFreqs = funcS[0];
        IsEnabled_IProtect = funcS[1];
        IsEnabled_PST = funcS[2];
        IsEnabled_YX = funcS[3];
        IsEnabled_HF = funcS[4];
        IsEnabled_PWM = funcS[5];
    }

    /// <summary>
    /// 【解码】读取交流源档位信息
    /// </summary>
    /// <param name="response"></param>
    /// <returns>
    /// <list type="bullet">  
    ///     <item>T1:电压档位数量</item>
    ///     <item>T2:单相电压档位起始档位索引值</item>
    ///     <item>T3:电流档位数量</item>
    ///     <item>T4:单相电流档位起始档位索引值</item>
    ///     <item>T5:保护电流档位数量</item>
    ///     <item>T6:单相保护电流档位起始档位索引值</item>
    ///     <item>T7:电压档位集合</item>
    ///     <item>T8:电流档位集合</item>
    ///     <item>T9:保护电流档位集合</item>
    /// </list>
    /// </returns>
    public OperateResult DecodeGetRanges_ACS ( OperateResult<byte[ ]> response )
    {

        if ( response. IsSuccess && response. Content != null )
        {
            //下位机回复的经验证的有效报文
            byte[ ] responseBytes = response. Content;

            //电压档位数量
            URanges_Count = responseBytes[6];

            //单相电压档位起始档位索引值
            URangeStartIndex_Asingle = responseBytes[7];

            //电流档位数量
            IRanges_Count = responseBytes[8];

            //单相电流档位起始档位索引值
            IRangeStartIndex_Asingle = responseBytes[9];

            //保护电流档位数量
            IProtectRanges_Count = responseBytes[10];

            //单相保护电流档位起始档位索引值
            IProtectRange_CurrentIndex = responseBytes[11];

            //电压档位集合
            URanges = _byteTransform. TransSingle ( responseBytes , 12 , URanges_Count );

            //电流档位集合
            IRanges = _byteTransform. TransSingle ( responseBytes , 12 + 4 * URanges_Count , IRanges_Count );

            //保护电流档位集合
            IProtectRanges = _byteTransform. TransSingle ( responseBytes , 12 + 4 * URanges_Count + 4 * IRanges_Count , IProtectRanges_Count );
            return OperateResult. CreateSuccessResult ( );
        }
        return new OperateResult ( response. Message );
    }

    public OperateResult DecodeReadData_ACS ( OperateResult<byte[ ]> responsResult )
    {
        if ( !responsResult. IsSuccess || responsResult. Content == null )
        {
            return new OperateResult ( responsResult. Message );
        }
        byte[ ] responseBytes = responsResult. Content;
        Freq = _byteTransform. TransSingle ( responseBytes , Offset );
        URange_CurrentIndex = responseBytes[7];
        IRange_CurrentIndex = responseBytes[10];
        UA = _byteTransform. TransSingle ( responseBytes , 16 );
        UB = _byteTransform. TransSingle ( responseBytes , 20 );
        UC = _byteTransform. TransSingle ( responseBytes , 24 );
        IA = _byteTransform. TransSingle ( responseBytes , 28 );
        IB = _byteTransform. TransSingle ( responseBytes , 32 );
        IC = _byteTransform. TransSingle ( responseBytes , 36 );
        FAI_UA = _byteTransform. TransSingle ( responseBytes , 40 );
        FAI_UB = _byteTransform. TransSingle ( responseBytes , 44 );
        FAI_UC = _byteTransform. TransSingle ( responseBytes , 48 );
        FAI_IA = _byteTransform. TransSingle ( responseBytes , 52 );
        FAI_IB = _byteTransform. TransSingle ( responseBytes , 56 );
        FAI_IC = _byteTransform. TransSingle ( responseBytes , 60 );
        PA = _byteTransform. TransSingle ( responseBytes , 64 );
        PB = _byteTransform. TransSingle ( responseBytes , 68 );
        PC = _byteTransform. TransSingle ( responseBytes , 72 );
        P = _byteTransform. TransSingle ( responseBytes , 76 );
        QA = _byteTransform. TransSingle ( responseBytes , 80 );
        QB = _byteTransform. TransSingle ( responseBytes , 84 );
        QC = _byteTransform. TransSingle ( responseBytes , 88 );
        Q = _byteTransform. TransSingle ( responseBytes , 92 );
        SA = _byteTransform. TransSingle ( responseBytes , 96 );
        SB = _byteTransform. TransSingle ( responseBytes , 100 );
        SC = _byteTransform. TransSingle ( responseBytes , 104 );
        S = _byteTransform. TransSingle ( responseBytes , 108 );
        PFA = _byteTransform. TransSingle ( responseBytes , 112 );
        PFB = _byteTransform. TransSingle ( responseBytes , 116 );
        PFC = _byteTransform. TransSingle ( responseBytes , 120 );
        PF = _byteTransform. TransSingle ( responseBytes , 124 );
        WireMode = ( WireMode ) responseBytes[128];
        CloseLoopMode = ( CloseLoopMode ) responseBytes[129];
        HarmonicMode = ( HarmonicMode ) responseBytes[130];
        return OperateResult. CreateSuccessResult ( );
    }


    #endregion Decoders

}
