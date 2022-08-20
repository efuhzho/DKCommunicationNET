using DKCommunicationNET. BaseClass;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克设备类
/// </summary>
public class Dandick : DandickSerialBase<RegularByteTransform>, IModuleACS, IDeviceFunctions
{
    #region 【私有字段】

    readonly IProtocolFactory _ProtocolFactory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctions _Functions;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private IPacketsBuilderOfACS? _PacketsOfACS;

    /// <summary>
    /// 定义交流表模块对象
    /// </summary>
    private IPacketBuilderOfACM? _PacketOfACM;

    /// <summary>
    /// 定义直流源模块对象
    /// </summary>
    private IPacketBuilderOfDCS? _PacketOfDCS;

    /// <summary>
    /// 定义直流表模块对象
    /// </summary>
    private IPacketBuilderOfDCM? _PacketOfDCM;

    /// <summary>
    /// 定义电能模块模块对象
    /// </summary>
    private IPacketBuilderOfPQ? _PacketOfPQ;

    /// <summary>
    /// 定义开关量模块对象
    /// </summary>
    private IPacketBuilderOfIO? _PacketOfIO;

    /// <summary>
    /// CRC校验器
    /// </summary>
    private ICRCChecker _CRCChecker;

    /// <summary>
    /// 解码器
    /// </summary>
    private IDecoder _Decoder;

    /// <summary>
    /// 交流源模块
    /// </summary>
    private IPacketsBuilderOfACS PacketsOfACS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForACS , IsEnabled_ACS );
            return _PacketsOfACS;
        }
    }

    /// <summary>
    /// 交流表模块
    /// </summary>
    private IPacketBuilderOfACM PacketOfACM
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForACS , IsEnabled_ACM );
            return _PacketOfACM;
        }
    }

    /// <summary>
    /// 直流源模块
    /// </summary>
    private IPacketBuilderOfDCS PacketOfDCS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForDCS , IsEnabled_DCS );
            return _PacketOfDCS;
        }
    }

    /// <summary>
    /// 直流表模块
    /// </summary>
    private IPacketBuilderOfDCM PacketOfDCM
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForDCM , IsEnabled_DCM );
            return _PacketOfDCM;
        }
    }


    /// <summary>
    /// 电能模块
    /// </summary>
    private IPacketBuilderOfPQ PacketOfPQ
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForPQ , IsEnabled_EPQ );
            return _PacketOfPQ;
        }
    }

    /// <summary>
    /// 开关量模块
    /// </summary>
    private IPacketBuilderOfIO PacketOfIO
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForIO , IsEnabled_IO );
            return _PacketOfIO;
        }
    }

    #endregion 私有字段

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
        _ProtocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );

        _PacketsOfACS = _ProtocolFactory. GetPacketsOfACS (id ). Content;
        _PacketOfACM = _ProtocolFactory. GetPacketsOfACM ( id ). Content;
        _PacketOfDCS = _ProtocolFactory. GetPacketsOfDCS ( id ). Content;
        _PacketOfDCM = _ProtocolFactory. GetPacketsOfDCM ( id ). Content;
        _PacketOfIO = _ProtocolFactory. GetPacketsOfIO (id ). Content;
        _PacketOfPQ = _ProtocolFactory. GetPacketsOfPQ (id ). Content;
        _CRCChecker = _ProtocolFactory. GetCRCChecker ( );
        _Functions = _ProtocolFactory. GetProtocolFunctionsState ( );
        _Decoder = _ProtocolFactory. GetDecoder ( ByteTransform );
    }
    #endregion 构造函数


    #region 【公共属性】

    #region 公共属性>>>【设备信息】

    /// <inheritdoc/>
    public string? Model { get; set; }

    /// <inheritdoc/>
    public string? SN { get; set; }

    /// <inheritdoc/>
    public string? Firmware { get; private set; }

    /// <inheritdoc/>
    public string? ProtocolVer { get; private set; }
    #endregion 公共属性>>>【设备信息】

    #region 公共属性>>>【功能状态指示标志】
    #region FuncB
    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool IsEnabled_ACS { get; private set; }

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
    #endregion FuncB

    #region FuncS

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
    #endregion FuncS
    #endregion 公共属性>>>【功能状态指示标志】

    #region 公共属性>>>【交流源】
    /// <inheritdoc/>
    public float Range_ACU => throw new NotImplementedException ( );
    /// <inheritdoc/>
    public float Range_ACI => throw new NotImplementedException ( );
    /// <inheritdoc/>
    public float Range_IProtect => throw new NotImplementedException ( );
    /// <inheritdoc/>
    public float[ ] ACU_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float[ ] ACI_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float[ ] IProtect_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum WireMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum CloseLoopMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum HarmonicMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Freq { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Freq_C { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public byte HarmonicCount { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public Enum HarmonicChannels { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public HarmonicArgs[ ] Harmonics { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float IPC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float FAI_IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float PC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float P { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float QC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float Q { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    /// <inheritdoc/>
    public float SA { get; private set; }
    /// <inheritdoc/>
    public float SB { get; private set; }
    /// <inheritdoc/>
    public float SC { get; private set; }
    /// <inheritdoc/>
    public float S { get; private set; }
    /// <inheritdoc/>
    public float PFA { get; private set; }
    /// <inheritdoc/>
    public float PFB { get; private set; }
    /// <inheritdoc/>
    public float PFC { get; private set; }
    /// <inheritdoc/>
    public float PF { get; private set; }
    /// <inheritdoc/>
    public byte Flag_A { get; private set; }
    /// <inheritdoc/>
    public byte Flag_B { get; private set; }
    /// <inheritdoc/>
    public byte Flag_C { get; private set; }

    byte IModuleACS.RangesCount_ACU => throw new NotImplementedException ( );

    byte IModuleACS.RangesCount_ACI => throw new NotImplementedException ( );

    int IModuleACS.RangeIndex_ACU { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    int IModuleACS.RangeIndex_ACI { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    int IModuleACS.RangeIndex_IProtect { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    byte IModuleACS.RangesCount_IProtect => throw new NotImplementedException ( );

    byte IModuleACS.URanges_Asingle => throw new NotImplementedException ( );

    byte IModuleACS.IRanges_Asingle => throw new NotImplementedException ( );

    byte IModuleACS.IProtectRanges_Asingle => throw new NotImplementedException ( );



    #endregion 公共属性>>>【交流源】

    #endregion 【公共属性】


    #region 【功能状态使能】初始化

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
        IsEnabled_ACM_Cap= _Decoder. IsEnabled_ACM_Cap;
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


    #endregion 设备【功能状态使能】初始化


    #region --------------------------------- Core Interative 核心交互-------------------------
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
    #endregion Core Interative 核心交互

    #region Public Methods ==> [ACS]


    public OperateResult<byte[ ]> GetRangesOfACS ( )
    {
        return CommandAction. Action ( PacketsOfACS. PacketOfGetRanges , CheckResponse );
    }

    /// <inheritdoc/>
    public OperateResult<byte[ ]> SetAmplitudeOfACS ( float amplitude )
    {
        throw new NotImplementedException ( );

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc cref="IModuleACS.OpenACS"/></returns>
    /// <exception cref="NotImplementedException" >description</exception>
    public OperateResult<byte[ ]> OpenACS ( )
    {
        throw new NotImplementedException ( "meiyou此功能" );

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc/></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OperateResult<byte[ ]> CloseACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetWireMode ( Enum wireMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetClosedLoop ( Enum ClosedLoopMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetHarmonicMode ( Enum HarmonicMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> WriteHarmonics ( Enum harmonicChannels , HarmonicArgs[ ] harmonicArgs )
    {
        throw new NotImplementedException ( );
    }



    #endregion Public Methods ==> [ACS]

}





