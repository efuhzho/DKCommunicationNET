using System;
using System. Collections. Generic;
using System. Dynamic;
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
                byte[ ] length = BitConverter. GetBytes ( commandLength );
                byte[ ] codes = BitConverter. GetBytes ( commandCode );
                byte[ ] buffer = new byte[commandLength * 2];
                buffer[0] = 0xAA;
                buffer[1] = 0x55;
                buffer[2] = length[1];
                buffer[3] = length[0];
                buffer[4] = codes[1];
                buffer[5] = codes[0];
                if ( commandLength == HexAA. MinLength )
                {
                    byte[ ] crc = BitConverter. GetBytes ( HexAA. CRCcalculator ( buffer ) );
                    buffer[6] = crc[1];
                    buffer[7] = crc[0];
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
