﻿using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5APacketBuilderOfACS : IPacketsBuilderOfACS
    {
        public ushort ID { get; set ; }

        public Hex5APacketBuilderOfACS (ushort id )
        {
            ID = id;
        }

        public OperateResult<byte[ ]> PacketOfClose ( )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> PacketOfGetRanges ( )
        {
            byte[ ] data = new byte[ 1] { (byte)Hex5AInformation. GetRangeType. ACS };
            return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. OK , Hex5AInformation. OKLength ,ID);
        }

        public OperateResult<byte[ ]> PacketOfOpen ( )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> PacketOfSetAmplitude ( float amplitude )
        {
            throw new NotImplementedException ( );
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
