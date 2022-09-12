using System;
using System. Collections. Generic;
using System. Linq;
using System. Reflection. Emit;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols. HexAA. Decoders;

internal class HexAADecoder_ACM : IDecoder_ACM
{
    IByteTransform _byteTransform;
    public HexAADecoder_ACM ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }
    public OperateResult DecodeReadData ( byte[ ] buffer )
    {
        try
        {
            WireMode = ( WireMode ) buffer[HexAA. DataStartIndex];
            CurrentInputChannel = ( CurrentInputChannel ) buffer[7];
            RangeSwitchMode = ( RangeSwitchMode ) buffer[9];
            Freq = _byteTransform. TransSingle ( buffer , 10 );
            RangeIndex_Ua=buffer[15];
            RangeIndex_Ia=buffer[17];
            RangeIndex_Ub=buffer[19];
            RangeIndex_Ib = buffer[21];
            RangeIndex_Uc = buffer[23];
            RangeIndex_Ic = buffer[25];
            UA=_byteTransform.TransSingle(buffer , 26);
            IA=_byteTransform.TransSingle(buffer, 30);
            UB = _byteTransform. TransSingle ( buffer , 26 );
            IB = _byteTransform. TransSingle ( buffer , 30 );
            UC = _byteTransform. TransSingle ( buffer , 34 );
            IC = _byteTransform. TransSingle ( buffer , 38 );
            FAI_UA = _byteTransform. TransSingle ( buffer , 42 );
            FAI_IA = _byteTransform. TransSingle ( buffer , 46 );
            FAI_UB= _byteTransform. TransSingle ( buffer , 50 );
            FAI_IB = _byteTransform. TransSingle ( buffer , 54 );
            FAI_UC = _byteTransform. TransSingle ( buffer , 58 );
            FAI_IC = _byteTransform. TransSingle ( buffer , 62 );
            PA = _byteTransform. TransSingle ( buffer , 64 );
            PB = _byteTransform. TransSingle ( buffer , 68 );
            PC = _byteTransform. TransSingle ( buffer , 72 );
            P = _byteTransform. TransSingle ( buffer , 76 );
            QA = _byteTransform. TransSingle ( buffer , 80 );
            QB = _byteTransform. TransSingle ( buffer , 84 );
            QC = _byteTransform. TransSingle ( buffer , 88 );
            Q = _byteTransform. TransSingle ( buffer , 92 );
            SA = _byteTransform. TransSingle ( buffer , 96 );
            SB= _byteTransform. TransSingle ( buffer , 100 );
            SC = _byteTransform. TransSingle ( buffer , 104 );
            S = _byteTransform. TransSingle ( buffer , 108 );
            PFA = _byteTransform. TransSingle ( buffer , 112 );
            PFB = _byteTransform. TransSingle ( buffer , 116 );
            PFC = _byteTransform. TransSingle ( buffer , 120 );
            PF = _byteTransform. TransSingle ( buffer , 124 );
            return OperateResult. CreateSuccessResult ( );

        }
        catch ( Exception ex)
        {
            return new OperateResult ( StringResources. Language. DecodeError + ex. Message );            
        }
    }

    public OperateResult DecodeGetRanges ( byte[ ] buffer )
    {
        try
        {
            RangesCount_ACU = buffer[9];
            RangesCount_ACI = buffer[11];
            Ranges_ACU = _byteTransform. TransSingle ( buffer , 12 , RangesCount_ACU );
            Ranges_ACI = _byteTransform. TransSingle ( buffer , RangesCount_ACU * 4 + 12 , RangesCount_ACI );
            return OperateResult. CreateSuccessResult ( );
        }
        catch ( Exception ex)
        {
            return new OperateResult (StringResources.Language.DecodeError+ex.Message );
        }
    }

    #region 《档位列表
    /// <summary>
    /// 电压档位集合
    /// </summary>
    public float[ ]? Ranges_ACU { get; private set; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    public float[ ]? Ranges_ACI { get; private set; }

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
    public byte RangeIndex_Ia { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ib { get; private set; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ic { get; private set; }
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
    #endregion 当前档位值》 

    #region 《频率幅值
    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    public float Freq { get; private set; }
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
    #endregion 电流幅值》

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
    public Channels? HarmonicChannels { get; private set; }

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
    /// 当前谐波模式
    /// </summary>
    public HarmonicMode HarmonicMode { get; set; }
    /// <summary>
    /// 无功计算方法
    /// </summary>
    public QP_Mode QP_Mode { get; set; }
    /// <summary>
    /// 档位切换模式
    /// </summary>
    public RangeSwitchMode RangeSwitchMode { get; set; }
    /// <summary>
    /// 大小电流输入通道切换
    /// </summary>
    public CurrentInputChannel CurrentInputChannel { get; set; }
    #endregion 枚举属性》
}
