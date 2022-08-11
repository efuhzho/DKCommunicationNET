using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 【设备功能状态接口】所有基于不同协议类型的设备的功能状态：只读
/// </summary>
internal interface IProtocolFunctionsState
{
    /// <summary>
    /// 设备型号：已关联协议类型
    /// </summary>
    public Models Model { get; }

    /// <summary>
    /// 是否支持交流源模块
    /// </summary>
    public bool IsACSModuleSupported { get; }

    /// <summary>
    /// 是否支持直流源模块
    /// </summary>
    public bool IsDCSModuleSupported { get; }

    /// <summary>
    /// 是否支持直流表模块
    /// </summary>
    public bool IsDCMModuleSupported { get; }

    /// <summary>
    /// 是否支持开关量模块
    /// </summary>
    public bool IsIOModuleSupported { get; }

    /// <summary>
    /// 是否支持电能模块
    /// </summary>
    public bool IsPQModuleSupported { get; }

    //TODO 协议增加
}
