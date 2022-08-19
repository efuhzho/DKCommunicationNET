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
    #region FuncB

    /// <summary>
    /// 指示是否激活交流源功能
    /// </summary>
    public bool IsEnabled_ACS { get; private set; } 

    /// <summary>
    /// 指示是否激活交流表功能
    /// </summary>
    public bool IsEnabled_ACM { get; private set; }

    /// <summary>
    /// 指示是否激活直流源功能
    /// </summary>
    public bool IsEnabled_DCS { get; private set; }

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
    #endregion

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
    #endregion

    #endregion 公共属性==>功能状态指示标志

    #region 公共属性==>[功能模块]
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
    #endregion 公共属性==>功能模块

    #region 公共属性==>[系统设置]

    public ISystemSettings? SystemSettings { get; private set; }
   

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
        IsEnabled_ACS=_Decoder. IsEnabled_ACS;
        IsEnabled_ACM=_Decoder. IsEnabled_ACM;
        IsEnabled_DCS= _Decoder. IsEnabled_DCS;
        IsEnabled_DCM= _Decoder. IsEnabled_DCM;
        IsEnabled_EPQ= _Decoder. IsEnabled_EPQ;
        IsEnabled_IO= _Decoder. IsEnabled_IO;
        IsEnabled_DualFreqs= _Decoder. IsEnabled_DualFreqs;
        IsEnabled_IProtect= _Decoder. IsEnabled_IProtect;
        IsEnabled_PST= _Decoder. IsEnabled_PST;
        IsEnabled_YX= _Decoder. IsEnabled_YX;
        IsEnabled_HF= _Decoder. IsEnabled_HF;
        IsEnabled_PWM= _Decoder. IsEnabled_PWM;
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



