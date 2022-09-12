using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. HexAA. Encoders
{
    internal class HexAAEncoder_ACM : IEncoder_ACM
    {
        IByteTransform _byteTransform;
        HexAAEncoderHelper encodeHelper;

        internal HexAAEncoder_ACM ( IByteTransform byteTransform )
        {
            _byteTransform = byteTransform;
            encodeHelper = new HexAAEncoderHelper ();
        }

      

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            return encodeHelper. EncodeHelper ( HexAA. GetRanges );
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            return encodeHelper. EncodeHelper ( ( byte ) HexAA. ReadData );
        }

        #region 《设置系统参数
        public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexUa , byte rangeIndexIa , byte rangeIndexUb , byte rangeIndexIb , byte rangeIndexUc , byte rangeIndexIc )
        {
            _rangeIndexUa = rangeIndexUa; _rangeIndexUb = rangeIndexUb; _rangeIndexUc = rangeIndexUc;
            _rangeIndexIa = rangeIndexIa; _rangeIndexIb = rangeIndexIb; _rangeIndexIc = rangeIndexUc;
            return Packet_SetSystemArgs ( WireMode ,CurrentInputChannel, RangeSwitchMode.Manual , rangeIndexUa , rangeIndexIa , rangeIndexUb , rangeIndexIb , rangeIndexUc , rangeIndexIc );
        }

        public OperateResult<byte[ ]> Packet_SetRangeSwitchMode ( RangeSwitchMode rangeSwitchMode )
        {
            return Packet_SetSystemArgs ( WireMode ,CurrentInputChannel, rangeSwitchMode , _rangeIndexUa , _rangeIndexIa , _rangeIndexUb , _rangeIndexIb , _rangeIndexUc , _rangeIndexIc );
        }

        public OperateResult<byte[ ]> Packet_SetWireMode ( WireMode wireMode )
        {
            return Packet_SetSystemArgs ( wireMode ,CurrentInputChannel, RangeSwitchMode , _rangeIndexUa , _rangeIndexIa , _rangeIndexUb , _rangeIndexIb , _rangeIndexUc , _rangeIndexIc );
        }
        public OperateResult<byte[ ]> Packet_SetCurrentInputChannel ( CurrentInputChannel currentInputChannel )
        {
            return Packet_SetSystemArgs ( WireMode , currentInputChannel , RangeSwitchMode , _rangeIndexUa , _rangeIndexIa , _rangeIndexUb , _rangeIndexIb , _rangeIndexUc , _rangeIndexIc );
        }
        #endregion 设置系统参数》
        public OperateResult<byte[ ]> Packet_ReadHarmonics ( )
        {
            return new OperateResult<byte[ ]> ( StringResources. Language. NotSupportedFunction );//TODO 暂未实现
        }

        #region 《私有方法
        byte _rangeIndexUa;
        byte _rangeIndexIa;
        byte _rangeIndexUb;
        byte _rangeIndexIb;
        byte _rangeIndexUc;
        byte _rangeIndexIc;
        public WireMode WireMode { get; set; }
        public RangeSwitchMode RangeSwitchMode { get; set; }
        public CurrentInputChannel CurrentInputChannel { get; set; }
        /// <summary>
        /// 设置系统参数的原始API
        /// </summary>
        /// <param name="wireMode"></param>
        /// <param name="currentInputChannel"></param>
        /// <param name="rangeSwitchMode"></param>
        /// <param name="rangeIndexUa"></param>
        /// <param name="rangeIndexIa"></param>
        /// <param name="rangeIndexUb"></param>
        /// <param name="rangeIndexIb"></param>
        /// <param name="rangeIndexUc"></param>
        /// <param name="rangeIndexIc"></param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_SetSystemArgs ( WireMode wireMode ,CurrentInputChannel currentInputChannel, RangeSwitchMode rangeSwitchMode , byte rangeIndexUa , byte rangeIndexIa , byte rangeIndexUb , byte rangeIndexIb , byte rangeIndexUc , byte rangeIndexIc )
        {
            try
            {
                byte[ ] args = new byte[16];
                byte[ ] rangeSwitchBytes = BitConverter. GetBytes ( ( byte ) rangeSwitchMode );
                args[0] = ( byte ) wireMode;
                args[1] = ( byte ) currentInputChannel;
                args[2] = rangeSwitchBytes[1];
                args[3] = rangeSwitchBytes[0];
                BitConverter. GetBytes ( rangeIndexUa ). Reverse ( ). ToArray ( ). CopyTo ( args , 4 );
                BitConverter. GetBytes ( rangeIndexIa ). Reverse ( ). ToArray ( ). CopyTo ( args , 6 );
                BitConverter. GetBytes ( rangeIndexUb ). Reverse ( ). ToArray ( ). CopyTo ( args , 8 );
                BitConverter. GetBytes ( rangeIndexIb ). Reverse ( ). ToArray ( ). CopyTo ( args , 10 );
                BitConverter. GetBytes ( rangeIndexUc ). Reverse ( ). ToArray ( ). CopyTo ( args , 12 );
                BitConverter. GetBytes ( rangeIndexIc ). Reverse ( ). ToArray ( ). CopyTo ( args , 14 );
                return encodeHelper. EncodeHelper ( HexAA. SetSystemArgs , HexAA. SetSystemArgsLen , args );
            }
            catch ( Exception ex )
            {
                return new OperateResult<byte[ ]> ( "创建报文异常。" + ex. Message );
            }
        }
    }
    #endregion 私有方法》
}
