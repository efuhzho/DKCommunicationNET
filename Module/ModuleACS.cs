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

    public void SetAmplitudeOfACS ( float amplitude )
    {

    }

    public void SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI  , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
    {
        throw new NotImplementedException ( );
    }

    public void StartOutputOfACS ( )
    {
        
    }

    public void StopOutputOfACS ( )
    {
       
    }
}
