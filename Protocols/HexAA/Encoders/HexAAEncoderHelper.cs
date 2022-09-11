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
            try
            {
                var shellResult = EncodeShell ( commandCode , commandLength );
                if ( !shellResult. IsSuccess || shellResult. Content == null )
                {
                    return shellResult;
                }
                var shell = shellResult. Content;
                data. CopyTo ( shell , 6 );
                shell[shell. Length ^ 2] = BitConverter. GetBytes ( HexAA. CRCcalculator ( shell ) )[1];
                shell[shell. Length ^ 1] = BitConverter. GetBytes ( HexAA. CRCcalculator ( shell ) )[0];
                return OperateResult. CreateSuccessResult ( shell );
            }
            catch ( Exception ex)
            {
                return new OperateResult<byte[ ]>(ex.Message);
            }            
        }

        public OperateResult<byte[ ]> EncodeHelper ( byte commandCode )
        {
            return EncodeShell ( commandCode , HexAA. MinLength );
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
