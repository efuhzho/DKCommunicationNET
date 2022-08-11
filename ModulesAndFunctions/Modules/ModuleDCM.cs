using DKCommunicationNET. Interface;
using DKCommunicationNET. Module;

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
