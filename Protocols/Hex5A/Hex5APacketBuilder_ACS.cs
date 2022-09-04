using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;
using DKCommunicationNET. ModulesAndFunctions;

namespace DKCommunicationNET. Protocols. Hex5A;

internal class Hex5APacketBuilder_ACS : IPacketsBuilder_ACS
{
    private readonly Hex5APacketBuilderHelper _PBHelper;
    private readonly IByteTransform _transform;


    public Hex5APacketBuilder_ACS ( ushort id , IByteTransform byteTransform )
    {
        _PBHelper = new Hex5APacketBuilderHelper ( id );
        _transform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_GetRanges ( )
    {
        byte[ ] data = new byte[1] { ( byte ) Type_Module. ACS };
        return _PBHelper. PacketShellBuilder ( Hex5AInformation. GetRanges_ACS , Hex5AInformation. GetRanges_ACS_Len , data );
    }

    public OperateResult<byte[ ]> Packet_Open ( )
    {
        return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );
    }

    public OperateResult<byte[ ]> Packet_ReadData ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_ReadData_Status ( )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPAOrUx  , float IPBOrIX , float IPC  )
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[8];
        args[0] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ua , UA );
        args[1] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ub , UB );
        args[2] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Uc , UC );
        args[3] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ia , IA );
        args[4] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ib , IB );
        args[5] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ic , IC );
        args[6] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ux , IPAOrUx );
        args[7] = new SetStandardSourceArgs ( SetStandardSource_Channels. Channel_Ix , IPBOrIX );
        return SetArgs_ACS ( Type_SetStandardSource. Amplitude , args );
    }

    public OperateResult<byte[ ]> Packet_SetClosedLoop ( CloseLoopMode closeLoopMode )
    {
        return SetModeAndRanges_ACS ( Flag_SetType. SetCloseLoopMode , 0 , 0 , closeLoopMode , 0 , 0 , Array. Empty<byte> ( ) );
    }

    public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float Freq_C_Or_X = 0 )
    {
        SetStandardSourceArgs[ ] args = new SetStandardSourceArgs[1];
        args[0] = new SetStandardSourceArgs ( SetStandardSource_Channels_Freq. Freq , FreqOfAll );
        args[1] = new SetStandardSourceArgs ( SetStandardSource_Channels_Freq. Freq_X , Freq_C_Or_X );
        return SetArgs_ACS ( Type_SetStandardSource. Freqency , args );
    }

    public OperateResult<byte[ ]> Packet_SetHarmonicMode ( HarmonicMode harmonicMode )
    {
        return SetModeAndRanges_ACS ( Flag_SetType. SetHarmonicMode , 0 , 0 , 0 , harmonicMode , 0 , Array. Empty<byte> ( ) );
    }

    public OperateResult<byte[ ]> Packet_SetHarmonics ( Channels channels , HarmonicArgs[ ]? harmonics = null )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        byte[ ] ranges = new byte[8] { rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfACU , rangeIndexOfACI , 0 , 0 };
        return SetModeAndRanges_ACS ( Flag_SetType. SetRanges , 0 , 0 , 0 , 0 , 0 , ranges );
    }

    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndex_Ux , byte rangeIndex_Ix )
    {
        byte[ ] ranges = new byte[8] { rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfACU , rangeIndexOfACI , rangeIndexOfACU , rangeIndexOfACI , rangeIndex_Ux , rangeIndex_Ix };
        return SetModeAndRanges_ACS ( Flag_SetType. SetRanges , 0 , 0 , 0 , 0 , 0 , ranges );
    }

    public OperateResult<byte[ ]> Packet_SetWattLessPower ( byte channel , float q )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetWattPower ( byte channel , float p )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetWireMode ( WireMode wireMode )
    {
        return SetModeAndRanges_ACS ( Flag_SetType. SetWireMode , 0 , wireMode , 0 , 0 , 0 , Array. Empty<byte> ( ) );
    }

    public OperateResult<byte[ ]> Packet_Stop ( )
    {
        throw new NotImplementedException ( );
    }

    #region 私有方法
    public OperateResult<byte[ ]> SetModeAndRanges_ACS ( Flag_SetType flag_Type , WorkMode workMode , WireMode wireMode , CloseLoopMode closeLoopMode , HarmonicMode harmonicMode , QP_Mode qP_Mode , byte[ ] rangs )
    {
        byte[ ] data = new byte[14];
        data[0] = ( byte ) flag_Type;
        data[1] = ( byte ) workMode;
        data[2] = ( byte ) wireMode;
        data[3] = ( byte ) closeLoopMode;
        data[4] = ( byte ) harmonicMode;
        data[5] = ( byte ) qP_Mode;

        if ( rangs. Length == 8 )
        {
            rangs. CopyTo ( data , 6 );
        }
        return _PBHelper. PacketShellBuilder ( Hex5AInformation. SetSystemModeAndRanges , Hex5AInformation. SetSystemModeAndRanges_L , data );
    }

    /// <summary>
    /// 设置标准源参数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    private OperateResult<byte[ ]> SetArgs_ACS ( Type_SetStandardSource type , SetStandardSourceArgs[ ] args )
    {
        int count = args. Length;
        byte[ ] data = new byte[2 + 5 * count];
        data[0] = ( byte ) type;
        data[1] = ( byte ) count;
        for ( int i = 0 ; i < count ; i++ )
        {
            var result = SetStandardSourceArgs. SourceArgsToBytes ( args[i] , _transform );
            if ( !result. IsSuccess || result. Content == null )
            {
                return result;
            }
            result. Content. CopyTo ( data , index: 2 + ( i * 5 ) );
        }

        return _PBHelper. PacketShellBuilder ( Hex5AInformation. SetStandardSource , ( ushort ) ( 11 + data. Length ) , data );
    }
    #endregion
}
