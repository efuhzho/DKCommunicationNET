using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 交流源解码器接口
/// </summary>
internal interface IDecoder_ACS:IProperties_ACS
{
    /// <summary>
    /// 【解码】解析读取交流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">【操作结果】下位机回复的报文</param>
    /// <returns></returns>
     OperateResult DecodeGetRanges_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取交流源/表的命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
     OperateResult DecodeReadData_ACS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】交流源输出状态读取
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    OperateResult DecodeReadData_Status_ACS ( byte[ ] response );
}

/// <summary>
/// 直流源解码器
/// </summary>
internal interface IDecoder_DCS:IReadProperties_DCS
{
    /// <summary>
    /// 【解码】解析读取直流源数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
     OperateResult DecodeReadData_DCS ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析获取直流源档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
     OperateResult DecodeGetRanges_DCS ( byte[ ] responsResult );    
}

/// <summary>
/// 【解码】直流表解码器
/// </summary>
internal interface IDecoder_DCM:IReadProperties_DCM
{
    /// <summary>
    /// 【解码】解析获取直流表档位信息命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
     OperateResult DecodeGetRanges_DCM ( byte[ ] responsResult );

    /// <summary>
    /// 【解码】解析读取直流表数据命令的回复报文
    /// </summary>
    /// <param name="responsResult">指令操作结果</param>
    /// <returns></returns>
     OperateResult DecodeReadData_DCM ( byte[ ] responsResult );
}

/// <summary>
/// 电能模块解码器
/// </summary>
internal interface IDecoder_EPQ:IReadProperies_EPQ
{
    /// <summary>
    /// 【解码】解析读取电能校验命令的回复报文
    /// </summary>
    /// <param name="responsResult"></param>
    /// <returns></returns>
    OperateResult DecodeReadData_EPQ ( byte[ ] responsResult );
}

/// <summary>
/// 设置解码器[HandShake]
/// </summary>
internal interface IDecoder_Settings:IProperties_Settings
{
    /// <summary>
    /// 【解码】联机命令，初始化设备信息和功能状态
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
     OperateResult DecodeHandShake ( byte[ ] buffer );
    
}

/// <summary>
/// 交流表解码器
/// </summary>
internal interface IDecoder_ACM:IPropertiesACM
{
    OperateResult DecodeReadData ( byte[ ] buffer );
    OperateResult DecodeGetRanges ( byte[ ] buffer );
}

/// <summary>
/// 对时功能解码器
/// </summary>
internal interface IDecoder_PPS
{

}

/// <summary>
/// 开关量解码器
/// </summary>
internal interface IDecoder_IO
{

}
