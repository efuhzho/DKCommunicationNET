using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_ACS : IPacketsBuilder_ACS
    {
        private ushort _id;

        public Hex81PacketBuilder_ACS ( ushort id )
        {
            _id = id;
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRangesOfACS , Hex81Information. GetRangesOfACSLength , _id );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude ( float amplitude )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetACSAmplitude , Hex81Information. SetACSAmplitudeLength , _id );
        }

        public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetWireMode ( string wireMode )
        {
            throw new NotImplementedException ( );
        }

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Close ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. CloseACS , Hex81Information. CloseACSLength , _id );
        }

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Open ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
