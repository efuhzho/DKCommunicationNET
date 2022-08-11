using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Protocols. Hex5A;

/// <summary>
/// 基于Hex5AA5协议的设备所支持的功能状态
/// </summary>
internal class Hex5AFunctionsState : IProtocolFunctionsState
{
    /// <summary>
    /// 本类设备的协议类型：Hex5A
    /// </summary>
    public Models Model => Models. Hex5A;

    /// <summary>
    /// 基于本协议的设备：是否支持【交流源模块】：是
    /// </summary>
    public bool IsSupportedForACS => true;
    public bool IsSupportedForACM => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流源模块】：是
    /// </summary>
    public bool IsSupportedForDCS => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流表模块】：是
    /// </summary>
    public bool IsSupportedForDCM => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【开关量模块】：是
    /// </summary>
    public bool IsSupportedForIO => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【电能模块】：是
    /// </summary>
    public bool IsSupportedForPQ => true;

    public OperateResult<byte[ ]> GetPacketOfHandShake ( )
    {
        return OperateResult. CreateSuccessResult ( Hex5AInformation.HandShakePacket );
    }
}


/// <summary>
/// 基于Hex5AA5协议的设备所支持的功能状态
/// </summary>
[Flags]
internal enum FunctionsHex5A
{
    /// <summary>
    /// 基于本协议的设备：是否支持【交流源模块】：是
    /// </summary>
    IsACSModuleSupported = 0b_0000_0001,

    /// <summary>
    /// 基于本协议的设备：是否支持【直流源模块】：是
    /// </summary>
    IsDCSModuleSupported = 0b_0000_0010,

    /// <summary>
    /// 基于本协议的设备：是否支持【直流表模块】：是
    /// </summary>
    IsDCMModuleSupported = 0b_0000_0100,

    /// <summary>
    /// 基于本协议的设备：是否支持【开关量模块】：是
    /// </summary>
    IsIOModuleSupported = 0b_0000_1000,

    /// <summary>
    /// 基于本协议的设备：是否支持【电能模块】：是
    /// </summary>
    IsPQModuleSupported = 0b_0001_0000
}

