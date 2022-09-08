using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols. Hex5A. Decoders;

internal class Hex5ADecoder_ACS:IDecoder_ACS
{
    //数据转换规则
    private readonly IByteTransform _byteTransform;

    public Hex5ADecoder_ACS ( IByteTransform byteTransform )
    {
        _byteTransform = byteTransform;
    }

    #region 《方法

    OperateResult IDecoder_ACS.DecodeGetRanges_ACS ( byte[ ] responsResult )
    {
        try
        {

        }
        catch ( Exception )
        {

            throw;
        }
        throw new NotImplementedException ( );

    }

    OperateResult IDecoder_ACS.DecodeReadData_ACS ( byte[ ] responsResult )
    {
        try
        {

        }
        catch ( Exception )
        {

            throw;
        }
        throw new NotImplementedException ( );
    }

    OperateResult IDecoder_ACS.DecodeReadData_Status_ACS ( byte[ ] responsResult )
    {
        try
        {

        }
        catch ( Exception )
        {

            throw;
        }
        throw new NotImplementedException ( );
    }
    #endregion 方法》

    #region 《属性
    #region 《档位列表
    /// <summary>
    /// 电压档位集合
    /// </summary>
    public float[ ]? Ranges_ACU { get; set; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    public float[ ]? Ranges_ACI { get; set; }

    /// <summary>
    /// 保护电流档位集合
    /// </summary>
    public float[ ]? Ranges_IPr { get; set; }
    #endregion 档位列表》

    #region 《档位数量
    /// <summary>
    /// 电压档位个数
    /// </summary>
    public byte RangesCount_ACU { get; }

    /// <summary>
    /// 电流档位个数
    /// </summary>
    public byte RangesCount_ACI { get; }

    /// <summary>
    /// 保护电流档位个数
    /// </summary>
    public byte RangesCount_IPr { get; }

    #endregion 档位数量》

    #region 《当前档位索引
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_ACU { get; }

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_ACI { get; }

    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPr { get; }

    #endregion 当前档位索引》

    #region 《当前档位值
    /// <summary>
    /// 当前交流电压档位值，单位V
    /// </summary>
    public float RangeValue_ACU { get; }

    /// <summary>
    /// 当前交流电流档位值，单位A
    /// </summary>
    public float RangeValue_ACI { get; }

    /// <summary>
    /// 当前保护电流档位值，单位A
    /// </summary>
    public float RangeValue_IPr { get; }

    #endregion 当前档位值》

    #region 《只支持A相的档位起始索引
    /// <summary>
    /// 只支持A相电压输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_ACU { get; }

    /// <summary>
    /// 只支持A相电流输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_ACI { get; }

    /// <summary>
    /// 只支持A相保护电流输出的起始档位号
    /// </summary>
    public byte OnlyAStartIndex_IPr { get; }
    #endregion 只支持A相的档位起始索引》

    #region 《频率幅值
    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    public float Freq { get; }

    /// <summary>
    /// 【34B2适用】C相频率(支持双频输出时有效)
    /// </summary>
    public float Freq_C { get; }
    #endregion 频率幅值》

    #region 《电压幅值
    /// <summary>
    /// A相电压数据
    /// </summary>
    public float UA { get; }

    /// <summary>
    /// B相电压数据
    /// </summary>
    public float UB { get; }

    /// <summary>
    /// C相电压数据
    /// </summary>
    public float UC { get; }
    #endregion 电压幅值》

    #region 《电流幅值
    /// <summary>
    /// A相电流数据
    /// </summary>
    public float IA { get; }
    /// <summary>
    /// B相电流数据
    /// </summary>
    public float IB { get; }

    /// <summary>
    /// C相电流数据
    /// </summary>
    public float IC { get; }
    #endregion 电流幅值》

    #region 《保护电流幅值
    /// <summary>
    /// 【51F适用】A相保护电流数据
    /// </summary>
    public float IPA { get; }

    /// <summary>
    /// 【51F适用】B相保护电流数据
    /// </summary>
    public float IPB { get; }

    /// <summary>
    /// 【51F适用】C相保护电流数据
    /// </summary>
    public float IPC { get; }
    #endregion 保护电流幅值》

    #region 《相位幅值
    /// <summary>
    /// A相电压相位数据
    /// </summary>
    public float FAI_UA { get; }

    /// <summary>
    /// B相电压相位数据
    /// </summary>
    public float FAI_UB { get; }

    /// <summary>
    /// C相电压相位数据
    /// </summary>
    public float FAI_UC { get; }

    /// <summary>
    /// A相电流相位数据
    /// </summary>
    public float FAI_IA { get; }

    /// <summary>
    /// B相电流相位数据
    /// </summary>
    public float FAI_IB { get; }

    /// <summary>
    /// C相电流相位数据
    /// </summary>
    public float FAI_IC { get; }
    #endregion 相位幅值》

    #region 《功率幅值
    /// <summary>
    /// A相有功功率数据
    /// </summary>
    public float PA { get; }

    /// <summary>
    /// B相有功功率数据
    /// </summary>
    public float PB { get; }

    /// <summary>
    /// C相有功功率数据
    /// </summary>
    public float PC { get; }

    /// <summary>
    /// 总有功功率数据
    /// </summary>
    public float P { get; }

    /// <summary>
    /// A相无功功率数据
    /// </summary>
    public float QA { get; }

    /// <summary>
    /// B相无功功率数据
    /// </summary>
    public float QB { get; }

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    public float QC { get; }

    /// <summary>
    /// 总无功功率数据
    /// </summary>    
    public float Q { get; }

    /// <summary>
    /// A相视在功率，单位：VA
    /// </summary>
    public float SA { get; }

    /// <summary>
    /// B相视在功率，单位：VA
    /// </summary>
    public float SB { get; }

    /// <summary>
    /// C相视在功率，单位：VA
    /// </summary>
    public float SC { get; }

    /// <summary>
    /// 总实在功率
    /// </summary>
    public float S { get; }
    #endregion 功率幅值》

    #region 《功率因数
    /// <summary>
    /// A相功率因数
    /// </summary>
    public float PFA { get; }

    /// <summary>
    /// B相功率因数
    /// </summary>
    public float PFB { get; }

    /// <summary>
    /// C相功率因数
    /// </summary>
    public float PFC { get; }

    /// <summary>
    /// 总功率因数
    /// </summary>
    public float PF { get; }
    #endregion 功率因数》

    #region 《谐波数据
    /// <summary>
    /// 当前输出的谐波个数
    /// </summary>
    public byte HarmonicCount { get; }

    /// <summary>
    /// 当前谐波输出通道
    /// </summary>
    public Enum? HarmonicChannels { get; }

    /// <summary>
    /// 当前谐波输出数据
    /// </summary>
    public HarmonicArgs[ ]? Harmonics { get; }
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
    #endregion 枚举属性》

    #region 《输出状态
    /// <summary>
    /// A相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public byte Flag_A { get; }

    /// <summary>
    /// B相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public byte Flag_B { get; }

    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public byte Flag_C { get; }
    #endregion 输出状态》
    #endregion 属性》
}
