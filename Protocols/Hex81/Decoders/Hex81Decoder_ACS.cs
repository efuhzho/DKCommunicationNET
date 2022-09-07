using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols. Hex81. Decoders;

/// <summary>
/// 交流源解码器
/// </summary>
public class Hex81Decoder_ACS : IDecoder_ACS
{
    private readonly IByteTransform _byteTransform;

    internal Hex81Decoder_ACS ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 属性>>>交流源/表

    public byte RangesCount_ACU { get; private set; }

    public byte RangesCount_ACI { get; private set; }

    public byte RangeIndex_ACU { get; set; }

    public float RangeValue_ACU { get; private set; }

    public float RangeValue_ACI { get; private set; }

    public float RangeValue_IPr { get; private set; }

    public byte RangesCount_IPr { get; private set; }

    public byte OnlyAStartIndex_ACU { get; private set; }

    public byte OnlyAStartIndex_ACI { get; private set; }

    public byte OnlyAStartIndex_IPr { get; private set; }

    public float[ ]? Ranges_ACU { get; set; }
    public float[ ]? Ranges_ACI { get; set; }
    public float[ ]? Ranges_IPr { get; set; }
    public WireMode WireMode { get; set; } = WireMode. WireMode_3P4L;
    public CloseLoopMode CloseLoopMode { get; set; } = CloseLoopMode. CloseLoop;
    public HarmonicMode HarmonicMode { get; set; } = HarmonicMode. ValidValuesConstant;
    public float Freq { get; set; }
    public float Freq_C { get; set; }
    public byte HarmonicCount { get; set; }
    public Enum? HarmonicChannels { get; set; }
    public HarmonicArgs[ ]? Harmonics { get; set; }
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
    public byte RangeIndex_ACI { get; set; }
    public byte RangeIndex_IPr { get; set; }

    #endregion 属性>>>交流源/表

    OperateResult IDecoder_ACS.DecodeGetRanges_ACS ( byte[ ] responseBytes )
    {
        //电压档位数量
        RangesCount_ACU = responseBytes[Hex81Information. DataStartIndex];

        //单相电压档位起始档位索引值
        OnlyAStartIndex_ACU = responseBytes[7];

        //电流档位数量
        RangesCount_ACI = responseBytes[8];

        //单相电流档位起始档位索引值
        OnlyAStartIndex_ACI = responseBytes[9];

        //保护电流档位数量
        RangesCount_IPr = responseBytes[10];

        //单相保护电流档位起始档位索引值
        RangeIndex_IPr = responseBytes[11];

        //电压档位集合
        Ranges_ACU = _byteTransform. TransSingle ( responseBytes , 12 , RangesCount_ACU );

        //电流档位集合
        Ranges_ACI = _byteTransform. TransSingle ( responseBytes , 12 + 4 * RangesCount_ACU , RangesCount_ACI );

        //保护电流档位集合
        Ranges_IPr = _byteTransform. TransSingle ( responseBytes , 12 + 4 * RangesCount_ACU + 4 * RangesCount_ACI , RangesCount_IPr );
        return OperateResult. CreateSuccessResult ( );

    }

    OperateResult IDecoder_ACS.DecodeReadData_ACS ( byte[ ] responseBytes )
    {       
        Freq = _byteTransform. TransSingle ( responseBytes , Hex81Information. DataStartIndex );
        RangeIndex_ACU = responseBytes[Hex81Information. DataStartIndex + 4];  //取UA的档位索引
        RangeIndex_ACI = responseBytes[Hex81Information. DataStartIndex + 7];  //取IA的档位索引
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

    OperateResult IDecoder_ACS.DecodeReadData_Status_ACS ( byte[ ] response )
    {
        Flag_A = response[6];
        Flag_B = response[7];
        Flag_C = response[8];
        Freq = _byteTransform. TransSingle ( response , 9 );
        Freq_C = _byteTransform. TransSingle ( response , 17 );
        IPA = _byteTransform. TransSingle ( response , 21 );
        IPB = _byteTransform. TransSingle ( response , 25 );
        IPC = _byteTransform. TransSingle ( response , 29 );
        RangeValue_ACU = _byteTransform. TransSingle ( response , 33 );
        RangeValue_ACI = _byteTransform. TransSingle ( response , 37 );
        RangeValue_IPr = _byteTransform. TransSingle ( response , 41 );
        return OperateResult. CreateSuccessResult ( );
    }
}



