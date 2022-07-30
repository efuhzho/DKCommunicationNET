using DKCommunicationNET. Device;
using DKCommunicationNET. Interface;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Module;
using System. Windows;
namespace DKCommunicationNET;

public class Dandick : DKSerialBase
{
    #region 私有字段

    /// <summary>
    /// 定义设备对象
    /// </summary>
    private readonly IDevice _Device;

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

    #region 公共属性

    /// <summary>
    /// 是否具备交流源模块
    /// </summary>
    public bool IsACSModuleConnected { get; set; }

    /// <summary>
    /// 是否具备直流源模块
    /// </summary>
    public bool IsDCSModuleConnected { get; set; }

    /// <summary>
    /// 是否具备开关量模块
    /// </summary>
    public bool IsIOModuleConnected { get; set; }

    /// <summary>
    /// 是否具备电能模块
    /// </summary>
    public bool IsPQModuleConnected { get; set; }

    /// <summary>
    /// 是否具备直流表模块
    /// </summary>
    public bool IsDCMModuleConnected { get; set; }

    /// <summary>
    /// 交流源模块
    /// </summary>

    public IModuleACS ModuleACS
    {
        get
        {
            if ( !IsACSModuleConnected )
            {
                throw new Exception ( $"{Model}不支持此功能" );

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
            if ( !IsDCSModuleConnected )
            {
                throw new Exception ( $"{Model}不支持此功能" );
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
            if ( !IsDCMModuleConnected )
            {
                throw new Exception ( $"{Model}不支持此功能" );

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
            if ( !IsPQModuleConnected )
            {
                throw new Exception ( $"{Model}不支持此功能" );

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
            if ( !IsIOModuleConnected )
            {
                throw new Exception ( $"{Model}不支持此功能" );
            }
            return _ModuleIO;
        }
        set { _ModuleIO = value; }
    }

    #endregion 公共属性

    #region 构造函数

    public Dandick ( Models model )
    {        
        _ModuleACS = new ModuleACS ( model );
        _ModuleDCS = new ModuleDCS ( model );
        _ModuleDCM = new ModuleDCM ( model );
        _ModuleIO = new ModuleIO ( model );
        _ModulePQ = new ModulePQ ( model );

        _Device = model switch //TODO 看视频如何解决这个问题
        {
            Models. Hex81 => new DeviceHex81 ( ),
            Models. Hex5AA5 => new DeviceHex5AA5 ( ),
            _ => new DeviceHex81 ( ),
        };

        FunctionsInitializer ( );
    }
    #endregion 构造函数

    #region 初始化

    /// <summary>
    /// 功能状态初始化器
    /// </summary>
    void FunctionsInitializer ( )
    {
        IsACSModuleConnected = _Device. IsACSModuleSupported;
        IsDCSModuleConnected = _Device. IsDCSModuleSupported;
        IsDCMModuleConnected = _Device. IsDCMModuleSupported;
        IsIOModuleConnected = _Device. IsIOModuleSupported;
        IsPQModuleConnected = _Device. IsPQModuleSupported;
    }

    #endregion 初始化
}



