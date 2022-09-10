using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. HexAA. Encoders
{
    internal class HexAAEncoderHelper : IEncodeHelper
    {
        public OperateResult<byte[ ]> EncodeHelper ( byte commandCode , ushort commandLength , byte[ ] data )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> EncodeHelper ( byte commandCode )
        {
            throw new NotImplementedException ( );
        }
    }
}
