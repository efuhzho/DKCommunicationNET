using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols;

internal interface IPacketBuilderHelper
{
    OperateResult<byte[]> PacketShellBuilder ( byte commandCode , ushort commandLength , ushort id  );
    OperateResult<byte[]> PacketShellBuilder ( byte commandCode , ushort commandLength , byte[] data , ushort id  );
}

internal interface IPacketsBuilderOfACS
{
    public ushort ID { get; set; }
    public OperateResult<byte[ ]> PacketOfGetRanges ( );
    public OperateResult<byte[ ]> PacketOfSetAmplitude ( float amplitude );
    public OperateResult<byte[ ]> PacketOfOpen ( );
    public OperateResult<byte[ ]> PacketOfClose ( );
    public OperateResult<byte[ ]> PacketOfSetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 );
    public OperateResult<byte[ ]> PacketOfSetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc );
    public OperateResult<byte[ ]> PacketOfSetFrequency ( float FreqOfAll , float FreqOfC = 0 );
    public OperateResult<byte[ ]> PacketOfSetWireMode ( string wireMode );
}
internal interface IPacketBuilderOfACM
{
}

internal interface IPacketBuilderOfDCS
{
}

internal interface IPacketBuilderOfDCM
{
}

internal interface IPacketBuilderOfIO
{
}

internal interface IPacketBuilderOfPQ
{
}
internal interface IHandShake
{

}