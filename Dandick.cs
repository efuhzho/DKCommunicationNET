using DKCommunicationNET. BaseClass;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using DKCommunicationNET. Module;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. ModulesAndFunctions. Functions;
using System. Security. Cryptography;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克设备类:版本Ver2分组架构 2022年8月25日01点56分。第二次修改
/// </summary>
public class Dandick : DandickSerialBase<RegularByteTransform>, IDeviceFunctions
{
    #region 【私有字段】
    readonly IProtocolFactory _protocolFactory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    readonly IProtocolFunctions _prodocolFunctions;

    /// <summary>
    /// CRC校验器
    /// </summary>
    readonly ICRCChecker _CRCChecker;

    /// <summary>
    /// 解码器
    /// </summary>
    public IDecoder Decoder { get; }

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IPacketsBuilder_ACS? _packetsBuilder_ACS;
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

        //由抽象协议工厂根据客户选择的设备型号返回对应的协议工厂实例。
        _protocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );

        //初始化CRC校验器
        _CRCChecker = _protocolFactory. GetCRCChecker ( );

        //初始化当前协议（设备型号）所支持的功能标志
        _prodocolFunctions = _protocolFactory. GetProtocolFunctions ( );

        _packetsBuilder_ACS = _protocolFactory. GetPacketBuilderOfACS ( ID , ByteTransform ). Content;

        //初始化解码器
        Decoder = _protocolFactory. GetDecoder ( ByteTransform );

        _packetsBuilder_ACS = _protocolFactory. GetPacketBuilderOfACS ( ID , ByteTransform ). Content;

        ACS = new ( _packetsBuilder_ACS , Decoder , CheckResponse , true );
    }
    #endregion 构造函数

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

    #region 公共属性>>>功能模块

    /// <summary>
    /// <inheritdoc cref="Module.ACS"/>
    /// </summary>
    public ACS ACS { get; }

    /// <summary>
    /// <inheritdoc cref="Module.DCS"/>
    /// </summary>
    public DCS DCS => new ( ID , _protocolFactory , CheckResponse , ByteTransform , IsEnabled_DCS );

    /// <summary>
    /// [警告:错误使用此功能将可能导致严重的后果]
    /// </summary>
    public Calibrate Calibrate => new Calibrate ( ID , _protocolFactory , CheckResponse , ByteTransform , true );

    #endregion 公共属性>>>功能模块

    #region 公共属性>>>功能状态指示>>>FuncB

    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool _IsEnabled_ACS;

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

    bool IDeviceFunctions.IsEnabled_ACS => throw new NotImplementedException ( );

    #endregion 公共属性>>>功能状态指示>>>FuncS    

    #region 【功能状态初始化】

    /// <inheritdoc/>   
    public override OperateResult<byte[ ]> HandShake ( )
    {
        OperateResult<byte[ ]> res = CommandAction. Action ( _prodocolFunctions. GetPacketOfHandShake ( ) , CheckResponse );
        Decoder. DecodeHandShake ( res );
        Model = Decoder. Model;
        Firmware = Decoder. Firmware;
        ProtocolVer = Decoder. ProtocolVer;
        SN = Decoder. SN;
        _IsEnabled_ACS = Decoder. IsEnabled_ACS;
        IsEnabled_ACM = Decoder. IsEnabled_ACM;
        IsEnabled_ACM_Cap = Decoder. IsEnabled_ACM_Cap;
        IsEnabled_DCS = Decoder. IsEnabled_DCS;
        IsEnabled_DCS_AUX = Decoder. IsEnabled_DCS_AUX;
        IsEnabled_DCM = Decoder. IsEnabled_DCM;
        IsEnabled_DCM_RIP = Decoder. IsEnabled_DCM_RIP;
        IsEnabled_EPQ = Decoder. IsEnabled_EPQ;
        IsEnabled_IO = Decoder. IsEnabled_IO;
        IsEnabled_DualFreqs = Decoder. IsEnabled_DualFreqs;
        IsEnabled_IProtect = Decoder. IsEnabled_IProtect;
        IsEnabled_PST = Decoder. IsEnabled_PST;
        IsEnabled_YX = Decoder. IsEnabled_YX;
        IsEnabled_HF = Decoder. IsEnabled_HF;
        IsEnabled_PWM = Decoder. IsEnabled_PWM;
        IsEnabled_PPS = Decoder. IsEnabled_PPS;
        return res;
    }
    #endregion 【功能状态初始化】

    #region 【Core Interative 核心交互】
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文。
    /// </summary>
    /// <param name="send">发送的报文</param>
    /// <param name="awaitData"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> CheckResponse ( byte[ ] send , bool awaitData = true )
    {
        // 发送报文并获取回复报文
        OperateResult<byte[ ]> response = ReadBase ( send , awaitData );

        //获取回复不成功
        if ( !response. IsSuccess || response. Content == null )
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

    #endregion 【Core Interative 核心交互】
}





