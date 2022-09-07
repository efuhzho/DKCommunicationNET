using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKCommunicationNET.Core;
using DKCommunicationNET.Protocols.Hex5A;

namespace DKCommunicationNET.Protocols.Hex81.Encoders
{
    internal class Hex81Encoder_DCS : IEncoder_DCS
    {
        private readonly Hex81EncodeHelper _encoderHelper;

        private readonly IByteTransform _transform;

        public bool IsAutoRange_DCU { get; set; }
        public bool IsAutoRange_DCI { get; set; }
        public bool IsAutoRange_DCR { get; set; }

        public Hex81Encoder_DCS(ushort id, IByteTransform transform)
        {
            _encoderHelper = new Hex81EncodeHelper(id);

            _transform = transform;
        }

        public OperateResult<byte[ ]> Packet_ReadData(char? Resistor = null)
        {
            if (Resistor == null)
            {
                return _encoderHelper.EncodeHelper(Hex81Information.ReadData_DCS);
            }

            //如果Type不为空，则创建兼容报文
            byte[ ] data = new byte[1] { (byte)Resistor };
            return _encoderHelper.EncodeHelper(Hex81Information.ReadData_DCS, Hex81Information.ReadData_DCS_Length, data);
        }

        public OperateResult<byte[ ]> Packet_Stop_DCU()
        {
            return Packet_Stop(OutputType_DCS.DCS_Type_U);
        }

        public OperateResult<byte[ ]> Packet_Stop_DCI()
        {
            return Packet_Stop(OutputType_DCS.DCS_Type_I);
        }

        public OperateResult<byte[ ]> Packet_Stop_DCR()
        {
            return Packet_Stop(OutputType_DCS.DCS_Type_R);
        }

        public OperateResult<byte[ ]> Packet_Open_DCU()
        {
            return Packet_Open(OutputType_DCS.DCS_Type_U);
        }

        public OperateResult<byte[ ]> Packet_Open_DCI()
        {
            return Packet_Open(OutputType_DCS.DCS_Type_I);
        }

        public OperateResult<byte[ ]> Packet_Open_DCR()
        {
            return Packet_Open(OutputType_DCS.DCS_Type_R);
        }

        public OperateResult<byte[ ]> Packet_GetRanges()
        {
            return _encoderHelper.EncodeHelper(Hex81Information.GetRanges_DCS);
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCI(float amplitude, byte indexOfRange)
        {
            if (IsAutoRange_DCI == true)
            {
                return Packet_SetAmplitude(0xFF, amplitude, OutputType_DCS.DCS_Type_I);

            }
            return Packet_SetAmplitude(indexOfRange, amplitude, OutputType_DCS.DCS_Type_I);
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCU(float amplitude, byte indexOfRange)
        {
            if (IsAutoRange_DCU == true)
            {
                return Packet_SetAmplitude(0xFF, amplitude, OutputType_DCS.DCS_Type_U);

            }
            return Packet_SetAmplitude(indexOfRange, amplitude, OutputType_DCS.DCS_Type_U);
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCR(float amplitude, byte indexOfRange)
        {
            if (IsAutoRange_DCR == true)
            {
                return Packet_SetAmplitude(0xFF, amplitude, OutputType_DCS.DCS_Type_R);
            }
            return Packet_SetAmplitude(indexOfRange, amplitude, OutputType_DCS.DCS_Type_R);
        }

        public OperateResult<byte[ ]> Packet_SetRange_DCI(byte indexOfRange)
        {
            return Packet_SetRange(indexOfRange, OutputType_DCS.DCS_Type_I);
        }

        public OperateResult<byte[ ]> Packet_SetRange_DCU(byte indexOfRange)
        {
            return Packet_SetRange(indexOfRange, OutputType_DCS.DCS_Type_U);
        }

        public OperateResult<byte[ ]> Packet_SetRange_DCR(byte indexOfRange)
        {
            return Packet_SetRange(indexOfRange, OutputType_DCS.DCS_Type_R);
        }

        #region Private

        /// <summary>
        /// 设置直流源输出幅值
        /// </summary>
        /// <param name="indexOfRange">直流源档位索引值</param>
        /// <param name="amplitude">幅值</param>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_SetAmplitude(byte indexOfRange, float amplitude, OutputType_DCS type)
        {
            byte[ ] data = new byte[6];
            data[0] = indexOfRange;
            _transform.TransByte(amplitude).CopyTo(data, 1);
            data[5] = (byte)type;
            return _encoderHelper.EncodeHelper(Hex81Information.SetAmplitude_DCS, Hex81Information.SetAmplitude_DCS_Length, data);
        }

        /// <summary>
        /// 停止直流源输出
        /// </summary>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_Stop(OutputType_DCS type)
        {
            byte[ ] data = new byte[1] { (byte)type };
            return _encoderHelper.EncodeHelper(Hex81Information.Stop_DCS, Hex81Information.Stop_DCS_Length, data);
        }

        /// <summary>
        /// 打开直流源输出
        /// </summary>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_Open(OutputType_DCS type)
        {

            byte[ ] data = new byte[1] { (byte)type };
            return _encoderHelper.EncodeHelper(Hex81Information.Open_DCS, Hex81Information.Open_DCS_Length, data);
        }

        /// <summary>
        /// 【暂时不用的方法】设置档位
        /// </summary>
        /// <param name="indexOfRange"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_SetRange(byte indexOfRange, OutputType_DCS type)
        {
            byte[ ] data = new byte[2] { indexOfRange, (byte)type };
            return _encoderHelper.EncodeHelper(Hex81Information.SetRange_DCS, Hex81Information.SetRange_DCS_Length, data);
        }

        #endregion
    }
}
