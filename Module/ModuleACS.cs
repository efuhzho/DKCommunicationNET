using DKCommunicationNET. Interface;
using DKCommunicationNET. Module. IModule;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

internal class ModuleACS :IModuleACS
{    
   
    public void GetRangesOfACS ( )
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
