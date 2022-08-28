namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 直流表模块接口
/// </summary>
public interface IModuleDCM
{
    /// <summary>
    /// 直流表属性
    /// </summary>
    public IProperties_DCM Properties { get; }

    /// <summary>
    /// 获取直流表档位信息
    /// </summary>
    public OperateResult<byte[ ]> GetRanges ( );

    /// <summary>
    /// 设置直流表直流电流档位
    /// </summary>
    /// <param name="rangeIndex_DCMI">要设置的直流电流测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_I ( byte rangeIndex_DCMI );

    /// <summary>
    /// 设置直流表直流电压档位
    /// </summary>
    /// <param name="rangeIndex_DCMU">要设置的直流电压测量档位</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetRange_U ( byte rangeIndex_DCMU );    

    /// <summary>
    /// 读取直流表当前测量数据
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> ReadData (  );
}

/// <summary>
/// 直流表属性
/// </summary>
public interface IProperties_DCM
{

}