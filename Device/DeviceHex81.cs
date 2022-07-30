using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

namespace DKCommunicationNET. Device;

public class DeviceHex81 :IDevice
{
    public bool IsACSModuleSupported => false;
    public bool IsDCSModuleSupported=>true;
    public bool IsDCMModuleSupported =>true;
    public bool IsIOModuleSupported => false;
    public bool IsPQModuleSupported =>true;

    public ProtocolTypes ProtocolType => ProtocolTypes.Hex81;



    #region MyRegion


    //public DeviceHex81 ( )
    //{
    //    ModuleACS = new ModuleACS ( ProtocolType );
    //}
    //public IModuleACS ModuleACS { get; set; }
    //public IModuleDCS ModuleDCS { get; set; }
    //public IModuleDCM ModuleDCM { get; set; }
    //public IModulePQ ModulePQ { get; set; }
    //public IModuleIO ModuleIO { get; set; }

    //private IModuleACS _ModuleACS;
    //private IModuleDCS _ModuleDCS;
    //private IModuleDCM _ModuleDCM;
    //private IModulePQ _ModulePQ;

    //public float Ua { get; set; }
    //public float Ub { get; set; }
    //public float Uc { get; set; }
    //public float Ia { get; set; }
    //public float Ib { get; set; }
    //public float Ic { get; set; }
    //public float IPa { get; set; }
    //public float IPb { get; set; }
    //public float IPc { get; set; }
    //public float DCU { get; set; }
    //public float DCI { get; set; }

    ////多余的
    //public float Ux { get  ; set ; }
    //public float Ix { get  ; set ; }

    //public void Hex81dosomething ( )
    //{
    //    _ModuleACS  = new ModuleACS ( ProtocolType );
    //    _ModuleACS. SetACSAmplitude ( 100.005f );
    //}

    #endregion
}
