using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Decoders;

/// <summary>
/// 交流源解码器
/// </summary>
internal class Hex81Decoder_ACS : IDecoder_ACS
{
    private readonly IByteTransform _byteTransform;

    internal Hex81Decoder_ACS ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 《属性
    #region 《档位列表
    /// <summary>
    /// 电压档位集合
    /// </summary>
    public float[ ]? Ranges_ACU { get; private set; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    public float[ ]? Ranges_ACI { get; private set; }

    /// <summary>
    /// 保护电流档位集合
    /// </summary>
    public float[ ]? Ranges_IPr { get; private set; }
    #endregion 档位列表》

    #region 《档位数量
    /// <summary>
    /// 电压档位个数
    /// </summary>
    public byte RangesCount_ACU { get; private set; }

    /// <summary>
    /// 电流档位个数
    /// </summary>
    public byte RangesCount_ACI { get; private set; }

    /// <summary>
    /// 保护电流档位个数
    /// </summary>
    public byte RangesCount_IPr { get; private set; }

    #endregion 档位数量》

    #region 《当前档位索引
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ua { get; private set; }
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ub { get; private set; }
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Uc { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ux { get; private set; }

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ia { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ib { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ic { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ix { get; private set; }

    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPa { get; private set; }
    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPb { get; private set; }
    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPc { get; private set; }

    #endregion 当前档位索引》

    #region 《当前档位值
    /// <summary>
    /// 当前交流电压档位值，单位V
    /// </summary>
    public float RangeValue_ACU { get; private set; }

    /// <summary>
    /// 当前交流电流档位值，单位A
    /// </summary>
    public float RangeValue_ACI { get; private set; }

    /// <summary>
    /// 当前保护电流档位值，单位A
    /// </summary>
    public float RangeValue_IPr { get; private set; }

    #endregion 当前档位值》

    #region 《只支持A相的档位起始索引
    /// <summary>
    /// 只支持A相电压输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_ACU { get; private set; }

    /// <summary>
    /// 只支持A相电流输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_ACI { get; private set; }

    /// <summary>
    /// 只支持A相保护电流输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_IPr { get; private set; }
    #endregion 只支持A相的档位起始索引》

    #region 《频率幅值
    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    public float Freq { get; private set; }

    /// <summary>
    /// 【34B2适用】C相频率(支持双频输出时有效)
    /// </summary>
    public float Freq_C { get; private set; }

    /// <summary>
    /// 频率标志:同频/异频
    /// </summary>
    public string? FrequencySync { get; private set; }
    #endregion 频率幅值》

    #region 《电压幅值
    /// <summary>
    /// A相电压数据
    /// </summary>
    public float UA { get; private set; }

    /// <summary>
    /// B相电压数据
    /// </summary>
    public float UB { get; private set; }

    /// <summary>
    /// C相电压数据
    /// </summary>
    public float UC { get; private set; }
    /// <summary>
    /// X相电压数据
    /// </summary>
    public float UX { get; private set; }
    #endregion 电压幅值》

    #region 《电流幅值
    /// <summary>
    /// A相电流数据
    /// </summary>
    public float IA { get; private set; }
    /// <summary>
    /// B相电流数据
    /// </summary>
    public float IB { get; private set; }

    /// <summary>
    /// C相电流数据
    /// </summary>
    public float IC { get; private set; }
    /// <summary>
    /// C相电流数据
    /// </summary>
    public float IX { get; private set; }
    #endregion 电流幅值》

    #region 《保护电流幅值
    /// <summary>
    /// 【51F适用】A相保护电流数据
    /// </summary>
    public float IPA { get; private set; }

    /// <summary>
    /// 【51F适用】B相保护电流数据
    /// </summary>
    public float IPB { get; private set; }

    /// <summary>
    /// 【51F适用】C相保护电流数据
    /// </summary>
    public float IPC { get; private set; }
    #endregion 保护电流幅值》

    #region 《相位幅值
    /// <summary>
    /// A相电压相位数据
    /// </summary>
    public float FAI_UA { get; private set; }

    /// <summary>
    /// B相电压相位数据
    /// </summary>
    public float FAI_UB { get; private set; }

    /// <summary>
    /// C相电压相位数据
    /// </summary>
    public float FAI_UC { get; private set; }
    /// <summary>
    /// C相电压相位数据
    /// </summary>
    public float FAI_UX { get; private set; }

    /// <summary>
    /// A相电流相位数据
    /// </summary>
    public float FAI_IA { get; private set; }

    /// <summary>
    /// B相电流相位数据
    /// </summary>
    public float FAI_IB { get; private set; }

    /// <summary>
    /// C相电流相位数据
    /// </summary>
    public float FAI_IC { get; private set; }
    /// <summary>
    /// C相电流相位数据
    /// </summary>
    public float FAI_IX { get; private set; }
    #endregion 相位幅值》

    #region 《功率幅值
    /// <summary>
    /// A相有功功率数据
    /// </summary>
    public float PA { get; private set; }

    /// <summary>
    /// B相有功功率数据
    /// </summary>
    public float PB { get; private set; }

    /// <summary>
    /// C相有功功率数据
    /// </summary>
    public float PC { get; private set; }
    /// <summary>
    /// C相有功功率数据
    /// </summary>
    public float PX { get; private set; }

    /// <summary>
    /// 总有功功率数据
    /// </summary>
    public float P { get; private set; }

    /// <summary>
    /// A相无功功率数据
    /// </summary>
    public float QA { get; private set; }

    /// <summary>
    /// B相无功功率数据
    /// </summary>
    public float QB { get; private set; }

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    public float QC { get; private set; }

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    public float QX { get; private set; }

    /// <summary>
    /// 总无功功率数据
    /// </summary>    
    public float Q { get; private set; }

    /// <summary>
    /// A相视在功率，单位：VA
    /// </summary>
    public float SA { get; private set; }

    /// <summary>
    /// B相视在功率，单位：VA
    /// </summary>
    public float SB { get; private set; }

    /// <summary>
    /// C相视在功率，单位：VA
    /// </summary>
    public float SC { get; private set; }
    /// <summary>
    /// C相视在功率，单位：VA
    /// </summary>
    public float SX { get; private set; }

    /// <summary>
    /// 总实在功率
    /// </summary>
    public float S { get; private set; }
    #endregion 功率幅值》

    #region 《功率因数
    /// <summary>
    /// A相功率因数
    /// </summary>
    public float PFA { get; private set; }

    /// <summary>
    /// B相功率因数
    /// </summary>
    public float PFB { get; private set; }

    /// <summary>
    /// C相功率因数
    /// </summary>
    public float PFC { get; private set; }
    /// <summary>
    /// C相功率因数
    /// </summary>
    public float PFX { get; private set; }

    /// <summary>
    /// 总功率因数
    /// </summary>
    public float PF { get; private set; }
    #endregion 功率因数》

    #region 《谐波数据
    /// <summary>
    /// 当前输出的谐波个数
    /// </summary>
    public byte HarmonicCount { get; private set; }

    /// <summary>
    /// 当前谐波输出通道
    /// </summary>
    public Enum? HarmonicChannels { get; private set; }

    /// <summary>
    /// 当前谐波输出数据
    /// </summary>
    public HarmonicArgs[ ]? Harmonics { get; private set; }
    #endregion 谐波数据》

    #region 《枚举属性
    /// <summary>
    /// 当前接线模式
    /// </summary>
    public WireMode WireMode { get; set; }

    /// <summary>
    /// 当前闭环模式
    /// </summary>
    public CloseLoopMode CloseLoopMode { get; set; }

    /// <summary>
    /// 当前谐波模式
    /// </summary>
    public HarmonicMode HarmonicMode { get; set; }
    /// <summary>
    /// 无功计算方法
    /// </summary>
    public QP_Mode QP_Mode { get; set; }
    #endregion 枚举属性》

    #region 《输出状态
    /// <summary>
    /// A相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ua { get; private set; }

    /// <summary>
    /// B相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ub { get; private set; }

    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Uc { get; private set; }
    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ux { get; private set; }
    /// <summary>
    /// A相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ia { get; private set; }

    /// <summary>
    /// B相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ib { get; private set; }

    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ic { get; private set; }
    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ix { get; private set; }
    #endregion 输出状态》

    /// <inheritdoc/>
    public byte? OutputtingChannelsNum { get; private set; }
    /// <inheritdoc/>
    public byte? OutputChannelsNum { get; private set; }
    /// <inheritdoc/>
    public string? ACSMode { get; private set; }

    #endregion 属性》

    public OperateResult DecodeGetRanges_ACS ( byte[ ] responseBytes )
    {
        try
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
            RangeIndex_IPa = responseBytes[11];

            //电压档位集合
            Ranges_ACU = _byteTransform. TransSingle ( responseBytes , 12 , RangesCount_ACU );

            //电流档位集合
            Ranges_ACI = _byteTransform. TransSingle ( responseBytes , 12 + 4 * RangesCount_ACU , RangesCount_ACI );

            //保护电流档位集合
            Ranges_IPr = _byteTransform. TransSingle ( responseBytes , 12 + 4 * RangesCount_ACU + 4 * RangesCount_ACI , RangesCount_IPr );
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( StringResources. Language. DecodeError + ex. Message );
        }

    }

    public OperateResult DecodeReadData_ACS ( byte[ ] responseBytes )
    {
        try
        {
            Freq = _byteTransform. TransSingle ( responseBytes , Hex81Information. DataStartIndex );
            RangeIndex_Ua = responseBytes[Hex81Information. DataStartIndex + 4];  //取UA的档位索引
            RangeIndex_Ia = responseBytes[Hex81Information. DataStartIndex + 7];  //取IA的档位索引
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
        catch ( Exception ex )
        {
            return new OperateResult ( ex. Message );
        }

    }

    public OperateResult DecodeReadData_Status_ACS ( byte[ ] response )
    {
        try
        {
            Status_Ua = Enum. GetName ( ( ACSStatus ) response[6] );
            Status_Ia = Enum. GetName ( ( ACSStatus ) response[6] );
            Status_Ub = Enum. GetName ( ( ACSStatus ) response[7] );
            Status_Ib = Enum. GetName ( ( ACSStatus ) response[7] );
            Status_Uc = Enum. GetName ( ( ACSStatus ) response[8] );
            Status_Ic = Enum. GetName ( ( ACSStatus ) response[8] );
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
        catch ( Exception ex )
        {
            return new OperateResult ( ex. Message );
        }
    }
}



