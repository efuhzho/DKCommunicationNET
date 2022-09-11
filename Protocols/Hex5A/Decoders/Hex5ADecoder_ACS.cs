using DKCommunicationNET. Core;

/**************************************************************************************************
 * 
 *  【交流源解码类】 版本：V 1.0.0   Author:  Fuhong Zhou   2022年9月8日 22点43分  
 *  
 *  支持的协议为DK-PTS系列通讯协议V2018 修订时间：2021年06月 作者：苏老师
 *
 *************************************************************************************************/

namespace DKCommunicationNET. Protocols. Hex5A. Decoders;

internal class Hex5ADecoder_ACS : IDecoder_ACS
{
    //数据转换规则
    private readonly IByteTransform _byteTransform;

    public Hex5ADecoder_ACS ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 《方法
    public OperateResult DecodeGetRanges_ACS ( byte[ ] responsResult )
    {
        try
        {
            if ( ( Type_Model ) responsResult[8] != Type_Model. ACS )
            {
                return new OperateResult ( StringResources. Language. DecodeError +"下位机回复的档位类型不是【交流源】。");
            }
            OutputChannelsNum = responsResult[9];
            OnlyAStartIndex_ACU = responsResult[10];
            OnlyAStartIndex_ACI = responsResult[11];
            RangesCount_ACU = responsResult[12];
            RangesCount_ACI = responsResult[13];
            Ranges_ACU = _byteTransform. TransSingle ( responsResult , 14 , RangesCount_ACU );
            Ranges_ACI = _byteTransform. TransSingle ( responsResult , 14 + 4 * RangesCount_ACU , RangesCount_ACI );
            return OperateResult.CreateSuccessResult ();
        }
        catch ( Exception ex )
        {
            return new OperateResult ( StringResources. Language. DecodeError + ex. Message );
        }
    }

    public OperateResult DecodeReadData_ACS ( byte[ ] responsResult )
    {
        try
        {
            ACSWorkingMode = Enum. GetName ( ( ACSWorkingMode ) responsResult[8] );
            WireMode = ( WireMode ) responsResult[9];
            CloseLoopMode = ( CloseLoopMode ) responsResult[10];
            HarmonicMode = ( HarmonicMode ) responsResult[11];
            QP_Mode = ( QP_Mode ) responsResult[12];
            FrequencySync = Enum. GetName ( ( FrequencySync ) responsResult[13] );//Enum.GetName(typeof(FrequencySync), responsResult[13]);
            OutputtingChannelsNum = responsResult[14];
            //以下为A相数据
            RangeIndex_Ua = responsResult[15];
            Status_Ua = Enum. GetName ( ( ACSStatus ) ( responsResult[16] & 0b_0111_1111 ) );
            RangeIndex_Ia = responsResult[17];
            Status_Ia = Enum. GetName ( ( ACSStatus ) ( responsResult[18] & 0b_0111_1111 ) );
            Freq = _byteTransform. TransSingle ( responsResult , 19 );
            UA = _byteTransform. TransSingle ( responsResult , 23 );
            IA = _byteTransform. TransSingle ( responsResult , 27 );
            FAI_UA = _byteTransform. TransSingle ( responsResult , 31 );
            FAI_IA = _byteTransform. TransSingle ( responsResult , 35 );
            PA = _byteTransform. TransSingle ( responsResult , 39 );
            QA = _byteTransform. TransSingle ( responsResult , 43 );
            SA = _byteTransform. TransSingle ( responsResult , 47 );
            PFA = _byteTransform. TransSingle ( responsResult , 51 );
            if ( OutputtingChannelsNum > 1 )
            {
                //以下为B相数据
                RangeIndex_Ub = responsResult[52];
                Status_Ua = Enum. GetName ( ( ACSStatus ) ( responsResult[53] & 0b_0111_1111 ) );
                RangeIndex_Ib = responsResult[54];
                Status_Ib = Enum. GetName ( ( ACSStatus ) ( responsResult[55] & 0b_0111_1111 ) );
                Freq = _byteTransform. TransSingle ( responsResult , 56 );  //B相频率永等于A相
                UB = _byteTransform. TransSingle ( responsResult , 60 );
                IB = _byteTransform. TransSingle ( responsResult , 64 );
                FAI_UB = _byteTransform. TransSingle ( responsResult , 68 );
                FAI_IB = _byteTransform. TransSingle ( responsResult , 72 );
                PB = _byteTransform. TransSingle ( responsResult , 76 );
                QB = _byteTransform. TransSingle ( responsResult , 80 );
                SB = _byteTransform. TransSingle ( responsResult , 84 );
                PFB = _byteTransform. TransSingle ( responsResult , 88 );
                //以下为C相数据
                RangeIndex_Uc = responsResult[89];
                Status_Uc = Enum. GetName ( ( ACSStatus ) ( responsResult[90] & 0b_0111_1111 ) );
                RangeIndex_Ic = responsResult[91];
                Status_Ic = Enum. GetName ( ( ACSStatus ) ( responsResult[92] & 0b_0111_1111 ) );
                Freq_C = _byteTransform. TransSingle ( responsResult , 93 );     //TODO 异频数据是C相还是X相？
                UC = _byteTransform. TransSingle ( responsResult , 97 );
                IC = _byteTransform. TransSingle ( responsResult , 101 );
                FAI_UC = _byteTransform. TransSingle ( responsResult , 105 );
                FAI_IC = _byteTransform. TransSingle ( responsResult , 109 );
                PC = _byteTransform. TransSingle ( responsResult , 113 );
                QC = _byteTransform. TransSingle ( responsResult , 117 );
                SC = _byteTransform. TransSingle ( responsResult , 121 );
                PFC = _byteTransform. TransSingle ( responsResult , 125 );
            }
            if ( OutputtingChannelsNum == 4 )
            {
                //以下为X相数据
                RangeIndex_Ux = responsResult[126];
                Status_Ux = Enum. GetName ( ( ACSStatus ) ( responsResult[127] & 0b_0111_1111 ) );
                RangeIndex_Ix = responsResult[128];
                Status_Ix = Enum. GetName ( ( ACSStatus ) ( responsResult[129] & 0b_0111_1111 ) );
                Freq_X = _byteTransform. TransSingle ( responsResult , 130 );       //TODO 异频数据是C相还是X相？
                UX = _byteTransform. TransSingle ( responsResult , 134 );
                IX = _byteTransform. TransSingle ( responsResult , 138 );
                FAI_UX = _byteTransform. TransSingle ( responsResult , 142 );
                FAI_IX = _byteTransform. TransSingle ( responsResult , 146 );
                PX = _byteTransform. TransSingle ( responsResult , 150 );
                QX = _byteTransform. TransSingle ( responsResult , 154 );
                SX = _byteTransform. TransSingle ( responsResult , 158 );
                PFX = _byteTransform. TransSingle ( responsResult , 162 );
            }
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex )
        {
            return new OperateResult ( StringResources. Language. DecodeError + ex. Message );
        }
    }

    public OperateResult DecodeReadData_Status_ACS ( byte[ ] response )
    {
        return OperateResult. CreateSuccessResult ( );
    }
    #endregion 方法》

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
    /// 【34B2适用】C相频率(支持双频输出时有效)
    /// </summary>
    public float Freq_X { get; private set; }

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
    public string? ACSWorkingMode { get; private set; }

    #endregion 属性》
}
