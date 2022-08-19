using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. BasicFramework;

namespace DKCommunicationNET;

public class Dandick : DandickSerialBase<RegularByteTransform>, IModuleACS
{
    #region 【私有字段】

    IProtocolFactory _ProtocolFactory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctionsState? _Functions;

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

    private ICRCChecker? _CRCChecker;

    #endregion 私有字段

    #region 【公共属性】[功能状态指示标志][功能模块][系统设置]

    #region 公共属性==>[功能状态指示标志]
    /// <summary>
    /// 是否装配交流源模块
    /// </summary>
    public bool IsACSModuleEnabled { get; set; } = true;

    public bool IsACMModuleEnabled { get; set; } 

    /// <summary>
    /// 是否装配直流源模块
    /// </summary>
    public bool IsDCSModuleEnabled { get; set; }

    /// <summary>
    /// 是否装配开关量模块
    /// </summary>
    public bool IsIOModuleEnabled { get; set; }

    /// <summary>
    /// 是否装配电能模块
    /// </summary>
    public bool IsPQModuleEnabled { get; set; }

    /// <summary>
    /// 是否装配直流表模块
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

    public Dandick ( Models model , ushort id = 0 )
    {
        _ProtocolFactory = new DictionaryOfFactorys ( ). GetFactory ( model );
        FunctionsInitializer ( );
        Model = model;
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
        _PacketsOfACS = _ProtocolFactory. GetPacketsOfACS ( ).Content;
        _PacketOfACM = _ProtocolFactory. GetPacketsOfACM ( ).Content;
        _PacketOfDCS = _ProtocolFactory. GetPacketsOfDCS ( ).Content;
        _PacketOfDCM = _ProtocolFactory. GetPacketsOfDCM ( ).Content;
        _PacketOfIO = _ProtocolFactory. GetPacketsOfIO ( ).Content;
        _PacketOfPQ = _ProtocolFactory. GetPacketsOfPQ ( ).Content;
        _CRCChecker = _ProtocolFactory. GetCRCChecker ( );
        _Functions = _ProtocolFactory. GetProtocolFunctionsState ( );


        //SystemSettings = new SystemSettings ( model );
        //SystemSettings. SystemMode = new SystemMode ( );

    }

    public override OperateResult<byte[ ]> HandShake ( )
    {
        return CommandAction. Action ( _Functions. GetPacketOfHandShake , CheckResponse );
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
    #endregion Core Interative 核心交互

    #region Public Methods ==> [ACS]


    public OperateResult<byte[ ]> GetRanges ( )
    {
        return CommandAction. Action ( PacketsOfACS. PacketOfGetRanges , CheckResponse );
    }

    public OperateResult<byte[ ]> SetAmplitude ( float amplitude )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.Open ( )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.Close ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
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

    public OperateResult<byte[ ]> SetWireMode ( string wireMode )
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



