using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_EPQ : IPacketBuilder_PQ
    {
       readonly ushort _id;
        public Hex81PacketBuilder_EPQ ( ushort id )
        {
            _id = id;
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_EPQ , _id );
        }

        public OperateResult<byte[ ]> Packet_SetElectricity ( byte electricityType , float meterPConst , float meterQConst , float sourcePConst , float sourceQConst , uint meterDIV , uint meterRounds ,IByteTransform byteTransform)
        {
            byte[ ] data=new byte[ 25 ];
            data[ 0 ] = electricityType;
            byteTransform. TransByte ( meterPConst ). CopyTo ( data , 1);
            byteTransform. TransByte ( meterQConst ). CopyTo ( data , 5 );
            byteTransform. TransByte ( sourcePConst ). CopyTo ( data , 9 );
            byteTransform. TransByte ( sourceQConst ). CopyTo ( data , 13 );
            BitConverter.GetBytes ( meterDIV ). CopyTo ( data , 17 );
            BitConverter.GetBytes ( meterRounds ). CopyTo ( data , 21 );

            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetElectricity , Hex81Information. SetElectricity_Length , _id );
        }
    }
}
