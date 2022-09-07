using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81. Decoders
{
    internal class Hex81Decoder_DCM : IDecoder_DCM
    {
        IByteTransform _byteTransform;
        public Hex81Decoder_DCM ( IByteTransform byteTransform )
        {
            _byteTransform = byteTransform;
        }

        /// <summary>
        /// 直流表电压档位集合
        /// </summary>
        public float[ ]? Ranges_DCMU { get; set; }

        /// <summary>
        /// 直流表电流档位集合
        /// </summary>
        public float[ ]? Ranges_DCMI { get; set; }

        /// <summary>
        /// 直流纹波电压表档位集合
        /// </summary>
        public float[ ]? Ranges_DCMU_Ripple { get; set; }

        /// <summary>
        /// 直流纹波电流表的档位集合
        /// </summary>
        public float[ ]? Ranges_DCMI_Ripple { get; set; }

        /// <summary>
        /// 直流表电压测量值
        /// </summary>
        public float DCMU { get; private set; }

        /// <summary>
        /// 直流表电流测量值
        /// </summary>
        public float DCMI { get; private set; }

        /// <summary>
        /// 直流纹波电压测量值
        /// </summary>
        public float DCMU_Ripple { get; private set; }

        /// <summary>
        /// 直流纹波电流测量值
        /// </summary>
        public float DCMI_Ripple { get; private set; }

        /// <summary>
        /// 直流表电压量程当前索引值
        /// </summary>
        public byte RangeIndex_DCMU { get; private set; }

        /// <summary>
        /// 直流表电流量程当前索引值
        /// </summary>
        public byte RangeIndex_DCMI { get; private set; }

        /// <summary>
        /// 直流纹波电压量程当前索引值
        /// </summary>
        public byte RangeIndex_DCMU_Ripple { get; private set; }

        /// <summary>
        /// 直流纹波电流量程当前索引值
        /// </summary>
        public byte RangeIndex_DCMI_Ripple { get; private set; }

        /// <summary>
        /// 直流表电压量程数量
        /// </summary>
        public byte RangesCount_DCMU { get; private set; }

        /// <summary>
        /// 直流表电流量程数量
        /// </summary>
        public byte RangesCount_DCMI { get; private set; }

        /// <summary>
        /// 直流纹波电压量程数量
        /// </summary>
        public byte RangesCount_DCMU_Ripple { get; private set; }

        /// <summary>
        /// 直流纹波电流量程数量
        /// </summary>
        public byte RangesCount_DCMI_Ripple { get; private set; }

        OperateResult IDecoder_DCM.DecodeGetRanges_DCM ( byte[ ] response )
        {
            RangesCount_DCMU = response[8];
            RangesCount_DCMI = response[9];

            Ranges_DCMU = _byteTransform. TransSingle ( response , 10 , RangesCount_DCMU );
            Ranges_DCMI = _byteTransform. TransSingle ( response , 10 + 4 * RangesCount_DCMU , RangesCount_DCMI );
            //返回解析成功结果
            return OperateResult. CreateSuccessResult ( );
        }

        OperateResult IDecoder_DCM.DecodeReadData_DCM ( byte[ ] response )
        {
            //解析测量类型
            MeasureType_DCM measureType_DCM = ( MeasureType_DCM ) response[11];

            //根据测量类型解析数据
            switch ( measureType_DCM )
            {
                case MeasureType_DCM. DCM_Voltage:
                    RangeIndex_DCMU = response[6];
                    DCMU = _byteTransform. TransSingle ( response , 7 );
                    break;
                case MeasureType_DCM. DCM_Current:
                    RangeIndex_DCMI = response[6];
                    DCMI = _byteTransform. TransSingle ( response , 7 );
                    break;
                case MeasureType_DCM. DCM_VoltageRipple:
                    RangeIndex_DCMU_Ripple = response[6];
                    DCMU_Ripple = _byteTransform. TransSingle ( response , 7 );
                    break;
                case MeasureType_DCM. DCM_CurrentRipple:
                    RangeIndex_DCMI_Ripple = response[6];
                    DCMI_Ripple = _byteTransform. TransSingle ( response , 7 );
                    break;
                default:
                    return new OperateResult ( "（Hex81）MeasureType_DCM:回复数据解码失败:没能找到匹配的MeasureType_DCM枚举项" );
            }
            //返回解析成功结果
            return OperateResult. CreateSuccessResult ( );
        }
    }
}
