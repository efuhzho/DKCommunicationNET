using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. BasicFramework;
using System. ComponentModel. DataAnnotations;

namespace DKCommunicationNET;

/// <summary>
/// 丹迪克设备类
/// </summary>
public class Dandick : DandickSerialBase<RegularByteTransform>, IModuleACS
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

    private ICRCChecker _CRCChecker;

    private IDecoder _Decoder;

    #endregion 私有字段

    #region 【公共属性】[功能状态指示标志][功能模块][系统设置]

    #region 公共属性==>[功能状态指示标志]
    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool IsACSModuleEnabled { get; set; } 

    /// <summary>
    /// 指示是否激活交流表功能
    /// </summary>
    public bool IsACMModuleEnabled { get; set; }

    /// <summary>
    /// 指示是否激活直流源功能
    /// </summary>
    public bool IsDCSModuleEnabled { get; set; }

    /// <summary>
    /// 指示是否激活开关量功能
    /// </summary>
    public bool IsIOModuleEnabled { get; set; }

    /// <summary>
    /// 指示是否激活电能功能
    /// </summary>
    public bool IsPQModuleEnabled { get; set; }

    /// <summary>
    /// 指示是否激活直流表功能
    /// </summary>
    public bool IsDCMModuleEnabled { get; set; }
    #endregion 公共属性==>功能状态指示标志

    #region 公共属性==>[功能模块]
    /// <summary>
    /// 交流源模块
    /// </summary>
    private IPacketsBuilderOfACS PacketsOfACS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForACS , IsACSModuleEnabled );
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
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForACS , IsACMModuleEnabled );
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
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForDCS , IsDCSModuleEnabled );
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
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForDCM , IsDCMModuleEnabled );
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
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForPQ , IsPQModuleEnabled );
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
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsSupportedForIO , IsIOModuleEnabled );
            return _PacketOfIO;
        }
    }
    #endregion 公共属性==>功能模块

    #region 公共属性==>[系统设置]

    public ISystemSettings? SystemSettings { get; private set; }
    public bool IsEnabled { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    #endregion 公共属性==>[系统设置]

    #endregion 【公共属性】

    #region 【构造函数】

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="model">枚举的设备型号（或协议类型），[找不到对应的设备型号？]：可按枚举中的协议类型实例化对象</param>
    /// <param name="id">设备ID,默认值为0，[可选参数]</param>
    public Dandick ( Models model , ushort id = 0 )
    {
        _ProtocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );
        FunctionsInitializer ( );
        ID = id;
    }
    #endregion 构造函数

    #region 【功能状态使能】初始化

    /// <summary>
    /// 功能状态初始化器
    /// </summary>
    void FunctionsInitializer ( )
    {
        //
        _PacketsOfACS = _ProtocolFactory. GetPacketsOfACS ( ). Content;
        _PacketOfACM = _ProtocolFactory. GetPacketsOfACM ( ). Content;
        _PacketOfDCS = _ProtocolFactory. GetPacketsOfDCS ( ). Content;
        _PacketOfDCM = _ProtocolFactory. GetPacketsOfDCM ( ). Content;
        _PacketOfIO = _ProtocolFactory. GetPacketsOfIO ( ). Content;
        _PacketOfPQ = _ProtocolFactory. GetPacketsOfPQ ( ). Content;
        _CRCChecker = _ProtocolFactory. GetCRCChecker ( );
        _Functions = _ProtocolFactory. GetProtocolFunctionsState ( );
        _Decoder = _ProtocolFactory. GetDecoder ( ByteTransform );
    }

    /// <inheritdoc/>   
    public override OperateResult<byte[ ]> HandShake ( )
    {
        OperateResult<byte[ ]> res = CommandAction. Action ( _Functions. GetPacketOfHandShake , CheckResponse );
        _Decoder. DecodeHandShake ( res );
        Model = _Decoder. Model;
        Fireware = _Decoder. Firmware;
        SN = _Decoder. SN;

        return res;
    }


    #endregion 设备【功能状态使能】初始化

    #region 【Public Methods】

    #endregion 【Public Methods】

    #region --------------------------------- Core Interative 核心交互-------------------------
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



    #endregion Public Methods ==> [ACS]

}

internal class CheckModuleState
{
    // throw new Exception ( StringResources. Language. NotEnabledModule + $"型号【{Model}】，编号【{SN}】" );
    public static Dictionary<int , string> EnumNamedValues<T> ( ) where T : System. Enum
    {
        var result = new Dictionary<int , string> ( );
        var values = Enum. GetValues ( typeof ( T ) );

        foreach ( int item in values )
            result. Add ( item , Enum. GetName ( typeof ( T ) , item )! );
        return result;
    }
    public void Do ( )
    {

        var map = EnumNamedValues<Models> ( );

        foreach ( var pair in map )
        {

        }


    }

}



