using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. HexAA. Encoders
{
    internal class HexAAEncoder_Settings : IEncoder_Settings
    {
        HexAAEncoderHelper encoderHelper;
        public HexAAEncoder_Settings ( )
        {
            encoderHelper = new HexAAEncoderHelper ( );
        }
        public OperateResult<byte[ ]> Packet_HandShake ( )
        {
            return encoderHelper. EncodeHelper ( ( byte ) HexAA. HandShake );
        }

        public OperateResult<byte[ ]> Packet_SetBaudRate ( ushort baudRate )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );
        }

        public OperateResult<byte[ ]> Packet_SetDeviceInfo ( char[ ] password , byte id , string sn )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );

        }

        public OperateResult<byte[ ]> Packet_SetDisplayPage ( Enum displayPage )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );

        }

        public OperateResult<byte[ ]> Packet_SetSystemMode ( Enum systemMode )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );

        }
    }
}
