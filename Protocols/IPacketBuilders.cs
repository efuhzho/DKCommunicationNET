using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols;

/// <summary>
/// 报文创建助手
/// </summary>
internal interface IPacketBuilderHelper
{
    OperateResult<byte[]> PacketShellBuilder ( byte commandCode , ushort commandLength , ushort id  );
    OperateResult<byte[]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[] data , ushort id  );
}

internal interface IPacketsBuilder_ACS
{
   
    public OperateResult<byte[ ]> Packet_GetRanges ( );
    public OperateResult<byte[ ]> Packet_SetAmplitude ( float amplitude );
    internal OperateResult<byte[ ]> Packet_Open ( );
    internal OperateResult<byte[ ]> Packet_Close ( );
    public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 );
    public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );
    public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 );
    public OperateResult<byte[ ]> Packet_SetWireMode ( string wireMode );
}
internal interface IPacketBuilder_ACM
{
}

internal interface IPacketBuilder_DCS
{
}

internal interface IPacketBuilder_DCM
{
}

internal interface IPacketBuilder_IO
{
}

internal interface IPacketBuilder_PQ
{
}
internal interface IHandShake
{

}