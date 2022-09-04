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
    private readonly ushort _id;
    private readonly IByteTransform _transform;


    public Hex5APacketBuilder_ACS ( ushort id , IByteTransform byteTransform )
    {
        _id = id;
        _transform = byteTransform;
    }

    public OperateResult<byte[ ]> Packet_GetRanges ( )
    {
        byte[ ] data = new byte[1] { ( byte ) Type_Module. ACS };
        return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. GetRanges_ACS , Hex5AInformation. GetRanges_ACS_Len ,data, _id );
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

    public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
    {
        throw new NotImplementedException ( );
    }  

    public OperateResult<byte[ ]> Packet_SetClosedLoop ( CloseLoopMode closeLoopMode )
    {
        return SetModeAndRanges_ACS ( Flag_Type. 接线模式 , 0 , 0 , closeLoopMode , 0 , Array. Empty<byte> ( ) , _id );
    }

    public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetHarmonicMode ( HarmonicMode harmonicMode )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetHarmonics ( byte channels , HarmonicArgs[ ]? harmonics = null )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
    {
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
    {
        throw new NotImplementedException ( );
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
        throw new NotImplementedException ( );
    }

    public OperateResult<byte[ ]> Packet_Stop ( )
    {
        throw new NotImplementedException ( );
    }

    #region 私有方法
    public OperateResult<byte[ ]> SetModeAndRanges_ACS ( Flag_Type flag_Type , WorkMode workMode , WireMode wireMode , CloseLoopMode closeLoopMode , QP_Mode qP_Mode , byte[ ] rangs , ushort id )
    {
        byte[ ] data = new byte[14];
        data[0] = ( byte ) flag_Type;
        data[1] = ( byte ) workMode;
        data[2] = ( byte ) wireMode;
        data[3] = ( byte ) closeLoopMode;
        data[4] = ( byte ) qP_Mode;

        if ( rangs. Length == 8 )
        {
            rangs. CopyTo ( data , 5 );
        }
        return Hex5APacketBuilderHelper.Instance. PacketShellBuilder ( Hex5AInformation. SetSystemModeAndRanges , Hex5AInformation. SetSystemModeAndRanges_L , data , id );
    }
    #endregion
}
