using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCS : IModuleDCS
    {
        public ModuleDCS ( ProtocolTypes protocolType )
        {

        }
        public bool IsDCSModuleConnected { get; set; }

        public void SetDCSAmplitude ( )
        {
            throw new NotImplementedException ( );
        }

        public void StartDCS ( )
        {
           
        }

        public void StopDCS ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
