using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_DCS : IPacketBuilder_DCS
    {
        private readonly ushort _id;
        private readonly IByteTransform _transform;

        public bool IsAutoRange_DCU { get; set; }
        public bool IsAutoRange_DCI { get; set; }
        public bool IsAutoRange_DCR { get; set; }

        public Hex81PacketBuilder_DCS ( ushort id , IByteTransform transform )
        {
            _id = id;
            _transform = transform;
        }

        public OperateResult<byte[ ]> Packet_ReadData ( char? Resistor = null )
        {
            if ( Resistor == null )
            {
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCS , _id );
            }

            //如果Type不为空，则创建兼容报文
            byte[ ] data = new byte[1] { ( byte ) Resistor };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_DCS , Hex81Information. ReadData_DCS_Length , data , _id );
        }

        public OperateResult<byte[ ]> Packet_Stop_DCU ( )
        {
            return Packet_Stop ( OutputType_DCS. DCS_Type_U );
        }

        public OperateResult<byte[ ]> Packet_Stop_DCI ( )
        {
            return Packet_Stop ( OutputType_DCS. DCS_Type_I );
        }

        public OperateResult<byte[ ]> Packet_Stop_DCR ( )
        {
            return Packet_Stop ( OutputType_DCS. DCS_Type_R );
        }

        public OperateResult<byte[ ]> Packet_Open_DCU ( )
        {
            return Packet_Open ( OutputType_DCS. DCS_Type_U );
        }

        public OperateResult<byte[ ]> Packet_Open_DCI ( )
        {
            return Packet_Open ( OutputType_DCS. DCS_Type_I );
        }

        public OperateResult<byte[ ]> Packet_Open_DCR ( )
        {
            return Packet_Open ( OutputType_DCS. DCS_Type_R );
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRanges_DCS , _id );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCI ( float amplitude , byte indexOfRange )
        {
            if ( IsAutoRange_DCI == true )
            {
                return Packet_SetAmplitude ( 0xFF , amplitude , OutputType_DCS. DCS_Type_I );

            }
            return Packet_SetAmplitude ( indexOfRange , amplitude , OutputType_DCS. DCS_Type_I );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCU ( float amplitude , byte indexOfRange )
        {
            if ( IsAutoRange_DCU == true )
            {
                return Packet_SetAmplitude ( 0xFF , amplitude , OutputType_DCS. DCS_Type_U );

            }
            return Packet_SetAmplitude ( indexOfRange , amplitude , OutputType_DCS. DCS_Type_U );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude_DCR ( float amplitude , byte indexOfRange  )
        {
            if ( IsAutoRange_DCR == true )
            {
                return Packet_SetAmplitude ( 0xFF , amplitude , OutputType_DCS. DCS_Type_R );
            }
            return Packet_SetAmplitude ( indexOfRange , amplitude , OutputType_DCS. DCS_Type_R );
        }

        #region Private

        /// <summary>
        /// 设置直流源输出幅值
        /// </summary>
        /// <param name="indexOfRange">直流源档位索引值</param>
        /// <param name="amplitude">幅值</param>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_SetAmplitude ( byte indexOfRange , float amplitude , OutputType_DCS type )
        {
            byte[ ] data = new byte[6];
            data[0] = indexOfRange;
            _transform. TransByte ( amplitude ). CopyTo ( data , 1 );
            data[5] = ( byte ) type;
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetAmplitude_DCS , Hex81Information. SetAmplitude_DCS_Length , data , _id );
        }

        /// <summary>
        /// 停止直流源输出
        /// </summary>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_Stop ( OutputType_DCS type )
        {
            byte[ ] data = new byte[1] { ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Stop_DCS , Hex81Information. Stop_DCS_Length , data , _id );
        }

        /// <summary>
        /// 打开直流源输出
        /// </summary>
        /// <param name="type">直流源输出类型</param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_Open ( OutputType_DCS type )
        {

            byte[ ] data = new byte[1] { ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. Open_DCS , Hex81Information. Open_DCS_Length , data , _id );
        }

        /// <summary>
        /// 【暂时不用的方法】设置档位
        /// </summary>
        /// <param name="indexOfRange"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private OperateResult<byte[ ]> Packet_SetRange ( byte indexOfRange , OutputType_DCS type )
        {
            byte[ ] data = new byte[2] { indexOfRange , ( byte ) type };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetRange_DCS , Hex81Information. SetRange_DCS_Length , data , _id );
        }
        #endregion
    }
}
