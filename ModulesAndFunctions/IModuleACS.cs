namespace DKCommunicationNET. ModulesAndFunctions;

public interface IModuleACS
{

    public OperateResult<byte[ ]> Open ( );
    public OperateResult<byte[ ]> Close ( );
    
    public OperateResult<byte[ ]> GetRangesOfACS ( );
    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 );
    public OperateResult<byte[ ]> SetAmplitude ( float amplitude );
    
   
    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );
    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 );
    public OperateResult<byte[ ]> SetWireMode ( string wireMode );
}

