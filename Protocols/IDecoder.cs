using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 协议解码器
/// </summary>
internal interface IDecoder
{
    /// <summary>
    /// 数据区起始索引值
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// 设备型号
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// 设备版本号
    /// </summary>
    public string? Version { get; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string? SN { get; set; }

    /// <summary>
    /// 基本功能
    /// </summary>
    public byte FuncB { get; }

    /// <summary>
    /// 特殊功能
    /// </summary>
    public byte FuncS { get; }

    /// <summary>
    /// 【只适用于Hex5A】直流源功能
    /// </summary>
    public byte FuncD { get; }

    /// <summary>
    /// 解析联机指令的回复报文
    /// </summary>
    /// <param name="result">联机指令操作结果</param>
    void DecodeHandShake ( OperateResult<byte[ ]> result );
}
