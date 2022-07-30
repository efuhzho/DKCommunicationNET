namespace DKCommunicationNET. Interface;

public interface IDeviceHex81
{
    public IModuleACU ModuleACU { get; set; }
    public IModuleACI ModuleACI { get; set; }
    public IModuleDCU ModuleDCU { get; set; }
    public IModuleDCI ModuleDCI { get; set; }
    public float Ua { get; set; }
    public float Ub { get; set; }
    public float Uc { get; set; }
    public float Ia { get; set; }
    public float Ib { get; set; }
    public float Ic { get; set; }
    public float DCU { get; set; }
    public float DCI { get; set; }
    public void StopAC ( );
    public void StopDC ( );

}