using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module;

internal class ModuleACI : IModuleACI
{
  
    public bool IsModuleACIConnected => throw new NotImplementedException ( );

    public ProtocolTypes ProtocolType { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    public void SetACI ( float amplitude )
    {
        throw new NotImplementedException ( );
    }

    public void START ( )
    {
        throw new NotImplementedException ( );
    }

    public void STOP ( )
    {
        throw new NotImplementedException ( );
    }
}
