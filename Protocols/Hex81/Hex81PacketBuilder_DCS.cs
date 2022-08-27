using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_DCS : IPacketBuilder_DCS
    {
        private readonly ushort _id;
        private readonly IByteTransform _transform;
        public Hex81PacketBuilder_DCS ( ushort id , IByteTransform transform )
        {
            _id = id;
            _transform = transform;
        }
        public OperateResult<byte[ ]> Packet_Stop ( byte? type = null )
        {
            if ( type == null )
            {
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Stop_DCS , _id );
            }

            //如果Type不为空，则创建兼容报文
            byte[ ] data = new byte[1] { ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Stop_DCS , Hex81Information. Stop_DCS_Length , data , _id );
        }
        public OperateResult<byte[ ]> Packet_Open ( byte? type = null )
        {
            if ( type == null )
            {
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Open_DCS , _id );
            }

            //如果Type不为空，则创建兼容报文
            byte[ ] data = new byte[1] { ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Open_DCS , Hex81Information. Open_DCS_Length , data , _id );
        }

        public OperateResult<byte[ ]> Packet_ReadData ( byte? type = null )
        {
            if ( type == null )
            {
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCS , _id );
            }

            //如果Type不为空，则创建兼容报文
            byte[ ] data = new byte[1] { ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCS , Hex81Information. ReadData_DCS_Length , data , _id );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude ( byte indexOfRange , float amplitude , byte type  )
        {
            byte[ ] data = new byte[6];
            data[0] = indexOfRange;
            data[5] = type;
            _transform. TransByte ( amplitude ). CopyTo ( data , 1 );
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetAmplitude_DCS , Hex81Information. SetAmplitude_DCS_Length , data , _id );
        }

        public OperateResult<byte[ ]> Packet_SetRange ( byte indexOfRange , byte type )
        {
            byte[ ] data = new byte[2] { indexOfRange , type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetRange_DCS , Hex81Information. SetRange_DCS_Length , data , _id );
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRanges_DCS , _id );
        }

        public OperateResult<byte[ ]> Packet_SetRange_Auto ( byte type )
        {
            return Packet_SetRange ( 0xFF , type );
        }
    }
}
