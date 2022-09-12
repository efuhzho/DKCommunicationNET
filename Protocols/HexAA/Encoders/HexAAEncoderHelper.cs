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
        public  OperateResult<byte[ ]> EncodeHelper ( byte commandCode , ushort commandLength , byte[ ] data )
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
                shell[shell. Length ^ 2] = HexAA. CRCcalculator ( shell ) [0];
                shell[shell. Length ^ 1] = HexAA. CRCcalculator ( shell ) [1];
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

        public  OperateResult<byte[ ]> EncodeShell ( byte commandCode , ushort commandLength )
        {
            
            try
            {
                //协议内报文长度为字节长度的一半；
                byte[ ] length = BitConverter. GetBytes ( commandLength/2 );
                //byte[ ] codes = BitConverter. GetBytes ( commandCode );
                byte[ ] buffer = new byte[commandLength ];
                buffer[0] = 0xAA;
                buffer[1] = 0x55;
                buffer[2] = length[1];
                buffer[3] = length[0];
                buffer[4] = 0x00;
                buffer[5] = commandCode;
                if ( commandLength == HexAA. MinLength )
                {
                    byte[ ] crc = HexAA. CRCcalculator ( buffer );
                    buffer[6] = crc[0];
                    buffer[7] = crc[1];
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
