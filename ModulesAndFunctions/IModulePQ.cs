namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 
/// </summary>
public interface IModuleEPQ : ISetProperties_EPQ,IReadProperies_EPQ
{
    /// <summary>
    /// 读取电能误差数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData ( Channels_EPQ Channels = Channels_EPQ. Channel1 );

    /// <summary>
    /// 设置参数并启动校验有功电能
    /// </summary>
    /// <param name="Const_PM">表有功脉冲常数</param>
    /// <param name="DIV">分频系数</param>
    /// <param name="Rounds">校验圈数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> StartTest_P ( float Const_PM , uint Rounds = 10 , uint DIV = 1 );

    /// <summary>
    /// 设置参数并启动校验无功电能
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> StartTest_Q ( float Const_QM , uint Rounds = 10 , uint DIV = 1 );

    /// <summary>
    /// 设置输出脉冲常数
    /// </summary>
    /// <param name="Const_PS">源有功脉冲常数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetConst_PS ( float Const_PS );

    /// <summary>
    /// 设置输出脉冲常数
    /// </summary>
    /// <param name="Const_QS">源无功脉冲常数</param>
    /// <returns></returns>
    OperateResult<byte[ ]> SetConst_QS ( float Const_QS );

}

/// <summary>
/// 电能模块的设置属性
/// </summary>
public interface ISetProperties_EPQ
{
    /// <summary>
    /// 表有功常数
    /// </summary>
    float Const_PM { get; set; }

    /// <summary>
    /// 表无功常数
    /// </summary>
    float Const_QM { get; set; }

    /// <summary>
    /// 源有功常数
    /// </summary>
    float Const_PS { get; set; }

    /// <summary>
    /// 源无功常数
    /// </summary>
    float Const_QS { get; set; }

    /// <summary>
    /// （表）分频系数
    /// </summary>
    uint DIV { get; set; }

    /// <summary>
    /// （表）设置的校验圈数
    /// </summary>
    uint Rounds { get; set; }  
}

/// <summary>
/// 电能模块的读取属性
/// </summary>
public interface IReadProperies_EPQ
{
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