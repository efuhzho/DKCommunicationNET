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

        public OperateResult<byte[ ]> EncodeShell ( byte commandCode , ushort commandLength )
        {
            try
            {
                byte[ ] buffer = new byte[commandLength * 2];
                buffer[0] = BitConverter. GetBytes ( HexAA. FrameID )[1];
                buffer[1] = BitConverter. GetBytes ( HexAA. FrameID )[0];
                buffer[2] = BitConverter. GetBytes ( commandLength )[1];
                buffer[3] = BitConverter. GetBytes ( commandLength )[0];
                buffer[4] = BitConverter. GetBytes ( commandCode )[1];
                buffer[5] = BitConverter. GetBytes ( commandCode )[0];
                if ( commandLength == HexAA. MinLength )
                {
                    buffer[6] = BitConverter. GetBytes ( HexAA. CRCcalculator ( buffer ) )[1];
                    buffer[7] = BitConverter. GetBytes ( HexAA. CRCcalculator ( buffer ) )[0];
                }
                return OperateResult. CreateSuccessResult ( buffer );
            }
            catch ( Exception ex)
            {

                return new OperateResult<byte[ ]> ("HexAA EncodeShell Failed"+ ex. Message );
            }
           
        }
    }
}
