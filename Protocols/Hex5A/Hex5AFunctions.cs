using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Protocols. Hex5A;

/// <summary>
/// 基于Hex5AA5协议的设备所支持的功能状态
/// </summary>
internal class Hex5AFunctions : IProtocolFunctions
{
    /// <summary>
    /// 本类设备的协议类型：Hex5A
    /// </summary>
    public Models Model => Models. Hex5A;

    /// <summary>
    /// 基于本协议的设备：是否支持【交流源模块】：是
    /// </summary>
    public bool IsSupported_ACS => true;
    public bool IsSupported_ACM => false;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流源模块】：是
    /// </summary>
    public bool IsSupported_DCS => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【直流表模块】：是
    /// </summary>
    public bool IsSupported_DCM => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【开关量模块】：是
    /// </summary>
    public bool IsSupported_IO => true;

    /// <summary>
    /// 基于本协议的设备：是否支持【电能模块】：是
    /// </summary>
    public bool IsSupported_EPQ => true;

    public bool IsSupported_ACM_Cap => false;

    public bool IsSupported_DCS_AUX => false;

    public bool IsSupported_DCM_RIP => false;

    public bool IsSupported_DualFreqs => false;

    public bool IsSupported_IProtect => false;

    public bool IsSupported_PST => false;

    public bool IsSupported_YX => true;

    public bool IsSupported_HF => false;

    public bool IsSupported_PWM =>false;

    public bool IsSupported_PPS => true;

    public OperateResult<byte[ ]> GetPacketOfHandShake ( )
    {
        return OperateResult. CreateSuccessResult ( Hex5AInformation.HandShakePacket );
    }
}




