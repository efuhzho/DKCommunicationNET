namespace DKCommunicationNET. Module. IModule;

public interface IModuleACS
{
    public void GetRangesOfACS ( );
    public void SetAmplitudeOfACS ( float amplitude );
    public void StartOutputOfACS ( );
    public void StopOutputOfACS ( );
    public void SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 );
}

