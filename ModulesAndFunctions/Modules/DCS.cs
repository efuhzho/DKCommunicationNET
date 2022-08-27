using System. IO. Ports;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

/// <summary>
/// 直流源功能模块
/// </summary>
public class DCS :  IModuleDCS
{
    #region 私有字段

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
    private readonly IPacketsBuilder_ACS? _PacketsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder _decoder;

    #endregion
    internal DCS ( ushort id , IProtocolFactory protocolFactory , Func<byte[ ] , OperateResult<byte[ ]>> methodOfCheckResponse, IByteTransform byteTransform )
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

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="Exception" >ADS</exception>
    public void GetRangesOfDCS ( )
    {
        throw new NotImplementedException ( );
    }

    public void SetDCSAmplitude ( )
    {
        throw new NotImplementedException ( );
    }

    public void StartDCS ( )
    {

    }

    public void StopDCS ( )
    {
        throw new NotImplementedException ( );
    }
}
