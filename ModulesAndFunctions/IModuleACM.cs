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
/// <summary>
/// 交流标准表读取属性
/// </summary>
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
    public byte RangeIndex_Ia { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ib { get; }
    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    public byte RangeIndex_Ic { get; }
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
    #endregion 当前档位值》 

    #region 《频率幅值
    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    public float Freq { get; }
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
    public Channels? HarmonicChannels { get; }

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
}
