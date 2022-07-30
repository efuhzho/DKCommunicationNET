using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocol;
using DKCommunicationNET. BaseClass;

namespace DKCommunicationNET. Device;

public class DeviceHex81 : DKSerialBase, IDeviceHex81
{
    
    public IModuleACI ModuleACI { get; set; }
    public IModuleACU ModuleACU { get; set; }
    public IModuleDCU ModuleDCU { get; set; }
    public IModuleDCI ModuleDCI { get; set; }
    public float Ua { get ; set ; }
    public float Ub { get ; set ; }
    public float Uc { get ; set ; }
    public float Ia { get ; set ; }
    public float Ib { get ; set ; }
    public float Ic { get ; set ; }
    public float DCU { get ; set; }
    public float DCI { get ; set; }


    public DeviceHex81 ( Models model )
    {
        _protocolType = ( ProtocolTypes ) model;
        ModuleACU = new ModuleACU (_protocolType );
        ModuleACI = new ModuleACI ( );
        ModuleDCU = new ModuleDCU ( );
        ModuleDCI = new ModuleDCI ( );
    }
    public DeviceHex81 ( ProtocolTypes protocolType )
    {
        _protocolType = protocolType;
        ModuleACU = new ModuleACU ( _protocolType );
        ModuleACI = new ModuleACI ( );
        ModuleDCU = new ModuleDCU ( );
        ModuleDCI = new ModuleDCI ( );
    }

    public void StopAC ( )
    {
        throw new NotImplementedException ( );
        
    }

    public void StopDC ( )
    {
        throw new NotImplementedException ( );
    }
}
