using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

internal class ModuleACS : IModuleACS
{
    public bool IsEnabled { get; set; }=true;

    public OperateResult<byte[ ]> CloseACS ( )
    {
        throw new NotImplementedException ( );

    }

    public OperateResult<byte[ ]> GetRangesOfACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> OpenACS ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetAmplitudeOfACS ( float amplitude )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetWireMode ( Enum wireMode )
    {
        throw new NotImplementedException ( );
    }
}
