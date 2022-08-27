namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 直流源功能模块接口
/// </summary>
public interface IModuleDCS:IProperties_DCS
{
    /// <summary>
    /// 获取直流源档位信息
    /// </summary>
    public OperateResult<byte[ ]> GetRanges ( );

    /// <summary>
    /// 设置直流源档位
    /// </summary>
    /// <param name="rangeIndex">要设置的直流源档位</param>
    /// <param name="dCSourceType">要设置的直流源输出类型</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange ( byte rangeIndex , Enum dCSourceType );

    /// <summary>
    /// 设置直流源档位：自动档位模式
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> SetRange_AutoMode ( Enum dCSourceType );

    /// <summary>
    /// 设置直流源幅值
    /// </summary>
    /// <param name="rangeIndex">要设置的直流源档位索引值</param>
    /// <param name="SData">要设置的幅值</param>
    /// <param name="dCSourceType">枚举类型：直流源输出类型</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetAmplitude ( byte rangeIndex , float SData , Enum dCSourceType );

    /// <summary>
    /// 打开直流源命令
    /// </summary>
    public OperateResult<byte[ ]> Open ( );

    /// <summary>
    /// 关闭直流源命令
    /// </summary>
    public OperateResult<byte[ ]> Stop ( Enum? type = null );

    /// <summary>
    /// 读取直流源数据
    /// </summary>
    /// <param name="dCSourceType"></param>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData ( Enum? dCSourceType = null );
}

/// <summary>
/// 直流源属性接口
/// </summary>
public interface IProperties_DCS
{
    /// <summary>
    /// 直流源电压档位个数
    /// </summary>
    byte URanges_Count_DCS { get; }

    /// <summary>
    /// 直流源电流档位个数
    /// </summary>
    byte IRanges_Count_DCS { get; }

    /// <summary>
    /// 当前直流源电压幅值
    /// </summary>
    float U_CurrentValue_DCS { get; }

    /// <summary>
    /// 当前直流源电流幅值
    /// </summary>
    float I_CurrentValue_DCS { get; }

    /// <summary>
    /// 当前直流电阻幅值
    /// </summary>
    float R_CurrentValue_DCS { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte Index_CurrentRange_DCS { get; set; }

    /// <summary>
    /// 直流源电压档位列表
    /// </summary>
    float[ ]? URanges_DCS { get; }

    /// <summary>
    /// 直流源电流档位列表
    /// </summary>
    float[ ]? IRanges_DCS { get; }

    /// <summary>
    /// 直流源输出类型
    /// </summary>
    Enum? OutPutType_DCS { get; set; }

    /// <summary>
    /// 当前直流电压输出状态：true=源打开；false=源关闭
    /// </summary>
    bool U_IsOpen_DCS { get; }

    /// <summary>
    /// 当前直流电流输出状态：true=源打开；false=源关闭
    /// </summary>
    bool I_IsOpen_DCS { get; }

    /// <summary>
    /// 当前直流电阻输出状态：true=源打开；false=源关闭
    /// </summary>
    bool R_IsOpen_DCS { get; }
}