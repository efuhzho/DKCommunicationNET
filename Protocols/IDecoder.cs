using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 协议解码器
/// </summary>
internal interface IDecoder:IDeviceFunctions
{
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// 解析联机指令的回复报文
    /// </summary>
    /// <param name="result">联机指令操作结果</param>
    void DecodeHandShake ( OperateResult<byte[ ]> result );   
}
