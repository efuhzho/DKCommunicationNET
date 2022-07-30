namespace DKCommunicationNET. Interface;

public interface IDeviceHex81
{
    public ProtocolTypes ProtocolType { get; }
    public IModuleACS ModuleACS { get; set; }
    public IModuleDCS ModuleDCS { get; set; }
    public IModuleDCM ModuleDCM { get; set; }
    public IModulePQ ModulePQ { get; set; }
    public IModuleIO ModuleIO { get; set; }
    public float Ua { get; set; }
    public float Ub { get; set; }
    public float Uc { get; set; }
    public float Ia { get; set; }
    public float Ib { get; set; }
    public float Ic { get; set; }
    public float IPa { get; set; }
    public float IPb { get; set; }
    public float IPc { get; set; }
    public float DCU { get; set; }
    public float DCI { get; set; }

    public float Ux { get; set; }


    public float Ix { get; set; }

}