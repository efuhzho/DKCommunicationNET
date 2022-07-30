using DKCommunicationNET. Interface;

namespace DKCommunicationNET. Module
{
    internal class ModuleDCM : ModuleBase, IModuleDCM
    {
        public ModuleDCM ( Models model )
        {
            _protocolType = model;
        }
    }
}
