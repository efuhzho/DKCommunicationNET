using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;
using DKCommunicationNET. Protocol;
using DKCommunicationNET. BaseClass;

namespace DKCommunicationNET;

public class DandickSource : DKSerialBase, IHex81
{
    public IModuleACI ModuleACI { get; set; }
    public IModuleACU ModuleACU { get; set; }
    public IModuleDCU ModuleDCU { get; set; }
    public IModuleDCI ModuleDCI { get; set; }
    public float Ua { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float Ub { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float Uc { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float Ia { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float Ib { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float Ic { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float DCU { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    public float DCI { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    public DandickSource ( Models model )
    {
        protocolype = ( ProtocolTypes ) model;
        ModuleACU = new ModuleACU ( );
        ModuleACI = new ModuleACI ( );
        ModuleDCU = new ModuleDCU ( );
        ModuleDCI = new ModelDCI ( );
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
