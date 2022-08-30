using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5APacketBuilder_ACS //: IPacketsBuilder_ACS
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

       
    }
}
