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
    private readonly IDevice _Device;
    private IModuleACS _ModuleACS;

    public IModuleACS ModuleACS
    {
        get
        {
            if ( IsACSModuleConnected )
            {
                return _ModuleACS;
            }
            throw new Exception ( $"{Model}不支持此功能" );
        }
        set { _ModuleACS = value; }
    }

    private IModuleDCS _ModuleDCS;

    public IModuleDCS ModuleDCS
    {
        get { return _ModuleDCS; }
        set { _ModuleDCS = value; }
    }

    private IModuleDCM _ModuleDCM;

    public IModuleDCM ModuleDCM
    {
        get { return _ModuleDCM; }
        set { _ModuleDCM = value; }
    }


    private IModulePQ _ModulePQ;

    public IModulePQ ModulePQ
    {
        get { return _ModulePQ; }
        set { _ModulePQ = value; }
    }

    private IModuleIO _ModuleIO;

    public IModuleIO ModuleIO
    {
        get { return _ModuleIO; }
        set { _ModuleIO = value; }
    }


    public Dandick ( Models model )
    {
        Model = model. ToString ( ); ;
        ProtocolType = ( ProtocolTypes ) model;

        _ModuleACS = new ModuleACS ( ProtocolType );
        _ModuleDCS = new ModuleDCS ( ProtocolType );
        _ModuleDCM = new ModuleDCM ( ProtocolType );
        _ModuleIO = new ModuleIO ( ProtocolType );
        _ModulePQ = new ModulePQ ( ProtocolType );

        _Device = ProtocolType switch //TODO 看视频如何解决这个问题
        {
            ProtocolTypes. Hex81 => new DeviceHex81 ( ),
            ProtocolTypes. Hex5AA5 => new DeviceHex5AA5 ( ),
            _ => new DeviceHex81 ( ),
        };

        FunctionsInitializer ( );
    }

    /// <summary>
    /// 功能状态初始化器
    /// </summary>
   void FunctionsInitializer ( )
    {
        IsACSModuleConnected = _Device. IsACSModuleSupported;
        IsDCSModuleConnected = _Device. IsDCSModuleSupported;
    }

    public bool IsACSModuleConnected { get; set; }
    public bool IsDCSModuleConnected { get; set; }
    public bool IsIOModuleConnected { get; set; }
    public bool IsPQModuleConnected { get; set; }
    public bool IsDCMModuleConnected { get; set; }
}

    

