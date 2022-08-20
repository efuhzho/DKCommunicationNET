using System. Text;
using DKCommunicationNET. BasicFramework;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5ADecoder : IDecoder
    {
        //数据转换规则
        private readonly IByteTransform _byteTransform;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="byteTransform">数据转换规则</param>
        public Hex5ADecoder ( IByteTransform byteTransform )
        {
            _byteTransform = byteTransform;
        }

        public int Offset => Hex5AInformation.DataStartIndex;

        public string? Model { get;  set; }

        public string? SN { get;  set; }

        public string? Firmware { get; private set; }

        public string? ProtocolVer { get; private set; }

        public bool IsEnabled_ACS { get; private set; }

        public bool IsEnabled_ACM { get; private set; }

        public bool IsEnabled_DCS { get; private set; }

        public bool IsEnabled_DCM { get; private set; }

        public bool IsEnabled_IO { get; private set; }

        public bool IsEnabled_EPQ { get; private set; }

        public bool IsEnabled_DualFreqs { get; private set; }

        public bool IsEnabled_IProtect { get; private set; }

        public bool IsEnabled_PST { get; private set; }

        public bool IsEnabled_YX { get; private set; }

        public bool IsEnabled_HF { get; private set; }

        public bool IsEnabled_PWM { get; private set; }

        public bool IsEnabled_ACM_Cap { get; private set; }

        public bool IsEnabled_PPS { get; private set; }

        public bool IsEnabled_DCS_AUX { get; private set; }

        public bool IsEnabled_DCM_RIP { get; private set; }

        public void DecodeHandShake ( OperateResult<byte[ ]> result )
        {
            if ( !result. IsSuccess || result. Content == null )
            {
                return;
            }

            //下位机回复的原始报文
            byte[ ] buffer = result. Content;

            //将缓存数据转换成List方便查找字符串的结束标志:0x00
            List<byte> bufferList = buffer. ToList ( );    //可忽略null异常

            //获取设备型号结束符的索引值
            int endIndex = bufferList. IndexOf ( 0x00 , Offset );

            //计算model字节长度，包含0x00结束符,7=报文头的字节数8再减去1
            int modelLength = endIndex - Offset+1;
            //解析的设备型号
            Model = _byteTransform. TransString ( buffer , Offset , modelLength , Encoding. ASCII );

            //解析下位机版本号
            byte verA = buffer[modelLength + Offset];
            byte verB = buffer[modelLength + Offset+1];
            byte verC = buffer[modelLength + Offset+2];
            //下位机版本号
            Firmware = $"V{verA}.{verB}.{verC}";

            //解析设备编号
            int serialEndIndex = bufferList. IndexOf ( 0x00 , Offset + modelLength+3 );
            int serialLength = serialEndIndex - 7 - modelLength;
            //设备编号字节长度，包含0x00结束符            
            SN = _byteTransform. TransString ( buffer , Offset+ modelLength+3 , serialLength , Encoding. ASCII );

            //交流功能激活状态
            byte FuncB = buffer[^8];
            bool[ ] funcB = SoftBasic. ByteToBoolArray ( FuncB );
            IsEnabled_ACS = funcB[0];
            IsEnabled_ACM= funcB[1];
            IsEnabled_ACM_Cap = funcB[2];

            //特殊交流功能激活状态
            byte FuncS = buffer[^7];
            bool[ ] funcS = SoftBasic. ByteToBoolArray ( FuncS );
            IsEnabled_DualFreqs = funcS[0];
            IsEnabled_IO= funcS[3];
            IsEnabled_PPS = funcS[6];

            //直流功能
            byte FuncD= buffer[^6];
            bool[ ] funcD = SoftBasic. ByteToBoolArray ( FuncD);
            IsEnabled_DCS=funcD[0];
            IsEnabled_DCM = funcD[1];
            IsEnabled_DCM_RIP = funcD[2];
            IsEnabled_DCS_AUX = funcD[3];

            //协议版本号A
            byte PT_VerA = buffer[^5];

            //协议版本号B
            byte PT_VerB= buffer[^4];
        }
    
    }
}
