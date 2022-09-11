
/**************************************************************************************************
 * 
 *  【系统设置类接口】 版本：V 1.0.0   Author:  Fuhong Zhou   2022年9月8日 22点43分  
 *  
 *  所有DK设备均需要实现该接口。
 *
 *************************************************************************************************/

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 系统设置接口
/// </summary>
public interface IFuncSettings:IProperties_Settings
{
    /// <summary>
    /// 联机命令
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> HandShake ( );

    /// <summary>
    /// 【高级权限需要密码】设置/修改设备信息
    /// </summary>
    /// <param name="password"></param>
    /// <param name="id"></param>
    /// <param name="sn"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetDeviceInfo ( char[ ] password , byte id , string sn );

    /// <summary>
    /// 修改/设置设备波特率
    /// </summary>
    /// <param name="baudRate"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetBaudRate ( ushort baudRate );

    /// <summary>
    /// 设置系统模式
    /// </summary>
    /// <param name="SystemMode">在对应的命名空间内提供的枚举：系统模式</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetSystemMode ( Enum SystemMode );

    /// <summary>
    /// 设置显示页面
    /// </summary>
    /// <param name="DisplayPage">在对应的命名空间内提供的枚举：显示页面</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> SetDisplayPage ( Enum DisplayPage );
}

/// <summary>
/// 系统设置属性
/// </summary>
public interface IProperties_Settings
{
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

    //TODO 协议功能增加
}
