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

        public int Offset => 6;

        public string? Model { get; private set; }

        public string? Firmware { get; private set; }

        public string? SN { get; private set; }

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

        public void DecodeHandShake ( OperateResult<byte[ ]> result )
        {
            return;
        }
    }
}
