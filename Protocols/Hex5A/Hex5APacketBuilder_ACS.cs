using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5APacketBuilder_ACS : IPacketsBuilder_ACS
    {
        private readonly ushort _id;
        private readonly IByteTransform _transform;


        public Hex5APacketBuilder_ACS ( ushort id , IByteTransform byteTransform )
        {
            _id = id;
            _transform = byteTransform;
        }

        public OperateResult<byte[ ]> Packet_GetRanges ( )
        {
            byte[ ] data = new byte[1] { ( byte ) Type_Module. ACS };
            return Hex5APacketBuilderHelper. Instance. PacketShellBuilder ( Hex5AInformation. GetRanges_ACS , Hex5AInformation. GetRanges_ACS_Len ,data, _id );
        }

        public OperateResult<byte[ ]> Packet_Open ( )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_ReadData ( )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_ReadData_Status ( )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetAmplitude ( float UA , float UB , float UC , float IA , float IB , float IC , float IPA = 0 , float IPB = 0 , float IPC = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetClosedLoop ( CloseLoopMode closeLoopMode = CloseLoopMode. CloseLoop , HarmonicMode harmonicMode = HarmonicMode. ValidValuesConstant )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetFrequency ( float FreqOfAll , float FreqOfC = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetHarmonics ( byte channels , HarmonicArgs[ ]? harmonics = null )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetPhase ( float PhaseUa , float PhaseUb , float PhaseUc , float PhaseIa , float PhaseIb , float PhaseIc )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetRanges ( byte rangeIndexOfACU , byte rangeIndexOfACI , byte rangeIndexOfIP = 0 )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetWattLessPower ( byte channel , float q )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetWattPower ( byte channel , float p )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_SetWireMode ( WireMode wireMode )
        {
            throw new NotImplementedException ( );
        }

        public OperateResult<byte[ ]> Packet_Stop ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
