namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 直流表模块接口
/// </summary>
public interface IModuleDCM:IReadProperties_DCM,ISetProperties_DCM
{
    /// <summary>
    /// 获取直流表档位信息
    /// </summary>
    public OperateResult<byte[ ]> GetRanges ( );

    /// <summary>
    /// 设置直流表直流电流档位
    /// </summary>
    /// <param name="rangeIndex_DCMI">要设置的直流电流测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_DCMI ( byte rangeIndex_DCMI );

    /// <summary>
    /// 设置直流表直流电压档位
    /// </summary>
    /// <param name="rangeIndex_DCMU">要设置的直流电压测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_DCMU ( byte rangeIndex_DCMU );

    /// <summary>
    /// 设置直流表纹波电流档位
    /// </summary>
    /// <param name="rangeIndex_DCMI">要设置的直流电流测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_DCMI_Ripple ( byte rangeIndex_DCMI );

    /// <summary>
    /// 设置直流表纹波电压档位
    /// </summary>
    /// <param name="rangeIndex_DCMU">要设置的直流电压测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_DCMU_Ripple ( byte rangeIndex_DCMU );

    /// <summary>
    /// 读取直流表当前测量数据
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData ( );
}

/// <summary>
/// 直流表属性
/// </summary>
public interface IReadProperties_DCM
{  
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
    byte RangeIndex_DCMU { get;  }

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
    byte RangeIndex_DCMI_Ripple { get;  }

    /// <summary>
    /// 直流表电压量程数量
    /// </summary>
    byte RangesCount_DCMU { get;  }

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
    byte RangesCount_DCMI_Ripple { get;  }
}

/// <summary>
/// 直流表设置属性
/// </summary>
public interface ISetProperties_DCM
{
    /// <summary>
    /// 是否是多通道直流表
    /// </summary>
    public bool IsMultiChannel { get; }
}