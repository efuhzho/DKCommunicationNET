using DKCommunicationNET. Interface;
using DKCommunicationNET. Module. IModule;

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
