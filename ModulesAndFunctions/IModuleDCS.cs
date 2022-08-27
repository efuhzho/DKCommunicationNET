namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 直流源功能模块接口
/// </summary>
public interface IModuleDCS
{

    public void GetRangesOfDCS ( );
    public void SetDCSAmplitude ( );
    public void StartDCS ( );
    public void StopDCS ( );
}

/// <summary>
/// 直流源属性接口
/// </summary>
public interface IProperties_DCS
{
    /// <summary>
    /// 直流源电压档位个数
    /// </summary>
    byte URanges_Count { get; }

    /// <summary>
    /// 直流源电流档位个数
    /// </summary>
    byte IRanges_Count { get; }

    /// <summary>
    /// 当前直流源幅值
    /// </summary>
    float U_CurrentValue { get; }

    /// <summary>
    /// 当前直流源幅值
    /// </summary>
    float I_CurrentValue { get; }

    /// <summary>
    /// 当前档位索引值
    /// </summary>
    byte Index_CurrentRange { get; set; }

    /// <summary>
    /// 直流源电压档位列表
    /// </summary>
    float[ ] URanges { get; }

    /// <summary>
    /// 直流源电流档位列表
    /// </summary>
    float[ ] IRanges { get; }

    /// <summary>
    /// 直流源输出类型
    /// </summary>
    Enum DCS_Type { get; set; }

    /// <summary>
    /// 当前直流源输出状态：true=源打开；false=源关闭
    /// </summary>
    bool U_CurrentStatus { get; }

    /// <summary>
    /// 当前直流源输出状态：true=源打开；false=源关闭
    /// </summary>
    bool I_CurrentStatus { get; }
}