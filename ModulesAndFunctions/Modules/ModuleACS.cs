using DKCommunicationNET. Interface;
using DKCommunicationNET. ModulesAndFunctions;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Module;

internal class ModuleACS : IModuleACS
{
    byte IModuleACS.RangesCount_ACU => throw new NotImplementedException ( );

    byte IModuleACS.RangesCount_ACI => throw new NotImplementedException ( );

    int IModuleACS.RangeIndex_ACU { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    float IModuleACS.Range_ACU => throw new NotImplementedException ( );

    int IModuleACS.RangeIndex_ACI { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    float IModuleACS.Range_ACI => throw new NotImplementedException ( );

    int IModuleACS.RangeIndex_IProtect { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    float IModuleACS.Range_IProtect => throw new NotImplementedException ( );

    byte IModuleACS.RangesCount_IProtect => throw new NotImplementedException ( );

    byte IModuleACS.URanges_Asingle => throw new NotImplementedException ( );

    byte IModuleACS.IRanges_Asingle => throw new NotImplementedException ( );

    byte IModuleACS.IProtectRanges_Asingle => throw new NotImplementedException ( );

    float[ ] IModuleACS.ACU_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float[ ] IModuleACS.ACI_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float[ ] IModuleACS.IProtect_RangesList { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    Enum IModuleACS.WireMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    Enum IModuleACS.CloseLoopMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    Enum IModuleACS.HarmonicMode { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.Freq { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.Freq_C { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    byte IModuleACS.HarmonicCount { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    Enum IModuleACS.HarmonicChannels { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    HarmonicArgs[ ] IModuleACS.Harmonics { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IPA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IPB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.IPC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_UA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_UB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_UC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_IA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_IB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.FAI_IC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.PA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.PB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.PC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.P { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.QA { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.QB { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.QC { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
    float IModuleACS.Q { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

    float IModuleACS.SA => throw new NotImplementedException ( );

    float IModuleACS.SB => throw new NotImplementedException ( );

    float IModuleACS.SC => throw new NotImplementedException ( );

    float IModuleACS.S => throw new NotImplementedException ( );

    float IModuleACS.PFA => throw new NotImplementedException ( );

    float IModuleACS.PFB => throw new NotImplementedException ( );

    float IModuleACS.PFC => throw new NotImplementedException ( );

    float IModuleACS.PF => throw new NotImplementedException ( );

    byte IModuleACS.Flag_A => throw new NotImplementedException ( );

    byte IModuleACS.Flag_B => throw new NotImplementedException ( );

    byte IModuleACS.Flag_C => throw new NotImplementedException ( );

    OperateResult<byte[ ]> IModuleACS.CloseACS ( )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.GetRangesOfACS ( )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.OpenACS ( )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetAmplitudeOfACS ( float amplitude )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetClosedLoop ( Enum ClosedLoopMode )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetFrequency ( float FreqOfAll , float FreqOfC )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetHarmonicMode ( Enum HarmonicMode )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetRangesOfACS ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.SetWireMode ( Enum WireMode )
    {
        throw new NotImplementedException ( );
    }

    OperateResult<byte[ ]> IModuleACS.WriteHarmonics ( Enum harmonicChannels , HarmonicArgs[ ] harmonicArgs )
    {
        throw new NotImplementedException ( );
    }
}
