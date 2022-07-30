using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCU:IModuleDCU
    {
        private bool isModuleDCUConneted = true;       

        public bool IsModuleDCUConnected => isModuleDCUConneted;

        public ProtocolTypes ProtocolType { get => throw new NotImplementedException ( ); set => throw new NotImplementedException ( ); }

        public void SetDCU()
        {
            Console.WriteLine("ModuleDCU=600V");

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
