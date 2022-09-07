using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. ModulesAndFunctions. Modules;

/// <summary>
/// 交流表模块
/// </summary>
public class ACM : IModuleACM
{
    /// <summary>
    /// 设备ID
    /// </summary>
    private readonly ushort _id;

    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_ACS? _packetsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoders _decoder;

    internal ACM ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _packetsBuilder = protocolFactory. GetPacketBuilderOfACS ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }

    ///// <inheritdoc/>

    //public OperateResult<byte[ ]> ReadData ( )
    //{
    //    throw new NotImplementedException ( );
    //}
    ///// <inheritdoc/>

    //public OperateResult<byte[ ]> ReadData_Status ( )
    //{
    //    throw new NotImplementedException ( );
    //}
}
