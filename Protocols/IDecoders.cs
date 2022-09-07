using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols;

///// <summary>
///// 协议解码器
///// </summary>
//public interface IDecoders : IDeviceFunctions, IProperties_ACS, IReadProperties_DCS, IReadProperties_DCM, IReadProperies_EPQ
//{
//    /// <summary>
//    /// 数据区起始索引值
//    /// </summary>
//    int Offset { get; }

//    /// <summary>
//    /// 【解码】解析联机指令的回复报文
//    /// </summary>
//    /// <param name="responsResult">联机指令操作结果</param>
//    void DecodeHandShake ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取交流源档位信息命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
//    /// <returns></returns>
//    OperateResult DecodeGetRanges_ACS ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取交流源/表的命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeReadData_ACS ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取交流源/表输出状态的命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeReadData_Status_ACS ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取直流源数据命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeReadData_DCS ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析获取直流源档位信息命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeGetRanges_DCS ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析获取直流表档位信息命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeGetRanges_DCM ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取直流表数据命令的回复报文
//    /// </summary>
//    /// <param name="responsResult">指令操作结果</param>
//    /// <returns></returns>
//    OperateResult DecodeReadData_DCM ( OperateResult<byte[ ]> responsResult );

//    /// <summary>
//    /// 【解码】解析读取电能校验命令的回复报文
//    /// </summary>
//    /// <param name="responsResult"></param>
//    /// <returns></returns>
//    OperateResult DecodeReadData_EPQ ( OperateResult<byte[ ]> responsResult );

//}

/// <summary>
/// 交流源解码器接口
/// </summary>
public interface IDecoder_ACS:IProperties_ACS
{
    /// <summary>
    /// 【解码】解析读取交流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表输出状态的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_Status_ACS ( byte[ ] responsResult );    
}

/// <summary>
/// 直流源解码器
/// </summary>
public interface IDecoder_DCS:IReadProperties_DCS
{
    /// <summary>
    /// 【解码】解析读取直流源数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_DCS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析获取直流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_DCS ( byte[ ] responsResult );    
}

/// <summary>
/// 【解码】直流表解码器
/// </summary>
public interface IDecoder_DCM:IReadProperties_DCM
{
    /// <summary>
    /// 【解码】解析获取直流表档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeGetRanges_DCM ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取直流表数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
    internal OperateResult DecodeReadData_DCM ( byte[ ] responsResult );
}

/// <summary>
/// 电能模块解码器
/// </summary>
public interface IDecoder_EPQ:IReadProperies_EPQ
{
    /// <summary>
    /// 【解码】解析读取电能校验命令的回复报文
    /// </summary>
    /// <param name="responsResult"></param>
    /// <returns></returns>
   internal OperateResult DecodeReadData_EPQ ( byte[ ] responsResult );
}

/// <summary>
/// 设置解码器[HandShake]
/// </summary>
public interface IDecoder_Settings:IDeviceFunctions
{
    /// <summary>
    /// 【解码】联机命令，初始化设备信息和功能状态
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    internal OperateResult DecodeHandShake ( byte[ ] buffer );
    
}
