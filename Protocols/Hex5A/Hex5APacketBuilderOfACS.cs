using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5APacketBuilder_ACS : IPacketsBuilder_ACS
    {
        private readonly ushort _id;

        public Hex5APacketBuilder_ACS ( ushort id )
        {
            _id = id;
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            byte[ ] data = new byte[1] { ( byte ) Hex5AInformation. GetRangeType. ACS };
            return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. GetRanges_ACS , Hex5AInformation. GetRanges_ACS_Len ,data, _id );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude ( float amplitude )
        {
            throw new NotImplementedException ( );
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

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Open ( )
        {
            throw new NotImplementedException ( );
        }

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Close ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
