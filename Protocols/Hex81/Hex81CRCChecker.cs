using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81CRCChecker : ICRCChecker
    {
        /// <summary>
        /// 核验接收的下位机数据校验码
        /// </summary>
        /// <param name="responseBytes">下位机回复的报文</param>
        /// <returns>核验结果</returns>
        public bool CheckCRC ( byte[ ] responseBytes )
        {
            if ( responseBytes[0] != 0x81 ) return false;
            if ( responseBytes == null ) return false;
            if ( responseBytes. Length < 2 ) return false;

            int length = responseBytes. Length;
            byte[ ] buf = new byte[length - 1];
            Array. Copy ( responseBytes , 0 , buf , 0 , buf. Length );

            //断言
            byte CRC_Code = Hex81. CRCcalculator ( buf );
            if ( CRC_Code == responseBytes[length - 1] )
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
