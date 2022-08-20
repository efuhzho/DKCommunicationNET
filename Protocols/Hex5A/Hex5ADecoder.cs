using DKCommunicationNET. Core;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Protocols. Hex5A
{
    internal class Hex5ADecoder : IDecoder
    {
        private readonly IByteTransform _byteTransform;
        public Hex5ADecoder ( IByteTransform byteTransform )
        {
            _byteTransform = byteTransform;
        }

        public int Offset => throw new NotImplementedException ( );

        public string? Model => throw new NotImplementedException ( );

        public string? Firmware => throw new NotImplementedException ( );

        public string? SN => throw new NotImplementedException ( );

        public bool IsEnabled_ACS => throw new NotImplementedException ( );

        public bool IsEnabled_ACM => throw new NotImplementedException ( );

        public bool IsEnabled_DCS => throw new NotImplementedException ( );

        public bool IsEnabled_DCM => throw new NotImplementedException ( );

        public bool IsEnabled_IO => throw new NotImplementedException ( );

        public bool IsEnabled_EPQ => throw new NotImplementedException ( );

        public bool IsEnabled_DualFreqs => throw new NotImplementedException ( );

        public bool IsEnabled_IProtect => throw new NotImplementedException ( );

        public bool IsEnabled_PST => throw new NotImplementedException ( );

        public bool IsEnabled_YX => throw new NotImplementedException ( );

        public bool IsEnabled_HF => throw new NotImplementedException ( );

        public bool IsEnabled_PWM => throw new NotImplementedException ( );

        public void DecodeHandShake ( OperateResult<byte[ ]> result )
        {
            throw new NotImplementedException ( );
        }
    }
}
