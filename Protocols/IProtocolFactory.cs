using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET;

/// <summary>
/// 协议工厂接口
/// </summary>
internal interface IProtocolFactory
{    
    /// <summary>
    /// 获取交流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketsBuilderOfACS> GetPacketsOfACS ( );

    /// <summary>
    /// 获取交流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilderOfACM> GetPacketsOfACM ( );

    /// <summary>
    /// 获取直流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilderOfDCS> GetPacketsOfDCS ( );

    /// <summary>
    /// 获取直流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilderOfDCM> GetPacketsOfDCM ( );

    /// <summary>
    /// 获取开关量报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilderOfIO> GetPacketsOfIO ( );

    /// <summary>
    /// 获取电能报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilderOfPQ> GetPacketsOfPQ ( );

    /// <summary>
    /// 获取CRC校验器对象
    /// </summary>
    /// <returns></returns>
    ICRCChecker GetCRCChecker ( );

    /// <summary>
    /// 获取协议功能状态对象
    /// </summary>
    /// <returns></returns>
    IProtocolFunctions GetProtocolFunctionsState ( );

    /// <summary>
    /// 获取解码器
    /// </summary>
    /// <returns></returns>
    IDecoder GetDecoder ( IByteTransform byteTransform );
}
