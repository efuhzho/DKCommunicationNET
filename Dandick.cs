using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. BasicFramework;
using System. ComponentModel. DataAnnotations;
using DKCommunicationNET. Module;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克设备类
/// </summary>
public class Dandick : DandickSerialBase<RegularByteTransform>, IDeviceFunctions
{
    #region 【私有字段】
    readonly IProtocolFactory _protocolFactory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctions _Functions;

    /// <summary>
    /// CRC校验器
    /// </summary>
    private ICRCChecker _CRCChecker;

    /// <summary>
    /// 解码器
    /// </summary>
    private IDecoder _Decoder;  
    #endregion 【私有字段】

    #region 【构造函数】
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="model">枚举的设备型号（或协议类型），[找不到对应的设备型号？]：可按枚举中的协议类型实例化对象</param>
    /// <param name="id">设备ID,默认值为0，[可选参数]</param>
    public Dandick ( Models model , ushort id = 0 )
    {
        //设备ID初始化
        ID = id;

        //由抽象协议工厂根据客户选择的设备型号返回对应的实例。
        _protocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );

        _ACS = new ACS ( _protocolFactory , _SerialPort , CheckResponse );

        _CRCChecker = _protocolFactory. GetCRCChecker ( );
        _Functions = _protocolFactory. GetProtocolFunctions ( );
        _Decoder = _protocolFactory. GetDecoder ( ByteTransform );
    }
    #endregion 构造函数

    #region 【公共属性】

    #region 公共属性>>>设备信息

    /// <inheritdoc/>
    public string? Model { get; set; }

    /// <inheritdoc/>
    public string? SN { get; set; }

    /// <inheritdoc/>
    public string? Firmware { get; private set; }

    /// <inheritdoc/>
    public string? ProtocolVer { get; private set; }
    #endregion 公共属性>>>设备信息

    #region 公共属性>>>功能状态指示标志

    #region 公共属性>>>功能状态指示>>>FuncB
    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool IsEnabled_ACS { get ; private set; }

    /// <summary>
    /// 指示是否激活交流表功能
    /// </summary>
    public bool IsEnabled_ACM { get; private set; }

    /// <inheritdoc/>
    public bool IsEnabled_ACM_Cap { get; private set; }

    /// <summary>
    /// 指示是否激活直流源功能
    /// </summary>
    public bool IsEnabled_DCS { get; private set; }

    /// <inheritdoc/>
    public bool IsEnabled_DCS_AUX { get; private set; }

    /// <summary>
    /// 指示是否激活开关量功能
    /// </summary>
    public bool IsEnabled_IO { get; private set; }

    /// <summary>
    /// 指示是否激活电能功能
    /// </summary>
    public bool IsEnabled_EPQ { get; private set; }

    /// <summary>
    /// 指示是否激活直流表功能
    /// </summary>
    public bool IsEnabled_DCM { get; private set; }

    /// <inheritdoc/>
    public bool IsEnabled_DCM_RIP { get; private set; }
    #endregion 公共属性>>>功能状态指示>>>FuncB

    #region 公共属性>>>功能状态指示>>>FuncS

    /// <summary>
    /// 指示是否激活双频输出功能
    /// </summary>
    public bool IsEnabled_DualFreqs { get; private set; }

    /// <summary>
    /// 指示是否激活保护电流功能
    /// </summary>
    public bool IsEnabled_IProtect { get; private set; }

    /// <summary>
    /// 指示是否激活闪变输出功能
    /// </summary>
    public bool IsEnabled_PST { get; private set; }

    /// <summary>
    /// 指示是否激活遥信功能
    /// </summary>
    public bool IsEnabled_YX { get; private set; }

    /// <summary>
    /// 指示是否激活高频输出功能
    /// </summary>
    public bool IsEnabled_HF { get; private set; }

    /// <summary>
    /// 指示是否激活电机控制功能
    /// </summary>
    public bool IsEnabled_PWM { get; private set; }

    /// <inheritdoc/>
    public bool IsEnabled_PPS { get; private set; }
    #endregion 公共属性>>>功能状态指示>>>FuncS

    #endregion 公共属性>>>功能状态指示

    #region 公共属性>>>功能
    private ACS _ACS;
    /// <summary>
    /// <inheritdoc cref="Module.ACS"/>
    /// </summary>
    public ACS ACS
    {
        get { CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForACS , IsEnabled_ACS); return _ACS; }
        set { _ACS = value; }
    }


    #endregion 公共属性>>>功能

    #endregion 【公共属性】

    #region 【功能状态初始化】

    /// <inheritdoc/>   
    public override OperateResult<byte[ ]> HandShake ( )
    {
        OperateResult<byte[ ]> res = CommandAction. Action ( _Functions. GetPacketOfHandShake , CheckResponse );
        _Decoder. DecodeHandShake ( res );
        Model = _Decoder. Model;
        Firmware = _Decoder. Firmware;
        ProtocolVer = _Decoder. ProtocolVer;
        SN = _Decoder. SN;
        IsEnabled_ACS = _Decoder. IsEnabled_ACS;
        IsEnabled_ACM = _Decoder. IsEnabled_ACM;
        IsEnabled_ACM_Cap = _Decoder. IsEnabled_ACM_Cap;
        IsEnabled_DCS = _Decoder. IsEnabled_DCS;
        IsEnabled_DCS_AUX = _Decoder. IsEnabled_DCS_AUX;
        IsEnabled_DCM = _Decoder. IsEnabled_DCM;
        IsEnabled_DCM_RIP = _Decoder. IsEnabled_DCM_RIP;
        IsEnabled_EPQ = _Decoder. IsEnabled_EPQ;
        IsEnabled_IO = _Decoder. IsEnabled_IO;
        IsEnabled_DualFreqs = _Decoder. IsEnabled_DualFreqs;
        IsEnabled_IProtect = _Decoder. IsEnabled_IProtect;
        IsEnabled_PST = _Decoder. IsEnabled_PST;
        IsEnabled_YX = _Decoder. IsEnabled_YX;
        IsEnabled_HF = _Decoder. IsEnabled_HF;
        IsEnabled_PWM = _Decoder. IsEnabled_PWM;
        IsEnabled_PPS = _Decoder. IsEnabled_PPS;
        return res;
    }
    #endregion 【功能状态初始化】

    #region 【Core Interative 核心交互】
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文。
    /// </summary>
    /// <param name="send">发送的报文</param>
    /// <returns></returns>
    private OperateResult<byte[ ]> CheckResponse ( byte[ ] send )
    {
        // 发送报文并获取回复报文
        OperateResult<byte[ ]> response = ReadBase ( send );

        //获取回复不成功
        if ( !response. IsSuccess )
        {
            return response;
        }

        // 长度校验
        if ( response. Content. Length < 7 )
        {
            return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , StringResources. Language. ReceiveDataLengthTooShort );
        }

        // 检查CRC:CheckCRC包含报文头验证
        if ( !_CRCChecker. CheckCRC ( response. Content ) )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. CRCCheckFailed + SoftBasic. ByteToHexString ( response. Content , ' ' ) );
        }

        return response;
    }
    /// <inheritdoc/>
    #endregion 【Core Interative 核心交互】
}





