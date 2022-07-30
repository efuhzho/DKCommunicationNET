using DKCommunicationNET. Interface;


namespace DKCommunicationNET. Module;

internal class ModuleACS :ModuleBase ,IModuleACS
{    
    public ModuleACS ( Models protocolType )
    {
        _protocolType = protocolType;
    }
   

    public void SetACSAmplitude ( float amplitude )
    {

    }

    public void StartACS ( )
    {
        
    }

    public void StopACS ( )
    {
       
    }
}
