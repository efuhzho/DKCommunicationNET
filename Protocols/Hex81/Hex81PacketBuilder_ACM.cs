using System;
using System. Collections. Generic;
using System. Linq;
using System. Security. Cryptography;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_ACM:IPacketBuilder_ACM
    {
        readonly ushort _id;
        public Hex81PacketBuilder_ACM ( ushort id )
        {
            _id = id;
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_ACS , _id );
        }

        public OperateResult<byte[ ]> Packet_ReadData_Status ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetStatus_ACS , _id );
        }
    }
}
