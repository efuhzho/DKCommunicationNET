using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCI : IModuleDCI
    {
        public bool IsModuleDCIConnected { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }
        public ProtocolTypes ProtocolType { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

        public void SetDCI ( )
        {
            throw new NotImplementedException ( );
        }

        public void START ( )
        {
            throw new NotImplementedException ( );
        }

        public void STOP ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
