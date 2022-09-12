using System;
using System. Collections. Generic;
using System. Linq;
using System. Reflection. Emit;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 交流表模块功能
/// </summary>
public interface IModuleACM
{
    /// <summary>
    /// 创建报文：读取交流标准表测量值/标准源输出值
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData ( );

    /// <summary>
    /// 创建报文：读取输出状态：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData_Status ( );
}

/// <summary>
/// ACM设置属性
/// </summary>
public interface ISetPropertiesACM
{
    /// <summary>
    /// 接线模式
    /// </summary>
    public WireMode WireMode { get; set; }
    /// <summary>
    /// 档位切换模式
    /// </summary>
    public RangeSwitchMode RangeSwitchMode { get; set; }
    /// <summary>
    /// 大小电流输入通道切换
    /// </summary>
    public CurrentInputChannel CurrentInputChannel { get; set; }
}

public interface IReadPropertiesACM
{
    #region 《档位列表
    /// <summary>
    /// 电压档位集合
    /// </summary>
    public float[ ]? Ranges_ACU { get; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    public float[ ]? Ranges_ACI { get; }
   
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
    public byte RangeIndex_Ua { get; }
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ub { get; }
    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Uc { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ux { get; }

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ia { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ib { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ic { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ix { get; }

    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPa { get; }
    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPb { get; }
    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_IPc { get; }

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
    /// 只支持A相输出的起始档位号:0-表示单相电压；如果值等于档位个数则说明：没有仅A相输出的档位
    /// </summary>
    public byte OnlyAStartIndex_ACU { get; }

    /// <summary>
    /// 只支持A相输出的起始档位号:0-表示单相电压；如果值等于档位个数则说明：没有仅A相输出的档位
    /// </summary>
    public byte OnlyAStartIndex_ACI { get; }

    /// <summary>
    ///只支持A相输出的起始档位号:0-表示单相电压；如果值等于档位个数则说明：没有仅A相输出的档位
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
    /// <summary>
    /// 【34B2适用】C相频率(支持双频输出时有效)
    /// </summary>
    public float Freq_X { get; }
    /// <summary>
    /// 频率标志：四相同频/四相异频
    /// </summary>
    public string? FrequencySync { get; }
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
    /// <summary>
    /// X相电压数据
    /// </summary>
    public float UX { get; }
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
    /// <summary>
    /// C相电流数据
    /// </summary>
    public float IX { get; }
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
    /// C相电压相位数据
    /// </summary>
    public float FAI_UX { get; }

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
    /// <summary>
    /// C相电流相位数据
    /// </summary>
    public float FAI_IX { get; }
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
    /// C相有功功率数据
    /// </summary>
    public float PX { get; }

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
    /// C相无功功率数据
    /// </summary>
    public float QX { get; }

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
    /// C相视在功率，单位：VA
    /// </summary>
    public float SX { get; }

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
    public float PFX { get; }

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
    /// <summary>
    /// 无功计算方法
    /// </summary>
    public QP_Mode QP_Mode { get; set; }
    #endregion 枚举属性》

    #region 《输出状态
    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ua { get; }

    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ub { get; }

    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Uc { get; }
    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ux { get; }
    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ia { get; }

    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ib { get; }

    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ic { get; }
    /// <summary>
    /// 输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    public string? Status_Ix { get; }
    #endregion 输出状态》

    /// <summary>
    /// 当前输出的通道相数：1=单相，3=三相，4=四相
    /// </summary>
    public byte? OutputtingChannelsNum { get; }
    /// <summary>
    /// 本装置交流源的相数：1=单相，3=三相，4=四相
    /// </summary>
    public byte? OutputChannelsNum { get; }
    /// <summary>
    /// 交流源当前工作模式：标准源/功耗模式
    /// </summary>
    public string? ACSWorkingMode { get; }
}
