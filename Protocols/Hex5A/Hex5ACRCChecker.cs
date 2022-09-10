using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5ACRCChecker:ICRCChecker
    {    

        /// <summary>
        /// 核验接收的下位机数据校验码
        /// </summary>
        /// <param name="responseBytes">下位机回复的报文</param>
        /// <returns>核验结果</returns>
        public  bool CheckCRC ( byte[ ] responseBytes )
        {
            if ( responseBytes[0] !=0x5A || responseBytes[1] != 0xA5 ) return false;
            if ( responseBytes == null ) return false;
            if ( responseBytes. Length < 2 ) return false;

            int length = responseBytes. Length;
            byte[ ] buf = new byte[length - 5];
            Array. Copy ( responseBytes , 2, buf , 0 , buf. Length );

            //断言
            byte[] CRC_Code = Hex5A.CRCcalculator ( buf );
            if ( CRC_Code[0] == responseBytes[length - 3] && CRC_Code[1] == responseBytes[length - 2] )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
