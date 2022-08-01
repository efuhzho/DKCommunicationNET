using DKCommunicationNET. Interface;
using DKCommunicationNET. Interface. IModule;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCM : ModuleBase, IModuleDCM
    {
        public ModuleDCM ( Models model )
        {
            _protocolType = model;
        }

        public void GetRangesOfDCM ( )
        {
            throw new NotImplementedException ( );
        }
    }
}
