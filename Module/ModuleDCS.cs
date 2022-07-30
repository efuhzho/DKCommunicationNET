using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCS : ModuleBase, IModuleDCS
    {
        public ModuleDCS ( ProtocolTypes protocolType )
        {
            _protocolType = protocolType;
        }


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
