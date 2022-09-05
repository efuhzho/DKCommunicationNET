using System;
using System. Collections. Generic;
using System. Linq;
using System. Security. Cryptography;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols. Hex5A;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_ACM:IPacketBuilder_ACM
    {
        private readonly Hex5APacketBuilderHelper _PBHelper;

        public Hex81PacketBuilder_ACM ( ushort id )
        {
            _PBHelper = new Hex5APacketBuilderHelper ( id );
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            return _PBHelper. PacketShellBuilder ( Hex81Information. ReadData_ACS );
        }

        public OperateResult<byte[ ]> Packet_ReadData_Status ( )
        {
          return  _PBHelper. PacketShellBuilder ( Hex81Information. GetStatus_ACS );
        }
    }
}
