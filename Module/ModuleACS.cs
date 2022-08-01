using DKCommunicationNET. Interface;
using DKCommunicationNET. Interface. IModule;
using DKCommunicationNET. ProtocolInformation;

namespace DKCommunicationNET. Module;

internal class ModuleACS :ModuleBase ,IModuleACS
{    
    public ModuleACS ( Models protocolType )
    {
        _protocolType = protocolType;
    }

    public void GetRangesOfACS ( )
    {
        throw new NotImplementedException ( );
    }

    public List<Enum> GetSystemMode ( )
    {
        throw new NotImplementedException ( );
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
