using DKCommunicationNET. Interface;


namespace DKCommunicationNET. Module;

internal class ModuleACS : IModuleACS
{
    private readonly ProtocolTypes _protocolType;
    public ModuleACS ( ProtocolTypes protocolType )
    {
        _protocolType = protocolType;
    }
   

    public void SetACSAmplitude ( float amplitude )
    {

    }

    public void StartACS ( )
    {
        throw new NotImplementedException ( );
    }

    public void StopACS ( )
    {
        throw new NotImplementedException ( );
    }
}
