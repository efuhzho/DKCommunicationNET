namespace DKCommunicationNET. Interface;

public interface IDeviceHex5AA5
{
    public IModuleACS ModuleACS { get; set; }

    public IModuleDCS ModuleDCS { get; set; }

    public IModuleDCM ModuleDCM { get; set; }

    public IModulePQ ModulePQ { get; set; }
    public IModuleIO ModuleIO { get; set; }
}
