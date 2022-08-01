using DKCommunicationNET. BaseClass;
using DKCommunicationNET. ProtocolFunctions;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. Language;
using System. Collections. Generic;
using System. Collections;
using DKCommunicationNET. ProtocolInformation;
using DKCommunicationNET. Interface. IModule;

namespace DKCommunicationNET;

public class Dandick : DandickSerialBase
{
    #region 【私有字段】

    /// <summary>
    /// 定义协议所支持的功能对象
    /// </summary>
    private readonly IProtocolFunctions _Functions;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private IModuleACS _ModuleACS;

    /// <summary>
    /// 定义直流源模块对象
    /// </summary>
    private IModuleDCS _ModuleDCS;

    /// <summary>
    /// 定义直流表模块对象
    /// </summary>
    private IModuleDCM _ModuleDCM;

    /// <summary>
    /// 定义电能模块模块对象
    /// </summary>
    private IModulePQ _ModulePQ;

    /// <summary>
    /// 定义开关量模块对象
    /// </summary>
    private IModuleIO _ModuleIO;

    #endregion 私有字段

    #region 【公共属性】

    #region 公共属性==>[功能状态]
    /// <summary>
    /// 是否具备交流源模块
    /// </summary>
    public bool IsACSModuleEnabled { get; set; }

    /// <summary>
    /// 是否具备直流源模块
    /// </summary>
    public bool IsDCSModuleEnabled { get; set; }

    /// <summary>
    /// 是否具备开关量模块
    /// </summary>
    public bool IsIOModuleEnabled { get; set; }

    /// <summary>
    /// 是否具备电能模块
    /// </summary>
    public bool IsPQModuleEnabled { get; set; }

    /// <summary>
    /// 是否具备直流表模块
    /// </summary>
    public bool IsDCMModuleEnabled { get; set; }
    #endregion 公共属性==>功能状态

    #region 公共属性==>[功能模块]
    /// <summary>
    /// 交流源模块
    /// </summary>
    public IModuleACS ModuleACS
    {
        get
        {
            if ( !IsACSModuleEnabled )
            {
                throw new Exception ( StringResources. Language. NotSupportedModule + Model );
            }
            return _ModuleACS;
        }
        set { _ModuleACS = value; }
    }

    /// <summary>
    /// 直流源模块
    /// </summary>
    public IModuleDCS ModuleDCS
    {
        get
        {
            if ( !IsDCSModuleEnabled )
            {
                throw new Exception ( StringResources. Language. NotSupportedModule + Model );
            }
            return _ModuleDCS;
        }
        set { _ModuleDCS = value; }
    }

    /// <summary>
    /// 直流表模块
    /// </summary>
    public IModuleDCM ModuleDCM
    {
        get
        {
            if ( !IsDCMModuleEnabled )
            {
                throw new Exception ( StringResources. Language. NotSupportedModule + Model );

            }
            return _ModuleDCM;
        }
        set { _ModuleDCM = value; }
    }


    /// <summary>
    /// 电能模块
    /// </summary>
    public IModulePQ ModulePQ
    {
        get
        {
            if ( !IsPQModuleEnabled )
            {
                throw new Exception ( StringResources. Language. NotSupportedModule + Model );

            }
            return _ModulePQ;
        }
        set { _ModulePQ = value; }
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
        set { _ModuleIO = value; }
    }
    #endregion 公共属性==>功能模块

    #endregion 公共属性

    #region 【构造函数】

    public Dandick ( Models model )
    {
        _ModuleACS = new ModuleACS ( model );
        _ModuleDCS = new ModuleDCS ( model );
        _ModuleDCM = new ModuleDCM ( model );
        _ModuleIO = new ModuleIO ( model );
        _ModulePQ = new ModulePQ ( model );

        _Functions = model switch //TODO 看视频如何解决这个问题
        {
            Models. Hex81 => new Hex81Functions ( ),
            Models. Hex5AA5 => new Hex5AA5Functions ( ),
            _ => new Hex81Functions ( ),
        };

       // SystemSettings = new SystemSettings ( model );

        FunctionsInitializer ( );
    }
    #endregion 构造函数

    #region 设备功能【状态使能】初始化

    /// <summary>
    /// 功能状态初始化器
    /// </summary>
    void FunctionsInitializer ( )
    {
        IsACSModuleEnabled = _Functions. IsACSModuleSupported;
        IsDCSModuleEnabled = _Functions. IsDCSModuleSupported;
        IsDCMModuleEnabled = _Functions. IsDCMModuleSupported;
        IsIOModuleEnabled = _Functions. IsIOModuleSupported;
        IsPQModuleEnabled = _Functions. IsPQModuleSupported;
        HandShake ( );
    }

    #endregion 设备功能【状态使能】初始化

    public ISystemSettings SystemSettings { get; set; }

    //private ISystemSettings _SystemSettings;

    ///// <summary>
    ///// 设置系统模式
    ///// </summary>
    ///// <param name="SystemMode">枚举系统模式</param>
    //public void SetSystemMode ( Enum SystemMode )
    //{
    //   _SystemSettings.SystemMode.SetSystemMode ( SystemMode );
    //}







    #region 模块功能状态检查

    #endregion 模块功能状态检查
}

internal  class CheckModuleState
{
    // throw new Exception ( StringResources. Language. NotEnabledModule + $"型号【{Model}】，编号【{SN}】" );
    
    
} 



