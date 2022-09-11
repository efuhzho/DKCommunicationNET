using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. ModulesAndFunctions. Functions;

/// <summary>
/// 设置功能
/// </summary>
public class Settings : IFuncSettings
{
    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_Settings _encoder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_Settings _decoder;

    private readonly CommandAction CommandAction;

    internal Settings ( IEncoder_Settings encoder , IDecoder_Settings decoder , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        //编码器
        _encoder = encoder;

        //解码器
        _decoder = decoder;

        CommandAction = new CommandAction ( true , methodOfCheckResponse );
    }

    /// <summary>
    /// 联机命令，在实例化本通讯类库后，必须先执行该方法
    /// </summary>
    /// <returns></returns>
    OperateResult<byte[ ]> IFuncSettings.HandShake ( )
    {
        //执行命令
        var actionResult = CommandAction. Action ( _encoder. Packet_HandShake ( ) );

        //执行命令失败则返回执行结果
        if ( !actionResult. IsSuccess || actionResult. Content == null )
        {
            return actionResult;
        }

        //如果执行命令成功则解码
        var decodeResult = _decoder. DecodeHandShake ( actionResult. Content );

        //如果解码失败
        if ( !decodeResult. IsSuccess )
        {
            actionResult. Message = decodeResult. Message;
        }
        return actionResult;
    }
    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetDeviceInfo ( char[ ] password , byte id , string sn )
    {
        return CommandAction. Action ( _encoder. Packet_SetDeviceInfo ( password , id , sn ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetBaudRate ( ushort baudRate )
    {
        return CommandAction. Action ( _encoder. Packet_SetBaudRate ( baudRate ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetSystemMode ( Enum SystemMode )
    {
        return CommandAction. Action ( _encoder. Packet_SetSystemMode ( SystemMode ) );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetDisplayPage ( Enum DisplayPage )
    {
        return CommandAction. Action ( _encoder. Packet_SetDisplayPage ( DisplayPage ) );
    }

    #region 《设备基本信息
    /// <summary>
    /// 设备型号
    /// </summary>
    public string? Model { get => _decoder. Model; set => _decoder. Model = value; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string? SN { get => _decoder. SN; set => _decoder. SN = value; }

    /// <summary>
    /// 固件版本号
    /// </summary>
    public string? Firmware { get => _decoder. Firmware; }

    /// <summary>
    /// 协议版本号
    /// </summary>
    public string? ProtocolVer { get => _decoder. ProtocolVer; set => _decoder. ProtocolVer = value; }

    #endregion 设备基本信息》

    #region 《基本功能 FuncB
    /// <summary>
    /// 指示交流源功能是否激活
    /// </summary>
    public bool IsEnabled_ACS { get; }

    /// <summary>
    /// 指示交流表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM { get; }

    /// <summary>
    /// 指示标准表钳表功能是否激活
    /// </summary>
    public bool IsEnabled_ACM_Cap { get; }

    /// <summary>
    /// 指示直流源功能是否激活
    /// </summary>
    public bool IsEnabled_DCS { get; }

    /// <summary>
    /// 辅助直流源是否激活
    /// </summary>
    public bool IsEnabled_DCS_AUX { get; }

    /// <summary>
    /// 指示直流表功能是否激活
    /// </summary>
    public bool IsEnabled_DCM { get; }

    /// <summary>
    /// 指示直流纹波表是否激活
    /// </summary>
    public bool IsEnabled_DCM_RIP { get; }


    /// <summary>
    /// 指示开关量功能是否激活
    /// </summary>
    public bool IsEnabled_IO { get; }

    /// <summary>
    /// 指示电能校验功能是否激活
    /// </summary>
    public bool IsEnabled_EPQ { get; }
    #endregion 基本功能 FuncB》

    #region 《特殊功能 FuncS 
    /// <summary>
    /// 指示双频输出功能是否激活
    /// </summary>
    public bool IsEnabled_DualFreqs { get; }

    /// <summary>
    /// 指示保护电流功能是否激活
    /// </summary>
    public bool IsEnabled_IProtect { get; }

    /// <summary>
    /// 指示闪变输出功能是否激活
    /// </summary>
    public bool IsEnabled_PST { get; }

    /// <summary>
    /// 指示遥信功能是否激活
    /// </summary>
    public bool IsEnabled_YX { get; }

    /// <summary>
    /// 指示高频输出功能是否激活
    /// </summary>
    public bool IsEnabled_HF { get; }

    /// <summary>
    /// 指示电机控制功能是否激活
    /// </summary>
    public bool IsEnabled_PWM { get; }

    /// <summary>
    /// 指示对时功能是否激活
    /// </summary>
    public bool IsEnabled_PPS { get; }

    #endregion 特殊功能 FuncS》
}
