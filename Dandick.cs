using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;
using DKCommunicationNET. BasicFramework;

namespace DKCommunicationNET;

public class Dandick : DandickSerialBase<RegularByteTransform>, IModuleACS
{
    #region 【私有字段】

    IPacketFactory _factory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctions? _Functions;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private IPacketsOfACS? _PacketsOfACS;

    /// <summary>
    /// 定义直流源模块对象
    /// </summary>
    private IModuleDCS? _ModuleDCS;

    /// <summary>
    /// 定义直流表模块对象
    /// </summary>
    private IModuleDCM? _ModuleDCM;

    /// <summary>
    /// 定义电能模块模块对象
    /// </summary>
    private IModulePQ? _ModulePQ;

    /// <summary>
    /// 定义开关量模块对象
    /// </summary>
    private IModuleIO? _ModuleIO;

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
    /// 直流源模块
    /// </summary>
    private IModuleDCS ModuleDCS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions. IsDCSModuleSupported , IsDCSModuleEnabled );
            return _ModuleDCS;
        }
    }

    /// <summary>
    /// 直流表模块
    /// </summary>
    private IModuleDCM ModuleDCM
    {
        get
        {
            if ( IsDCMModuleEnabled )
            {
                return _ModuleDCM;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
    }


    /// <summary>
    /// 电能模块
    /// </summary>
    private IModulePQ ModulePQ
    {
        get
        {
            if ( IsPQModuleEnabled )
            {
                return _ModulePQ;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
    }

    /// <summary>
    /// 开关量模块
    /// </summary>
    private IModuleIO ModuleIO
    {
        get
        {
            if ( IsIOModuleEnabled )
            {
                return _ModuleIO;
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
        _factory = new DictionaryOfFactorys ( ). GetFactory ( model );
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

        _PacketsOfACS = _factory. GetPacketsOfACS ( );
        //_ModuleDCS = _factory. GetModuleDCS ( ). Content;
        //_ModuleDCM = _factory. GetModuleDCM ( ). Content;
        //_ModuleIO = _factory. GetModuleIO ( ). Content;
        //_ModulePQ = _factory. GetModulePQ ( ). Content;

        _Functions = Model switch //TODO 看视频如何解决这个问题
        {
            Models. Hex81 => new Hex81Functions ( ),
            Models. Hex5A => new Hex5AFunctions ( ),
            _ => new Hex81Functions ( ),
        };

        //SystemSettings = new SystemSettings ( model );
        //SystemSettings. SystemMode = new SystemMode ( );

    }
    #endregion 设备【功能状态使能】初始化

    #region 【Public Methods】
    #region Public Methods ==> [ACS]

    public OperateResult<byte[ ]> GetRanges ( )
    {
        return _PacketsOfACS. PacketOfGetRanges ( );
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
    private  OperateResult<byte[ ]> CheckResponse ( byte[ ] send )
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
            return new OperateResult<byte[ ]> ( 811300 , StringResources. Language. ReceiveDataLengthTooShort + "811300" );
        }

        // 检查crc
        if ( !DK81CommunicationInfo. CheckCRC ( response. Content ) )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. CRCCheckFailed + SoftBasic. ByteToHexString ( response. Content , ' ' ) );
        }

        //回复OK
        if ( response. Content[5] == DK81CommunicationInfo. OK && response. Content[6] == send[5] )
        {
            return response;
        }

        // 检查是否报故障：是     //TODO 随时主动报故障的问题
        if ( response. Content[5] == DK81CommunicationInfo. ErrorCode )
        {
            return new OperateResult<byte[ ]> ( response. Content[6] , ( ( ErrorCode ) response. Content[6] ). ToString ( ) ); //TODO 测试第二种故障码解析:/*DK81CommunicationInfo.GetErrorMessageByErrorCode(response.Content[6])*/
        }

        //检查命令码：命令码不一致且不是OK命令
        if ( send[5] != response. Content[5] )
        {
            return new OperateResult<byte[ ]> ( response. Content[5] , $"Receive CommandCode Check Failed:SendCode is {send[5]},ReceivedCode is {response. Content[5]}" );
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



