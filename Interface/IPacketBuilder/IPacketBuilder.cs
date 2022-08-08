using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Interface. IPacketBuilder
{
    internal interface IPacketBuilder
    {
        OperateResult<byte [ ]> PacketBuilder ( byte commandCode , ushort commandLength , ushort id = 0 );
        OperateResult<byte [ ]> PacketBuilder ( byte commandCode , ushort commandLength , byte [ ] data , ushort id = 0 );
    }
}
