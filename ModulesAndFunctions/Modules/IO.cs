using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. ModulesAndFunctions;

/// <summary>
/// 开关量功能模块
/// </summary>
public class IO : IModuleIO
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
    private readonly IEncoder_ACS? _PacketsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoders _decoder;

    internal IO ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse , IByteTransform byteTransform )
    {
        //接收设备ID
        _id = id;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;

        //初始化报文创建器
        _PacketsBuilder = protocolFactory. GetPacketBuilderOfACS ( _id , byteTransform ). Content; //忽略空值，调用时会捕获解引用为null的异常

        //接收解码器
        _decoder = protocolFactory. GetDecoder ( byteTransform );
    }
}
