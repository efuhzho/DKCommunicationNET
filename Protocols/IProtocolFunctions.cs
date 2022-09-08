namespace DKCommunicationNET. Protocols;

/// <summary>
/// 【设备功能状态接口】所有基于不同协议类型的设备的功能状态：只读
/// </summary>
internal interface IProtocolFunctions
{
    #region FuncB

    /// <summary>
    /// 是否支持交流源模块
    /// </summary>
    public bool IsSupported_ACS { get; }

    /// <summary>
    /// 是否支持交流表模块
    /// </summary>
    public bool IsSupported_ACM { get; }

    /// <summary>
    /// 指示标准表钳表功能是否支持
    /// </summary>
    public bool IsSupported_ACM_Cap { get; }

    /// <summary>
    /// 是否支持直流源模块
    /// </summary>
    public bool IsSupported_DCS { get; }

    /// <summary>
    /// 辅助直流源是否支持
    /// </summary>
    public bool IsSupported_DCS_AUX { get; }

    /// <summary>
    /// 是否支持直流表模块
    /// </summary>
    public bool IsSupported_DCM { get; }

    /// <summary>
    /// 指示直流纹波表是否支持
    /// </summary>
    public bool IsSupported_DCM_RIP { get; }

    /// <summary>
    /// 是否支持开关量模块
    /// </summary>
    public bool IsSupported_IO { get; }

    /// <summary>
    /// 是否支持电能模块
    /// </summary>
    public bool IsSupported_EPQ { get; }

    #endregion  FuncB

    #region FuncS 
    /// <summary>
    /// 指示双频输出功能是否支持
    /// </summary>
    public bool IsSupported_DualFreqs { get; }

    /// <summary>
    /// 指示保护电流功能是否支持
    /// </summary>
    public bool IsSupported_IProtect { get; }

    /// <summary>
    /// 指示闪变输出功能是否支持
    /// </summary>
    public bool IsSupported_PST { get; }

    /// <summary>
    /// 指示遥信功能是否支持
    /// </summary>
    public bool IsSupported_YX { get; }

    /// <summary>
    /// 指示高频输出功能是否支持
    /// </summary>
    public bool IsSupported_HF { get; }

    /// <summary>
    /// 指示电机控制功能是否支持
    /// </summary>
    public bool IsSupported_PWM { get; }

    /// <summary>
    /// 指示对时功能是否支持
    /// </summary>
    public bool IsSupported_PPS { get; }

    #endregion FuncS

    //TODO 协议功能增加1
}
