using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

internal class ModuleACS : IModuleACS
{
    public bool IsEnabled { get; set; }=true;

    public OperateResult<byte[ ]> Close ( )
    {
        throw new NotImplementedException ( );

    }

    public OperateResult<byte[ ]> GetRanges ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Open ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetAmplitude ( float amplitude )
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

    public OperateResult<byte[ ]> SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> SetWireMode ( string wireMode )
    {
        throw new NotImplementedException ( );
    }
}
