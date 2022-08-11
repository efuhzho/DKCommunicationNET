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

    IProtocolFactory _PacketFactory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctionsState? _Functions;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private IPacketsOfACS? _PacketsOfACS;

    /// <summary>
    /// 定义交流表模块对象
    /// </summary>
    private IPacketOfACM? _PacketOfACM;

    /// <summary>
    /// 定义直流源模块对象
    /// </summary>
    private IPacketOfDCS? _PacketOfDCS;

    /// <summary>
    /// 定义直流表模块对象
    /// </summary>
    private IPacketOfDCM? _PacketOfDCM;

    /// <summary>
    /// 定义电能模块模块对象
    /// </summary>
    private IPacketOfPQ? _PacketOfPQ;

    /// <summary>
    /// 定义开关量模块对象
    /// </summary>
    private IPacketOfIO? _PacketOfIO;

    private ICRCChecker? _CRCChecker;

    #endregion 私有字段

    #region 【公共属性】[功能状态指示标志][功能模块][系统设置]

    #region 公共属性==>[功能状态指示标志]
    /// <summary>
    /// 是否装配交流源模块
    /// </summary>
    public bool IsACSModuleEnabled { get; set; }

    /// <summary>
    /// 是否装配直流源模块
    /// </summary>
    public bool IsDCSModuleEnabled { get; set; } = true;

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
    private IPacketsOfACS PacketsOfACS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsACSModuleSupported , IsACSModuleEnabled );
            return _PacketsOfACS;
        }
    }

    /// <summary>
    /// 交流表模块
    /// </summary>
    private IPacketOfACM PacketOfACM
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsDCSModuleSupported , IsDCSModuleEnabled );
            return _PacketOfACM;
        }
    }

    /// <summary>
    /// 直流源模块
    /// </summary>
    private IPacketOfDCS PacketOfDCS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsDCSModuleSupported , IsDCSModuleEnabled );
            return _PacketOfDCS;
        }
    }

    /// <summary>
    /// 直流表模块
    /// </summary>
    private IPacketOfDCM PacketOfDCM
    {
        get
        {
            if ( IsDCMModuleEnabled )
            {
                return _PacketOfDCM;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
    }


    /// <summary>
    /// 电能模块
    /// </summary>
    private IPacketOfPQ PacketOfPQ
    {
        get
        {
            if ( IsPQModuleEnabled )
            {
                return _PacketOfPQ;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
    }

    /// <summary>
    /// 开关量模块
    /// </summary>
    private IPacketOfIO PacketOfIO
    {
        get
        {
            if ( IsIOModuleEnabled )
            {
                return _PacketOfIO;
            }
            throw new Exception ( StringResources. Language. NotEnabledModule + $"型号【{Model}】，编号【{SN}】" );
        }
    }
    #endregion 公共属性==>功能模块

    #region 公共属性==>[系统设置]
    public ISystemSettings? SystemSettings { get; private set; }
    public bool IsEnabled { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    #endregion 公共属性==>[系统设置]

    #endregion 【公共属性】

    #region 【构造函数】

    public Dandick ( Models model )
    {
        _PacketFactory = new DictionaryOfFactorys ( ). GetFactory ( model );
        Model = model;
        FunctionsInitializer ( );

    }
    #endregion 构造函数

    #region 【功能状态使能】初始化

    /// <summary>
    /// 功能状态初始化器
    /// </summary>
    void FunctionsInitializer ( )
    {

        _PacketsOfACS = _PacketFactory. GetPacketsOfACS ( );
        _PacketOfACM = _PacketFactory. GetPacketsOfACM ( );
        _PacketOfDCS = _PacketFactory. GetPacketsOfDCS ( );
        _PacketOfDCM = _PacketFactory. GetPacketsOfDCM ( );
        _PacketOfIO = _PacketFactory. GetPacketsOfIO ( );
        _PacketOfPQ = _PacketFactory. GetPacketsOfPQ ( );
        _CRCChecker = _PacketFactory. GetCRCChecker ( );

        _Functions = Model switch //TODO 看视频如何解决这个问题
        {
            Models. Hex81 => new Hex81FunctionsState ( ),
            Models. Hex5A => new Hex5AFunctionsState ( ),
            _ => new Hex81FunctionsState ( ),
        };

        //SystemSettings = new SystemSettings ( model );
        //SystemSettings. SystemMode = new SystemMode ( );

    }
    #endregion 设备【功能状态使能】初始化

    #region 【Public Methods】
    #region Public Methods ==> [ACS]

    public OperateResult<byte[ ]> GetRanges ( )
    {
        return CommandAction. Action ( _PacketsOfACS. PacketOfGetRanges , CheckResponse );
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
    #endregion
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



