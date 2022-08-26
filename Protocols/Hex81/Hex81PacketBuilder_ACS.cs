using System;
using System. Threading. Channels;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex81
{
    internal class Hex81PacketBuilder_ACS : IPacketsBuilder_ACS
    {
        private readonly ushort _id;
        private readonly IByteTransform _byteTransform;
        public Hex81PacketBuilder_ACS ( ushort id , IByteTransform byteTransform )
        {
            _id = id;
            _byteTransform = byteTransform;
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRanges_ACS , _id );
        }

        public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
        {
            try
            {
                byte[ ] data = new byte[9];
                for ( int i = 0 ; i < 3 ; i++ )
                {
                    data[i] = ( byte ) rangeIndexOfACU;
                    data[i + 3] = ( byte ) rangeIndexOfACI;
                    data[i + 6] = ( byte ) rangeIndexOfIP;
                }
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetRanges_ACS , Hex81Information. SetRanges_ACS_Length , data , _id );
            }
            catch ( Exception ex )
            {
                return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , StringResources. GetCurSourceFileName ( ) + ex. Message );
            }
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
        {
            float[ ] data = new float[9];
            data[0] = UA;
            data[1] = UB;
            data[2] = UC;
            data[3] = IA;
            data[4] = IB;
            data[5] = IC;
            data[6] = IPA;
            data[7] = IPB;
            data[8] = IPC;
            byte[ ] buffer = _byteTransform. TransByte ( data );
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetAmplitude_ACS , Hex81Information. SetAmplitude_ACS_Length , buffer , _id );
        }

        public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
        {
            float[ ] data = new float[2];
            data[0] = FreqOfAll;
            data[1] = FreqOfC;
            byte[ ] buffer = _byteTransform. TransByte ( data );
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetFrequency , Hex81Information. SetFrequencyLength , buffer , _id );
        }

        public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
        {
            float[ ] data = new float[6];
            data[0] = PhaseUa;
            data[1] = PhaseUb;
            data[2] = PhaseUc;
            data[3] = PhaseIa;
            data[4] = PhaseIb;
            data[5] = PhaseIc;
            byte[ ] buffer = _byteTransform. TransByte ( data );
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetPhase , Hex81Information. SetPhaseLength , buffer , _id );
        }

        public OperateResult<byte[ ]> Packet_SetWireMode ( byte wireMode )
        {
            byte[ ] data = new byte[1] { wireMode };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetWireMode , Hex81Information. SetWireModeLength , data , _id );
        }

        public OperateResult<byte[ ]> Packet_SetClosedLoop ( byte closeLoopMode , byte harmonicMode )
        {
            byte[ ] data = new byte[2] { closeLoopMode , harmonicMode };
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetClosedLoop , Hex81Information. SetClosedLoopLength , data , _id );
        }

        public OperateResult<byte[ ]> Packet_SetHarmonics ( byte channels , HarmonicArgs[ ]? harmonics = null )
        {
            //如何谐波组为空，则指令为清空谐波
            if ( harmonics == null )
            {
                byte[ ] dataClear = new byte[2] { channels , 0 };
                return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetHarmonics , ( ushort ) ( dataClear. Length + 7 ) , dataClear , _id );
            }
            //要设置的谐波个数
            byte count = ( byte ) harmonics. Length;

            //当协议报文长度超过256个时，禁止发送报文以避免下位机出错。【来源于Hex81协议要求】
            if ( count > 27 )
            {
                return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , "您设置的谐波个数超过了27个，建议您分批发送" + StringResources. GetCurSourceFileName ( ) );
            }
            //数据字节数组
            byte[ ] data = new byte[2 + count * 9];
            data[0] = channels;
            data[1] = count;

            //将谐波组分别转换成字节数组并复制到数据字节数组中
            for ( int i = 0 ; i < count ; i++ )
            {
              HarmonicArgs. HarmonicToBytes ( harmonics[i] , _byteTransform ). CopyTo ( data , i * 9 + 2 );
            }

            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetHarmonics , ( ushort ) ( data. Length + 7 ) , data , _id );
        }

        public OperateResult<byte[ ]> Packet_SetWattPower ( byte channel , float p )
        {
            byte[ ] data = new byte[5];
            data[0] = channel;
            _byteTransform. TransByte ( p ). CopyTo ( data , 1 );

            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetWattPower , Hex81Information. SetWattPowerLength , _id );
        }

        public OperateResult<byte[ ]> Packet_SetWattLessPower ( byte channel , float q )
        {
            byte[ ] data = new byte[5];
            data[0] = channel;
            _byteTransform. TransByte ( q ). CopyTo ( data , 1 );

            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetWattlessPower , Hex81Information. SetWattlessPowerLength , _id );
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. ReadData_ACS , _id );
        }

        public OperateResult<byte[ ]> Packet_ReadData_Status ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetStatus_ACS , _id );
        }

        public OperateResult<byte[ ]> Packet_Stop ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. CloseACS , _id );
        }

        public OperateResult<byte[ ]> Packet_Open ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. OpenACS , _id );
        }      
    }
}
