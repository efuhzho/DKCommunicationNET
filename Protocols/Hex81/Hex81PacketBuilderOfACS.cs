using System;
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
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. GetRanges_ACS , Hex81Information. GetRangesOfACSLength , _id );
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
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. SetACSAmplitude , Hex81Information. SetACSAmplitudeLength , buffer , _id );
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
            float[ ] data= new float[6];
            data[0] = PhaseUa;
            data[1] = PhaseUb;
            data[2] = PhaseUc;
            data[3] = PhaseIa;
            data[4] = PhaseIb;
            data[5] = PhaseIc;
            byte[ ] buffer = _byteTransform. TransByte ( data );

            return Hex81PacketBuilderHelper.Instance.PacketShellBuilder(Hex81Information.SetPhase, Hex81Information.SetPhaseLength , buffer , _id );
        }

        public OperateResult<byte[ ]> Packet_SetWireMode ( byte wireMode )
        {
            byte[ ] data= new byte[1]{ wireMode};
            return Hex81PacketBuilderHelper.Instance.PacketShellBuilder(Hex81Information.SetWireMode,Hex81Information.SetWireModeLength , data , _id );
        }

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Close ( )
        {
            return Hex81PacketBuilderHelper. Instance. PacketShellBuilder ( Hex81Information. CloseACS , Hex81Information. CloseACSLength , _id );
        }

        OperateResult<byte[ ]> IPacketsBuilder_ACS.Packet_Open ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
