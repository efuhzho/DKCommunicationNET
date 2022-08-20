using DKCommunicationNET. Protocols. Hex81;
using System. Collections. Generic;
using System. IO. Ports;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 【接口】交流源模块接口
/// </summary>
public interface IModuleACS
{

    //TODO 添加输出精度信息
    #region 【属性】

    /// <summary>
    /// 电压档位个数
    /// </summary>
    internal byte RangesCount_ACU { get; }

    /// <summary>
    /// 电流档位个数
    /// </summary>
    internal byte RangesCount_ACI { get; }

    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    internal int RangeIndex_ACU { get; set; }

    /// <summary>
    /// 当前交流电压档位值，单位V
    /// </summary>
    float Range_ACU { get; }

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    internal int RangeIndex_ACI { get; set; }

    /// <summary>
    /// 当前交流电流档位值，单位A
    /// </summary>
    float Range_ACI { get; }

    /// <summary>
    /// 保护电流档位的索引值，0为最大档位
    /// </summary>
    internal int RangeIndex_IProtect { get; set; }

    /// <summary>
    /// 当前保护电流档位值，单位A
    /// </summary>
    float Range_IProtect { get; }

    /// <summary>
    /// 保护电流档位个数
    /// </summary>
     byte RangesCount_IProtect { get; }

    /// <summary>
    /// 只支持A相电压输出的起始档位号
    /// </summary>
     byte URanges_Asingle { get; }

    /// <summary>
    /// 只支持A相电流输出的起始档位号
    /// </summary>
     byte IRanges_Asingle { get; }

    /// <summary>
    /// 只支持A相保护电流输出的起始档位号
    /// </summary>
     byte IProtectRanges_Asingle { get; }

    /// <summary>
    /// 电压档位集合
    /// </summary>
    float[ ] ACU_RangesList { get; set; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    float[ ] ACI_RangesList { get; set; }

    /// <summary>
    /// 保护电流档位集合
    /// </summary>
    float[ ] IProtect_RangesList { get; set; }

    /// <summary>
    /// 当前接线模式枚举
    /// </summary>
    Enum WireMode { get; set; }

    /// <summary>
    /// 当前闭环模式枚举
    /// </summary>
    Enum CloseLoopMode { get; set; }

    /// <summary>
    /// 当前谐波模式枚举
    /// </summary>
    Enum HarmonicMode { get; set; }

    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    float Freq { get; set; }

    /// <summary>
    /// C相频率(支持双频输出时有效)
    /// </summary>
    float Freq_C { get; set; }

    /// <summary>
    /// 当前输出的谐波个数
    /// </summary>
    byte HarmonicCount { get; set; }

    /// <summary>
    /// 当前所有谐波输出通道
    /// </summary>
    Enum HarmonicChannels { get; set; }

    /// <summary>
    /// 当前所有谐波输出数据
    /// </summary>
    HarmonicArgs[ ] Harmonics { get; set; }

    /// <summary>
    /// A相电压数据
    /// </summary>
    float UA { get; set; }

    /// <summary>
    /// B相电压数据
    /// </summary>
    float UB { get; set; }

    /// <summary>
    /// C相电压数据
    /// </summary>
    float UC { get; set; }

    /// <summary>
    /// A相电流数据
    /// </summary>
    float IA { get; set; }

    /// <summary>
    /// B相电流数据
    /// </summary>
    float IB { get; set; }

    /// <summary>
    /// C相电流数据
    /// </summary>
    float IC { get; set; }

    /// <summary>
    /// A相保护电流数据
    /// </summary>
    float IPA { get; set; }

    /// <summary>
    /// B相保护电流数据
    /// </summary>
    float IPB { get; set; }

    /// <summary>
    /// C相保护电流数据
    /// </summary>
    float IPC { get; set; }

    /// <summary>
    /// A相电压相位数据
    /// </summary>
    float FAI_UA { get; set; }

    /// <summary>
    /// B相电压相位数据
    /// </summary>
    float FAI_UB { get; set; }

    /// <summary>
    /// C相电压相位数据
    /// </summary>
    float FAI_UC { get; set; }

    /// <summary>
    /// A相电流相位数据
    /// </summary>
    float FAI_IA { get; set; }

    /// <summary>
    /// B相电流相位数据
    /// </summary>
    float FAI_IB { get; set; }

    /// <summary>
    /// C相电流相位数据
    /// </summary>
    float FAI_IC { get; set; }

    /// <summary>
    /// A相有功功率数据
    /// </summary>
    float PA { get; set; }

    /// <summary>
    /// B相有功功率数据
    /// </summary>
    float PB { get; set; }

    /// <summary>
    /// C相有功功率数据
    /// </summary>
    float PC { get; set; }

    /// <summary>
    /// 总有功功率数据
    /// </summary>
    float P { get; set; }

    /// <summary>
    /// A相无功功率数据
    /// </summary>
    float QA { get; set; }

    /// <summary>
    /// B相无功功率数据
    /// </summary>
    float QB { get; set; }

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    float QC { get; set; }

    /// <summary>
    /// 总无功功率数据
    /// </summary>    
    float Q { get; set; }

    /// <summary>
    /// A相视在功率，单位：VA
    /// </summary>
    float SA { get; }

    /// <summary>
    /// B相视在功率，单位：VA
    /// </summary>
    float SB { get; }

    /// <summary>
    /// C相视在功率，单位：VA
    /// </summary>
    float SC { get; }

    /// <summary>
    /// 总实在功率
    /// </summary>
    float S { get; }

    /// <summary>
    /// A相功率因数
    /// </summary>
    float PFA { get; }

    /// <summary>
    /// B相功率因数
    /// </summary>
    float PFB { get; }

    /// <summary>
    /// C相功率因数
    /// </summary>
    float PFC { get; }

    /// <summary>
    /// 总功率因数
    /// </summary>
    float PF { get; }

    /// <summary>
    /// A相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    byte Flag_A { get; }

    /// <summary>
    /// B相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    byte Flag_B { get; }

    /// <summary>
    /// C相输出状态标志：FLAG=1表示输出不稳定，FLAG=0表示输出已稳定
    /// </summary>
    byte Flag_C { get; }

    #endregion 属性

    /// <summary>
    /// 交流源打开命令
    /// </summary>    
    /// <returns>
    ///     <list  type="table">
    ///     <listheader>
    ///         <term>返回对象属性</term>
    ///         <description>描述</description>
    ///     </listheader>     
    ///     <item>
    ///         <term>IsSuccess</term>
    ///         <description>成功标志：指示操作是否成功；【重要】调用系统方法后必须判断此标志。</description>
    ///     </item>      
    ///     <item>
    ///         <term>ErrorCode</term>
    ///         <description>错误代码：IsSuccess为true时忽略。</description>
    ///     </item>     
    ///     <item>
    ///          <term>Message</term>
    ///          <description>错误信息：IsSuccess为true时忽略。</description>
    ///     </item>
    ///      <item>
    ///          <term>Content</term>
    ///          <description>下位机回复的原始报文（或字节数组），当怀疑系统解码错误时，可用于诊断和临时自行解码，IsSuccess为false时忽略。</description>
    ///     </item>
    ///      </list>    
    /// </returns>
    /// <exception cref="NotImplementedException" ></exception>
    public OperateResult<byte[ ]> OpenACS ( );

    /// <summary>
    /// 交流源关闭命令
    /// </summary>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> CloseACS ( );

    /// <summary>
    /// 读取交流源档位
    /// </summary>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> GetRangesOfACS ( );

    /// <summary>
    /// 设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU"></param>
    /// <param name="rangeIndexOfACI"></param>
    /// <param name="rangeIndexOfIP"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 );

    /// <summary>
    /// 设置交流源幅度
    /// </summary>
    /// <param name="amplitude"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetAmplitudeOfACS ( float amplitude );

    /// <summary>
    /// 设置相位
    /// </summary>
    /// <param name="PhaseUa"></param>
    /// <param name="PhaseUb"></param>
    /// <param name="PhaseUc"></param>
    /// <param name="PhaseIa"></param>
    /// <param name="PhaseIb"></param>
    /// <param name="PhaseIc"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );

    /// <summary>
    /// 设置频率
    /// </summary>
    /// <param name="FreqOfAll"></param>
    /// <param name="FreqOfC"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 );

    /// <summary>
    /// 设置接线模式
    /// </summary>
    /// <param name="WireMode"></param>
    /// <returns><inheritdoc cref="OpenACS"/></returns>
    public OperateResult<byte[ ]> SetWireMode ( Enum WireMode );

    /// <summary>
    /// 设置闭环模式
    /// </summary>
    /// <param name="ClosedLoopMode">枚举类型参数</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetClosedLoop ( Enum ClosedLoopMode );

    /// <summary>
    /// 设置谐波模式
    /// </summary>
    /// <param name="HarmonicMode">枚举类型参数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetHarmonicMode ( Enum HarmonicMode );

    /// <summary>
    /// 设置谐波参数
    /// </summary>
    /// <param name="harmonicChannels"></param>
    /// <param name="harmonicArgs"></param>
    /// <returns></returns>
    OperateResult<byte[ ]> WriteHarmonics ( Enum harmonicChannels , HarmonicArgs[ ] harmonicArgs );
}

