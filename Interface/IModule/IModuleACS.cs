namespace DKCommunicationNET. Interface. IModule;

public interface IModuleACS
{
    public void GetRangesOfACS ( );
    public void SetACSAmplitude ( float amplitude );
    public void StartACS ( );
    public void StopACS ( );
}

