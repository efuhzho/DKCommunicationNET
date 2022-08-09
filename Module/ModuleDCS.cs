using DKCommunicationNET. Interface;
using DKCommunicationNET. Module. IModule;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCS : ModuleBase, IModuleDCS
    {
        public ModuleDCS ( Models protocolType )
        {
            _protocolType = protocolType;
        }

        public void GetRangesOfDCS ( )
        {
            throw new NotImplementedException ( );
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
