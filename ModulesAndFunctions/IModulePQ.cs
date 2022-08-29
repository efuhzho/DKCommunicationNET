namespace DKCommunicationNET. ModulesAndFunctions;


public interface IModulePQ
{
    /// <summary>
    /// 读取电能误差数据
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> ReadData ( );

    /// <summary>
    /// 设置参数并启动校验有功电能
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> StartTest_P ( );

    /// <summary>
    /// 设置参数并启动校验无功电能
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> StartTest_Q ( );
}
public interface IProperties_EPQ
{
    /// <summary>
    /// 表有功常数
    /// </summary>
    float ElectricityMeterConst_EP { get; set; }

    /// <summary>
    /// 表无功常数
    /// </summary>
    float ElectricityMeterConst_EQ { get; set; }

    /// <summary>
    /// 源有功常数
    /// </summary>
    float ElectricitySourceConst_EP { get; set; }

    /// <summary>
    /// 源无功常数
    /// </summary>
    float ElectricitySourceConst_EQ { get; set; }

    /// <summary>
    /// （表）分频系数
    /// </summary>
    uint ElectricityMeterDIV { get; set; }

    /// <summary>
    /// （表）设置的校验圈数
    /// </summary>
    uint ElectricitySetRounds { get; set; }

    /// <summary>
    /// 当前校验圈数
    /// </summary>
    uint ElectricityCurrentRounds { get; }

    /// <summary>
    /// 当前校验次数
    /// </summary>
    uint ElectricityCurrentCount { get; }

    /// <summary>
    /// 电能误差有效标志位:Flag=0 表示EV值无效，Flag=80 表示EV值为有功电能校验误差，Flag=81 表示EV值为无功电能校验误差
    /// </summary>
    byte ElectricityDeviationDataFlag { get; }

    /// <summary>
    /// 电能误差数据
    /// </summary>
    float EPQ { get; }

}