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
        public OperateResult CheckCRC ( byte[ ] responseBytes )
        {
            if ( responseBytes[0] != 0x81 ) 
            {
                return new OperateResult("报文头校验失败");
            } 
            if ( responseBytes[5] ==Hex81.ErrorCode ) return new OperateResult("设备报故障");
            if ( responseBytes == null ) return new OperateResult("回复数据为空");

            int length = responseBytes. Length;
            byte[ ] buf = new byte[length - 1];
            Array. Copy ( responseBytes , 0 , buf , 0 , buf. Length );

            //断言
            byte CRC_Code = Hex81. CRCcalculator ( buf );
            if ( CRC_Code == responseBytes[length - 1] )
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return new OperateResult(StringResources.Language.CRCCheckFailed);
            }
        }
    }
}
