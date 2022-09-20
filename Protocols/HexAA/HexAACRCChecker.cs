using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. HexAA
{
    internal class HexAACRCChecker:ICRCChecker
    {
        /// <summary>
        /// 核验接收的下位机数据校验码
        /// </summary>
        /// <param name="responseBytes">下位机回复的报文</param>
        /// <returns>核验结果</returns>
        public OperateResult CheckCRC ( byte[ ] responseBytes )
        {
            if ( responseBytes == null ) return new OperateResult ( "回复数据为空" );
            if ( responseBytes. Length < HexAA.MinLength ) return new OperateResult ( "报文长度校验失败" );
            if ( responseBytes[0] != 0xAA || responseBytes[1] != 0x55 ) return new OperateResult ( "报文头校验失败" );

            //断言
            byte[ ] CRC_Code =HexAA. CRCcalculator ( responseBytes );
            if ( CRC_Code[0] == responseBytes[^1] && CRC_Code[1] == responseBytes[ ^2] )
            {
                return OperateResult. CreateSuccessResult ( );
            }
            else
            {
                return new OperateResult ( StringResources. Language. CRCCheckFailed );
            }
        }
    }
}
