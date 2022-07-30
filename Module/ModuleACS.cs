using DKCommunicationNET. Interface;


namespace DKCommunicationNET. Module;

internal class ModuleACS : IModuleACS
{
    public bool IsACSModuleConnected => throw new NotImplementedException ( );

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

internal class ModuleACS_TEST : IModuleACS
{
    public bool IsACSModuleConnected => throw new NotImplementedException ( "Marco Zhou" );

    public void SetACSAmplitude ( float amplitude )
    {
        throw new NotImplementedException ("Marco Zhou" );
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
