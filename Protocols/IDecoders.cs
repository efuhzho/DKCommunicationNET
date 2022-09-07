using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 协议解码器
/// </summary>
public interface IDecoders : IDeviceFunctions, IProperties_ACS, IReadProperties_DCS, IReadProperties_DCM, IReadProperies_EPQ
{
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// 【解码】解析联机指令的回复报文
    /// </summary>
    /// <param name="responsResult">联机指令操作结果</param>
    void DecodeHandShake ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取交流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
    /// <returns></returns>
    OperateResult DecodeGetRanges_ACS ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeReadData_ACS ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表输出状态的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeReadData_Status_ACS ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取直流源数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeReadData_DCS ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析获取直流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeGetRanges_DCS ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析获取直流表档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeGetRanges_DCM ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取直流表数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    OperateResult DecodeReadData_DCM ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】解析读取电能校验命令的回复报文
    /// </summary>
    /// <param name="responsResult"></param>
    /// <returns></returns>
    OperateResult DecodeReadData_EPQ ( OperateResult<byte[ ]> responsResult );

}

/// <summary>
/// 交流源解码器接口
/// </summary>
public interface IDecoder_ACS
{
    /// <summary>
    /// 【解码】解析读取交流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表输出状态的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_Status_ACS ( byte[ ] responsResult );

    #region 【属性】

    /// <summary>
    /// 电压档位个数
    /// </summary>
    byte RangesCount_ACU { get; }

    /// <summary>
    /// 电流档位个数
    /// </summary>
    byte RangesCount_ACI { get; }

    /// <summary>
    /// 当前电压档位的索引值，0为最大档位
    /// </summary>
    byte RangeIndex_ACU { get; }

    /// <summary>
    /// 当前交流电压档位值，单位V
    /// </summary>
    float RangeValue_ACU { get; }

    /// <summary>
    /// 当前电流档位的索引值，0为最大档位
    /// </summary>
    byte RangeIndex_ACI { get; }

    /// <summary>
    /// 当前交流电流档位值，单位A
    /// </summary>
    float RangeValue_ACI { get; }

    /// <summary>
    /// 当前保护电流档位的索引值，0为最大档位
    /// </summary>
    byte RangeIndex_IPr { get; }

    /// <summary>
    /// 当前保护电流档位值，单位A
    /// </summary>
    float RangeValue_IPr { get; }

    /// <summary>
    /// 保护电流档位个数
    /// </summary>
    byte RangesCount_IPr { get; }

    /// <summary>
    /// 只支持A相电压输出的起始档位号
    /// </summary>
    byte OnlyAStartIndex_ACU { get; }

    /// <summary>
    /// 只支持A相电流输出的起始档位号
    /// </summary>
    byte OnlyAStartIndex_ACI { get; }

    /// <summary>
    /// 只支持A相保护电流输出的起始档位号
    /// </summary>
    byte OnlyAStartIndex_IPr { get; }

    /// <summary>
    /// 电压档位集合
    /// </summary>
    float[ ]? Ranges_ACU { get; set; }

    /// <summary>
    /// 电流档位集合
    /// </summary>
    float[ ]? Ranges_ACI { get; set; }

    /// <summary>
    /// 保护电流档位集合
    /// </summary>
    float[ ]? Ranges_IPr { get; set; }

    /// <summary>
    /// 当前接线模式
    /// </summary>
    WireMode WireMode { get; set; }

    /// <summary>
    /// 当前闭环模式
    /// </summary>
    CloseLoopMode CloseLoopMode { get; set; }

    /// <summary>
    /// 当前谐波模式
    /// </summary>
    HarmonicMode HarmonicMode { get; set; }

    /// <summary>
    /// 频率(支持双频输出时为AB相频率)
    /// </summary>
    float Freq { get; }

    /// <summary>
    /// 【34B2适用】C相频率(支持双频输出时有效)
    /// </summary>
    float Freq_C { get; }

    /// <summary>
    /// 当前输出的谐波个数
    /// </summary>
    byte HarmonicCount { get; }

    /// <summary>
    /// 当前谐波输出通道
    /// </summary>
    Enum? HarmonicChannels { get; }

    /// <summary>
    /// 当前谐波输出数据
    /// </summary>
    HarmonicArgs[ ]? Harmonics { get; }

    /// <summary>
    /// A相电压数据
    /// </summary>
    float UA { get; }

    /// <summary>
    /// B相电压数据
    /// </summary>
    float UB { get; }

    /// <summary>
    /// C相电压数据
    /// </summary>
    float UC { get; }

    /// <summary>
    /// A相电流数据
    /// </summary>
    float IA { get; }
    /// <summary>
    /// B相电流数据
    /// </summary>
    float IB { get; }

    /// <summary>
    /// C相电流数据
    /// </summary>
    float IC { get; }

    /// <summary>
    /// 【51F适用】A相保护电流数据
    /// </summary>
    float IPA { get; }

    /// <summary>
    /// 【51F适用】B相保护电流数据
    /// </summary>
    float IPB { get; }

    /// <summary>
    /// 【51F适用】C相保护电流数据
    /// </summary>
    float IPC { get; }

    /// <summary>
    /// A相电压相位数据
    /// </summary>
    float FAI_UA { get; }

    /// <summary>
    /// B相电压相位数据
    /// </summary>
    float FAI_UB { get; }

    /// <summary>
    /// C相电压相位数据
    /// </summary>
    float FAI_UC { get; }

    /// <summary>
    /// A相电流相位数据
    /// </summary>
    float FAI_IA { get; }

    /// <summary>
    /// B相电流相位数据
    /// </summary>
    float FAI_IB { get; }

    /// <summary>
    /// C相电流相位数据
    /// </summary>
    float FAI_IC { get; }

    /// <summary>
    /// A相有功功率数据
    /// </summary>
    float PA { get; }

    /// <summary>
    /// B相有功功率数据
    /// </summary>
    float PB { get; }

    /// <summary>
    /// C相有功功率数据
    /// </summary>
    float PC { get; }

    /// <summary>
    /// 总有功功率数据
    /// </summary>
    float P { get; }

    /// <summary>
    /// A相无功功率数据
    /// </summary>
    float QA { get; }

    /// <summary>
    /// B相无功功率数据
    /// </summary>
    float QB { get; }

    /// <summary>
    /// C相无功功率数据
    /// </summary>
    float QC { get; }

    /// <summary>
    /// 总无功功率数据
    /// </summary>    
    float Q { get; }

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
}

/// <summary>
/// 直流源解码器
/// </summary>
public interface IDecoder_DCS
{
    /// <summary>
    /// 【解码】解析读取直流源数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_DCS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析获取直流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_DCS ( byte[ ] responsResult );

    #region 《档位数量

    /// <summary>
    /// 直流源电压档位个数
    /// </summary>
    byte RangesCount_DCU { get; }

    /// <summary>
    /// 直流源电流档位个数
    /// </summary>
    byte RangesCount_DCI { get; }

    /// <summary>
    /// 直流源电阻档位个数
    /// </summary>
    byte RangesCount_DCR { get; }

    #endregion 档位数量》

    #region 《幅值
    /// <summary>
    /// 当前直流源电压幅值
    /// </summary>
    float DCU { get; }

    /// <summary>
    /// 当前直流源电流幅值
    /// </summary>
    float DCI { get; }

    /// <summary>
    /// 当前直流电阻幅值
    /// </summary>
    float DCR { get; }

    #endregion 幅值》

    #region 《档位索引

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCU { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCI { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte RangeIndex_DCR { get; }

    #endregion 档位索引》

    #region 《档位列表

    /// <summary>
    /// 直流源电压档位列表
    /// </summary>
    float[ ]? Ranges_DCU { get; set; }

    /// <summary>
    /// 直流源电流档位列表
    /// </summary>
    float[ ]? Ranges_DCI { get; set; }

    /// <summary>
    /// 直流源电阻档位列表
    /// </summary>
    float[ ]? Ranges_DCR { get; set; }

    #endregion 档位列表》

    #region 《输出状态

    /// <summary>
    /// 当前直流电压输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCU { get; }

    /// <summary>
    /// 当前直流电流输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCI { get; }

    /// <summary>
    /// 当前直流电阻输出状态：true=源打开；false=源关闭
    /// </summary>
    bool IsOpen_DCR { get; }

    #endregion 输出状态》
}

/// <summary>
/// 【解码】直流表解码器
/// </summary>
public interface IDecoder_DCM
{
    /// <summary>
    /// 【解码】解析获取直流表档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_DCM ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取直流表数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_DCM ( byte[ ] responsResult );

    /// <summary>
    /// 直流表电压档位集合
    /// </summary>
    float[ ]? Ranges_DCMU { get; set; }

    /// <summary>
    /// 直流表电流档位集合
    /// </summary>
    float[ ]? Ranges_DCMI { get; set; }

    /// <summary>
    /// 直流纹波电压表档位集合
    /// </summary>
    float[ ]? Ranges_DCMU_Ripple { get; set; }

    /// <summary>
    /// 直流纹波电流表的档位集合
    /// </summary>
    float[ ]? Ranges_DCMI_Ripple { get; set; }

    /// <summary>
    /// 直流表电压测量值
    /// </summary>
    float DCMU { get; }

    /// <summary>
    /// 直流表电流测量值
    /// </summary>
    float DCMI { get; }

    /// <summary>
    /// 直流纹波电压测量值
    /// </summary>
    float DCMU_Ripple { get; }

    /// <summary>
    /// 直流纹波电流测量值
    /// </summary>
    float DCMI_Ripple { get; }

    /// <summary>
    /// 直流表电压量程当前索引值
    /// </summary>
    byte RangeIndex_DCMU { get; }

    /// <summary>
    /// 直流表电流量程当前索引值
    /// </summary>
    byte RangeIndex_DCMI { get; }

    /// <summary>
    /// 直流纹波电压量程当前索引值
    /// </summary>
    byte RangeIndex_DCMU_Ripple { get; }

    /// <summary>
    /// 直流纹波电流量程当前索引值
    /// </summary>
    byte RangeIndex_DCMI_Ripple { get; }

    /// <summary>
    /// 直流表电压量程数量
    /// </summary>
    byte RangesCount_DCMU { get; }

    /// <summary>
    /// 直流表电流量程数量
    /// </summary>
    byte RangesCount_DCMI { get; }

    /// <summary>
    /// 直流纹波电压量程数量
    /// </summary>
    byte RangesCount_DCMU_Ripple { get; }

    /// <summary>
    /// 直流纹波电流量程数量
    /// </summary>
    byte RangesCount_DCMI_Ripple { get; }
}

/// <summary>
/// 电能模块解码器
/// </summary>
public interface IDecoder_EPQ
{
    /// <summary>
    /// 【解码】解析读取电能校验命令的回复报文
    /// </summary>
    /// <param name="responsResult"></param>
    /// <returns></returns>
   internal OperateResult DecodeReadData_EPQ ( byte[ ] responsResult );

    /// <summary>
    /// 当前校验圈数
    /// </summary>
    uint Rounds_Current { get; }

    /// <summary>
    /// 当前校验次数
    /// </summary>
    uint Counts_Current { get; }

    /// <summary>
    /// 有功电能误差数据
    /// </summary>
    float EValue_P { get; }

    /// <summary>
    /// 无功电能误差数据
    /// </summary>
    float EValue_Q { get; }
}

/// <summary>
/// 设置解码器[HandShake]
/// </summary>
public interface IDecoder_Settings
{
    /// <summary>
    /// 【解码】联机命令，初始化设备信息和功能状态
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    internal OperateResult DecodeHandShake ( byte[ ] buffer );

    #region 《设备基本信息
    /// <summary>
    /// 设备型号
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string? SN { get; set; }

    /// <summary>
    /// 固件版本号
    /// </summary>
    public string? Firmware { get; }

    /// <summary>
    /// 协议版本号
    /// </summary>
    public string? ProtocolVer { get; set; }

    #endregion 设备基本信息》

    #region 《基本功能 FuncB
    /// <summary>
    /// 指示交流源功能是否激活
    /// </summary>
    public bool IsEnabled_ACS { get; }

    /// <summary>
    /// 指示交流表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM { get; }

    /// <summary>
    /// 指示标准表钳表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM_Cap { get; }

    /// <summary>
    /// 指示直流源功能是否激活
    /// </summary>
    public bool IsEnabled_DCS { get; }

    /// <summary>
    /// 辅助直流源是否激活
    /// </summary>
    public bool IsEnabled_DCS_AUX { get; }

    /// <summary>
    /// 指示直流表功能是否激活
    /// </summary>
    public bool IsEnabled_DCM { get; }

    /// <summary>
    /// 指示直流纹波表是否激活
    /// </summary>
    public bool IsEnabled_DCM_RIP { get; }


    /// <summary>
    /// 指示开关量功能是否激活
    /// </summary>
    public bool IsEnabled_IO { get; }

    /// <summary>
    /// 指示电能校验功能是否激活
    /// </summary>
    public bool IsEnabled_EPQ { get; }
    #endregion 基本功能 FuncB》

    #region 《特殊功能 FuncS 
    /// <summary>
    /// 指示双频输出功能是否激活
    /// </summary>
    public bool IsEnabled_DualFreqs { get; }

    /// <summary>
    /// 指示保护电流功能是否激活
    /// </summary>
    public bool IsEnabled_IProtect { get; }

    /// <summary>
    /// 指示闪变输出功能是否激活
    /// </summary>
    public bool IsEnabled_PST { get; }

    /// <summary>
    /// 指示遥信功能是否激活
    /// </summary>
    public bool IsEnabled_YX { get; }

    /// <summary>
    /// 指示高频输出功能是否激活
    /// </summary>
    public bool IsEnabled_HF { get; }

    /// <summary>
    /// 指示电机控制功能是否激活
    /// </summary>
    public bool IsEnabled_PWM { get; }

    /// <summary>
    /// 指示对时功能是否激活
    /// </summary>
    public bool IsEnabled_PPS { get; }

    #endregion 特殊功能 FuncS》
}
