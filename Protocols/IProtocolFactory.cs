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
    #region 《编码器
    /// <summary>
    /// 获取交流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_ACS> GetPacketBuilderOfACS ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取交流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_ACM> GetPacketBuilderOfACM ( ushort id );

    /// <summary>
    /// 获取直流源报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_DCS> GetPacketBuilderOfDCS ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取直流表报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder> GetPacketBuilderOfDCM ( ushort id );

    /// <summary>
    /// 获取开关量报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_IO> GetPacketBuilderOfIO ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取电能报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_EPQ> GetPacketBuilderOfEPQ ( ushort id , IByteTransform byteTransform );

    /// <summary>
    /// 获取校准报文创建类对象
    /// </summary>
    /// <returns></returns>
    OperateResult<IEncoder_Calibrate> GetPacketBuilderOfCalibrate ( ushort id , IByteTransform byteTransform );

    OperateResult<IEncoder_Settings> GetPacketBuilder_Settings ( ushort id );
    #endregion 编码器》

    #region 《解码器
    /// <summary>
    /// 生产交流源解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    IDecoder_ACS GetDecoder_ACS ( IByteTransform byteTransform );

    /// <summary>
    /// 生产直流源解码器
    /// </summary>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    IDecoder_DCS GetDecoder_DCS ( IByteTransform byteTransform );
    #endregion 解码器》

    #region 《校验器
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
    #endregion 校验器》

    /// <summary>
    /// 获取解码器
    /// </summary>
    /// <returns></returns>
    IDecoders GetDecoder ( IByteTransform byteTransform );   //TODO 删除




    //TODO 添加其他模块功能的报文创建器


}
