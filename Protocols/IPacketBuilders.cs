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
    public ushort ID { get; }
    public OperateResult<byte[ ]> PacketOfGetRanges ( );
    public OperateResult<byte[ ]> PacketOfSetAmplitude ( float amplitude );
    public OperateResult<byte[ ]> PacketOfOpen ( );
    public OperateResult<byte[ ]> PacketOfClose ( );
    public OperateResult<byte[ ]> PacketOfSetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 );
    public OperateResult<byte[ ]> PacketOfSetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );
    public OperateResult<byte[ ]> PacketOfSetFrequency ( float FreqOfAll , float FreqOfC = 0 );
    public OperateResult<byte[ ]> PacketOfSetWireMode ( string wireMode );
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