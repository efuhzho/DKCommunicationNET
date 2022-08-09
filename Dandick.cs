using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Core;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocols;
using DKCommunicationNET. Protocols. Hex5A;
using DKCommunicationNET. Protocols. Hex81;

namespace DKCommunicationNET;

public class Dandick : DandickSerialBase<RegularByteTransform>
{
    #region 【私有字段】

    IDandickFactory _factory;

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private IProtocolFunctions? _Functions = null;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private IModuleACS? _ModuleACS = null;

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
    public IModuleACS ModuleACS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions.IsACSModuleSupported , IsACSModuleEnabled );
            return _ModuleACS;
        }
    }

    /// <summary>
    /// 直流源模块
    /// </summary>
    public IModuleDCS ModuleDCS
    {
        get
        {
            CheckFunctionsStatus. CheckFunctionsState ( _Functions.IsDCSModuleSupported , IsDCSModuleEnabled );

            return _ModuleDCS;
          
        }
    }

    /// <summary>
    /// 直流表模块
    /// </summary>
    public IModuleDCM ModuleDCM
    {
        get
        {
            if ( IsDCMModuleEnabled )
            {
                return _ModuleDCM;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
        //set { _ModuleDCM = value; }
    }


    /// <summary>
    /// 电能模块
    /// </summary>
    public IModulePQ ModulePQ
    {
        get
        {
            if ( IsPQModuleEnabled )
            {
                return _ModulePQ;
            }
            throw new Exception ( StringResources. Language. NotSupportedModule + Model );
        }
        //set { _ModulePQ = value; }
    }

    /// <summary>
    /// 开关量模块
    /// </summary>
    public IModuleIO ModuleIO
    {
        get
        {
            if ( IsIOModuleEnabled )
            {
                return _ModuleIO;
            }
            throw new Exception ( StringResources. Language. NotEnabledModule + $"型号【{Model}】，编号【{SN}】" );
        }
        //set { _ModuleIO = value; }
    }
    #endregion 公共属性==>功能模块

    #region 公共属性==>[系统设置]
    public ISystemSettings? SystemSettings { get; private set; }
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

        _ModuleACS = _factory. GetModuleACS ( ). Content;
        _ModuleDCS = _factory. GetModuleDCS ( ). Content;
        _ModuleDCM = _factory. GetModuleDCM ( ). Content;
        _ModuleIO = _factory. GetModuleIO ( ). Content;
        _ModulePQ = _factory. GetModulePQ ( ). Content;

        _Functions = Model switch //TODO 看视频如何解决这个问题
        {
            Models. Hex81 => new Hex81Functions ( ),
            Models. Hex5A => new Hex5AFunctions ( ),
            _ => new Hex81Functions ( ),
        };

        //SystemSettings = new SystemSettings ( model );
        //SystemSettings. SystemMode = new SystemMode ( );

        //IsACSModuleEnabled = _Functions. IsACSModuleSupported;
        //IsDCSModuleEnabled = _Functions. IsDCSModuleSupported;
        //IsDCMModuleEnabled = _Functions. IsDCMModuleSupported;
        //IsIOModuleEnabled = _Functions. IsIOModuleSupported;
        //IsPQModuleEnabled = _Functions. IsPQModuleSupported;
    }

    #endregion 设备【功能状态使能】初始化

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



