﻿using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 【设备功能状态接口】所有基于不同协议类型的设备的功能状态：只读
/// </summary>
internal interface IProtocolFunctions
{  
    /// <summary>
    /// 是否支持交流源模块
    /// </summary>
    public bool IsSupportedForACS { get; }

    /// <summary>
    /// 是否支持交流表模块
    /// </summary>
    public bool IsSupportedForACM { get; }

    /// <summary>
    /// 是否支持直流源模块
    /// </summary>
    public bool IsSupportedForDCS { get; }

    /// <summary>
    /// 是否支持直流表模块
    /// </summary>
    public bool IsSupportedForDCM { get; }

    /// <summary>
    /// 是否支持开关量模块
    /// </summary>
    public bool IsSupportedForIO { get; }

    /// <summary>
    /// 是否支持电能模块
    /// </summary>
    public bool IsSupportedForPQ { get; }

    /// <summary>
    /// 获取联机协议报文
    /// </summary>
    /// <returns>含协议报文的操作结果对象</returns>
    OperateResult<byte[ ]> GetPacketOfHandShake ( );

    //TODO 协议功能增加
}
