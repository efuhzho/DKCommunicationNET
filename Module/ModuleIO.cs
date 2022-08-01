using DKCommunicationNET. Interface;
using DKCommunicationNET. Interface. IModule;

namespace DKCommunicationNET. Module
{
    internal class ModuleIO : ModuleBase, IModuleIO
    {
        public ModuleIO ( Models protocolType )
        {
            _protocolType = protocolType;
        }
    }
}
