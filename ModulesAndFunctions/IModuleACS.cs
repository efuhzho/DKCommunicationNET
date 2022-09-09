using DKCommunicationNET. Protocols. Hex81;
using System. Collections. Generic;
using System. IO. Ports;
using System. Security. Cryptography;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 【接口】交流源模块接口
/// </summary>
public interface IModuleACS : IProperties_ACS
{
    //TODO 添加输出精度信息？

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
    public OperateResult<byte[ ]> Open ( );

    /// <summary>
    /// 交流源关闭命令
    /// </summary>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> Stop ( );

    /// <summary>
    /// 读取交流源档位
    /// </summary>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> GetRanges ( );

    /// <summary>
    /// 设置交流源档位
    /// </summary>
    /// <param name="rangeIndexOfACU"></param>
    /// <param name="rangeIndexOfACI"></param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI );

    /// <summary>
    /// 设置保护电流档位
    /// </summary>
    /// <param name="rangeIndex_IP"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRanges_IP ( byte rangeIndex_IP );

    /// <summary>
    /// 设置X相档位
    /// </summary>
    /// <param name="rangeIndex_Ux"></param>
    /// <param name="rangeIndex_Ix"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRanges_X ( byte rangeIndex_Ux , byte rangeIndex_Ix );

    /// <summary>
    /// 设置交流源幅度
    /// </summary>
    /// <param name="UA">要设定的电压幅值</param>
    /// <param name="UB">要设定的电压幅值</param>
    /// <param name="UC">要设定的电压幅值</param>
    /// <param name="IA">要设定的电流幅值</param>
    /// <param name="IB">要设定的电流幅值</param>
    /// <param name="IC">要设定的电流幅值</param>
    /// <param name="IPA">要设定的保护电流幅值</param>
    /// <param name="IPB">要设定的保护电流幅值</param>
    /// <param name="IPC">要设定的保护电流幅值</param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 );

    /// <summary>
    /// <inheritdoc cref="SetAmplitude(float, float, float, float, float, float, float, float, float)"/>
    /// </summary>
    /// <param name="U">【所有相幅值相同】要设定的电压幅值</param>
    /// <param name="I">【所有相幅值相同】要设定的电流幅值</param>
    /// <param name="IP">【所有相幅值相同】要设定的保护电流幅值</param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetAmplitude ( float U , float I , float IP = 0 );

    /// <summary>
    /// 设置相位：【A相电压相位作为基准参考相位点，始终为0°】
    /// </summary>
    /// <param name="PhaseUb">B相电压相位</param>
    /// <param name="PhaseUc">C相电压相位</param>
    /// <param name="PhaseIa">A相电流相位</param>
    /// <param name="PhaseIb">B相电流相位</param>
    /// <param name="PhaseIc">C相电流相位</param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetPhase ( float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );

    /// <summary>
    /// 设置频率
    /// </summary>
    /// <param name="FreqOfAll">要设置的频率值：【当支持双频时，为A相和B相频率】</param>
    /// <param name="FreqOfC">【34B2适用，支持双频时有效】</param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 );

    /// <summary>
    /// 设置接线模式
    /// </summary>
    /// <param name="WireMode">枚举类型：接线方式；</param>
    /// <returns><inheritdoc cref="Open"/></returns>
    public OperateResult<byte[ ]> SetWireMode ( WireMode WireMode );

    /// <summary>
    /// 设置闭环模式
    /// </summary>
    /// <param name="ClosedLoopMode">枚举类型参数：闭环模式；</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetClosedLoop ( CloseLoopMode ClosedLoopMode );

    /// <summary>
    /// 设置谐波模式
    /// </summary>
    /// <param name="HarmonicMode">枚举类型参数：SetHarmonicMode；</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetHarmonicMode ( HarmonicMode HarmonicMode );

    /// <summary>
    /// 设置谐波参数
    /// </summary>
    /// <param name="harmonicChannels">枚举类型参数：谐波通道；【注意】需引用对应的协议类型的命名空间</param>
    /// <param name="harmonicArgs">要设置的谐波参数组【可选参数，当参数为null时，将清空所选通道的谐波。清空谐波还可以调用方法：ClearHarmonics】</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetHarmonics ( Enum harmonicChannels , HarmonicArgs[ ]? harmonicArgs=null );

    /// <summary>
    /// 清除谐波
    /// </summary>
    /// <param name="harmonicChannels">需要清除的谐波通道</param>
    /// <returns></returns>
    OperateResult<byte[ ]> ClearHarmonics ( Enum harmonicChannels );

    /// <summary>
    /// 读取交流源当前输出数据
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData ( );

    /// <summary>
    /// 读取交流源当前输出状态
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData_Status ( );
}

/// <summary>
/// 交流源属性
/// </summary>
public interface IProperties_ACS
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

    /// <summary>
    /// 保护电流档位集合
    /// </summary>
    public float[ ]? Ranges_IPr { get; }
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
    /// 当前交流源工作模式：标准源/功耗模式
    /// </summary>
    public string? ACSMode { get; }    
  
}

