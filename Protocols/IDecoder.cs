using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 协议解码器
/// </summary>
internal interface IDecoder : IDeviceFunctions, IProperties_ACS
{
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// 【解码】解析联机指令的回复报文
    /// </summary>
    /// <param name="responsResult">联机指令操作结果</param>
    void DecodeHandShake ( OperateResult<byte[ ]> responsResult );

    /// <summary>
    /// 【解码】读取交流源档位信息
    /// </summary>
    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
    /// <returns></returns>
    OperateResult DecodeGetRanges_ACS ( OperateResult<byte[ ]> responsResult );

    OperateResult DecodeReadData_ACS ( OperateResult<byte[ ]> responsResult );

}
