using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilderOfACS : IPacketsBuilder_ACS
    {
        private ushort _id;

        public Hex81PacketBuilderOfACS ( ushort id )
        {
            _id = id;
        }

        public OperateResult<byte[ ]> PacketOfClose ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. CloseACS , Hex81Information. CloseACSLength , _id );
        }

        public OperateResult<byte[ ]> PacketOfGetRanges ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRangesOfACS , Hex81Information. GetRangesOfACSLength , _id );
        }

        public OperateResult<byte[ ]> PacketOfOpen ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. OpenACS , Hex81Information. OpenACSLength , _id );
        }

        public OperateResult<byte[ ]> PacketOfSetAmplitude ( float amplitude )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetACSAmplitude , Hex81Information. SetACSAmplitudeLength , _id );
        }

        public OperateResult<byte[ ]> PacketOfSetFrequency ( float FreqOfAll , float FreqOfC = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> PacketOfSetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> PacketOfSetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIPa = 0 , byte rangeIndexOfIPb = 0 , byte rangeIndexOfIPc = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> PacketOfSetWireMode ( string wireMode )
        {
            throw new NotImplementedException ( );
        }
    }
}
