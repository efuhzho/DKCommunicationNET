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
/// 协议抽象工厂接口
/// </summary>
internal interface IProtocolFactory
{    
    /// <summary>
    /// 获取交流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketsBuilder_ACS> GetPacketBuilderOfACS (ushort id ,IByteTransform byteTransform);

    /// <summary>
    /// 获取交流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_ACM> GetPacketBuilderOfACM ( ushort id  );

    /// <summary>
    /// 获取直流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_DCS> GetPacketBuilderOfDCS ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取直流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_DCM> GetPacketBuilderOfDCM (ushort id );

    /// <summary>
    /// 获取开关量报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_IO> GetPacketBuilderOfIO ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取电能报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_EPQ> GetPacketBuilderOfEPQ ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取校准报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IPacketBuilder_Calibrate> GetPacketBuilderOfCalibrate ( ushort id , IByteTransform byteTransform );

    //TODO 添加其他模块功能的报文创建器

    /// <summary>
    /// 获取CRC校验器对象
    /// </summary>
    /// <returns></returns>
    ICRCChecker GetCRCChecker ( );

    /// <summary>
    /// 获取协议功能状态对象
    /// </summary>
    /// <returns></returns>
    IProtocolFunctions GetProtocolFunctions ( );

    /// <summary>
    /// 获取解码器
    /// </summary>
    /// <returns></returns>
    IDecoder GetDecoder ( IByteTransform byteTransform );
}
